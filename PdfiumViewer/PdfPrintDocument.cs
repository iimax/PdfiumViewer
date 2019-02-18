using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Text;

namespace PdfiumViewer
{
    internal class PdfPrintDocument : PrintDocument
    {
        private readonly IPdfDocument _document;
        private readonly PdfPrintSettings _settings;
        private int _currentPage;

        public event QueryPageSettingsEventHandler BeforeQueryPageSettings;

        protected virtual void OnBeforeQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            var ev = BeforeQueryPageSettings;
            if (ev != null)
                ev(this, e);
        }

        public event PrintPageEventHandler BeforePrintPage;

        protected virtual void OnBeforePrintPage(PrintPageEventArgs e)
        {
            var ev = BeforePrintPage;
            if (ev != null)
                ev(this, e);
        }

        public PdfPrintDocument(IPdfDocument document, PdfPrintSettings settings)
        {
            if (document == null)
                throw new ArgumentNullException("document");

            _document = document;
            _settings = settings;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            _currentPage = PrinterSettings.FromPage == 0 ? 0 : PrinterSettings.FromPage - 1;

            base.OnBeginPrint(e);
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            OnBeforeQueryPageSettings(e);

            // Some printers misreport landscape. The below check verifies
            // whether the page rotation matches the landscape setting.
            bool inverseLandscape = e.PageSettings.Bounds.Width > e.PageSettings.Bounds.Height != e.PageSettings.Landscape;
            //System.IO.File.WriteAllText(string.Format("{0}-1.log",DateTime.Now.ToString("HHmmss")), $"PageSettings={e.PageSettings.Bounds.ToString()}, Landscape={e.PageSettings.Landscape}");

            if (_settings.MultiplePages == null && _currentPage < _document.PageCount)
            {
                bool landscape = GetOrientation(_document.PageSizes[_currentPage]) == Orientation.Landscape;

                if (inverseLandscape)
                    landscape = !landscape;

                e.PageSettings.Landscape = landscape;
            }

            if (_settings.MultiplePages != null) // multiple page per sheet printing
            {
                if (_settings.MultiplePages.Horizontal == 1 || _settings.MultiplePages.Vertical == 1) //reverse landscape to get best size
                {
                    bool landscape = GetOrientation(_document.PageSizes[_currentPage]) == Orientation.Landscape;

                    if (inverseLandscape)
                        landscape = !landscape;

                    e.PageSettings.Landscape = !landscape;
                    //System.IO.File.WriteAllText(string.Format("{0}-Reverse.log", DateTime.Now.ToString("HHmmss")), string.Format("page# {0} reverse landscape to {1}", _currentPage * (_settings.MultiplePages.Horizontal * _settings.MultiplePages.Vertical), e.PageSettings.Landscape));
                    
                    //System.Diagnostics.Debug.WriteLine(string.Format("page# {0} reverse landscape to {1}", _currentPage * (_settings.MultiplePages.Horizontal * _settings.MultiplePages.Vertical), e.PageSettings.Landscape));
                }
                else
                {
                    //keep it's landscape
                    bool landscape = GetOrientation(_document.PageSizes[_currentPage]) == Orientation.Landscape;

                    if (inverseLandscape)
                        landscape = !landscape;

                    e.PageSettings.Landscape = landscape;
                }
            }

            base.OnQueryPageSettings(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            OnBeforePrintPage(e);

            if (_settings.MultiplePages != null)
                PrintMultiplePagesAdvanced(e);
            else
                PrintSinglePage(e);

            base.OnPrintPage(e);
        }

        private void PrintMultiplePages(PrintPageEventArgs e)
        {
            var settings = _settings.MultiplePages;

            int pagesPerPage = settings.Horizontal * settings.Vertical;
            int pageCount = (_document.PageCount - 1) / pagesPerPage + 1;

            if (_currentPage < pageCount)
            {
                double width = e.PageBounds.Width - e.PageSettings.HardMarginX * 2;
                double height = e.PageBounds.Height - e.PageSettings.HardMarginY * 2;

                double widthPerPage = (width - (settings.Horizontal - 1) * settings.Margin) / settings.Horizontal;
                double heightPerPage = (height - (settings.Vertical - 1) * settings.Margin) / settings.Vertical;

                for (int horizontal = 0; horizontal < settings.Horizontal; horizontal++)
                {
                    for (int vertical = 0; vertical < settings.Vertical; vertical++)
                    {
                        int page = _currentPage * pagesPerPage;
                        if (settings.Orientation == System.Windows.Forms.Orientation.Horizontal)
                        {
                            //page += vertical * settings.Vertical + horizontal;
                            page += vertical * settings.Horizontal + horizontal;
                        }
                        else
                        {
                            //page += horizontal * settings.Horizontal + vertical;
                            page += horizontal * settings.Vertical + vertical;
                        }

                        if (page >= _document.PageCount)
                            continue;

                        double pageLeft = (widthPerPage + settings.Margin) * horizontal;
                        double pageTop = (heightPerPage + settings.Margin) * vertical;

                        RenderPage(e, page, pageLeft, pageTop, widthPerPage, heightPerPage);
                    }
                }

                _currentPage++;
            }

            if (PrinterSettings.ToPage > 0)
                pageCount = Math.Min(PrinterSettings.ToPage, pageCount);

            e.HasMorePages = _currentPage < pageCount;
        }

        private void PrintSinglePage(PrintPageEventArgs e)
        {
            if (_currentPage < _document.PageCount)
            {
                var pageOrientation = GetOrientation(_document.PageSizes[_currentPage]);
                var printOrientation = GetOrientation(e.PageBounds.Size);

                e.PageSettings.Landscape = pageOrientation == Orientation.Landscape;

                double left;
                double top;
                double width;
                double height;

                if (_settings.Mode == PdfPrintMode.ShrinkToMargin)
                {
                    left = 0;
                    top = 0;
                    width = e.PageBounds.Width - e.PageSettings.HardMarginX * 2;
                    height = e.PageBounds.Height - e.PageSettings.HardMarginY * 2;
                }
                else
                {
                    left = -e.PageSettings.HardMarginX;
                    top = -e.PageSettings.HardMarginY;
                    width = e.PageBounds.Width;
                    height = e.PageBounds.Height;
                }

                if (pageOrientation != printOrientation)
                {
                    Swap(ref height, ref width);
                    Swap(ref left, ref top);
                }

                RenderPage(e, _currentPage, left, top, width, height);
                _currentPage++;
            }

            int pageCount = PrinterSettings.ToPage == 0
                ? _document.PageCount
                : Math.Min(PrinterSettings.ToPage, _document.PageCount);

            e.HasMorePages = _currentPage < pageCount;
        }

        private void RenderPage(PrintPageEventArgs e, int page, double left, double top, double width, double height)
        {
            var size = _document.PageSizes[page];

            double pageScale = size.Height / size.Width;
            double printScale = height / width;

            double scaledWidth = width;
            double scaledHeight = height;

            if (pageScale > printScale)
                scaledWidth = width * (printScale / pageScale);
            else
                scaledHeight = height * (pageScale / printScale);

            left += (width - scaledWidth) / 2;
            top += (height - scaledHeight) / 2;

            //var img = _document.Render(page, Convert.ToInt32(width), Convert.ToInt32(height), 96, 96, PdfRotation.Rotate0, PdfRenderFlags.Annotations);
            //img.Save(string.Format("{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmss")));

            //由于RenderPDFPageToDC无法显示 annotation(比如电子章)，这里先把页面转为图片，然后再打印
            Image image = _document.Render(page,
                AdjustDpi(e.Graphics.DpiX, scaledWidth),
                AdjustDpi(e.Graphics.DpiY, scaledHeight),
                e.Graphics.DpiX,
                e.Graphics.DpiY,
                PdfRotation.Rotate0, PdfRenderFlags.ForPrinting | PdfRenderFlags.Annotations);

            e.Graphics.DrawImageUnscaled(image, e.PageBounds.Location);

            //_document.Render(
            //    page,
            //    e.Graphics,
            //    e.Graphics.DpiX,
            //    e.Graphics.DpiY,
            //    new Rectangle(
            //        AdjustDpi(e.Graphics.DpiX, left),
            //        AdjustDpi(e.Graphics.DpiY, top),
            //        AdjustDpi(e.Graphics.DpiX, scaledWidth),
            //        AdjustDpi(e.Graphics.DpiY, scaledHeight)
            //    ),
            //    PdfRenderFlags.ForPrinting | PdfRenderFlags.Annotations
            //);
        }

        private static void Swap(ref double a, ref double b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        private static int AdjustDpi(double value, double dpi)
        {
            return (int)((value / 100.0) * dpi);
        }

        private Orientation GetOrientation(SizeF pageSize)
        {
            if (pageSize.Height > pageSize.Width)
                return Orientation.Portrait;
            return Orientation.Landscape;
        }

        private enum Orientation
        {
            Portrait,
            Landscape
        }
        
        /// <summary>
        /// 每页pdf内容输出在物理纸张上的坐标
        /// </summary>
        private List<RectangleF> lstPdfPagePrintArea = new List<RectangleF>();
        
        /// <summary>
        /// 新版MultiPagesPerSheet打印
        /// </summary>
        /// <param name="e"></param>
        private void PrintMultiplePagesAdvanced(PrintPageEventArgs e)
        {
            var settings = _settings.MultiplePages;

            int pagesPerPage = settings.Horizontal * settings.Vertical;
            int pageCount = (_document.PageCount - 1) / pagesPerPage + 1;
            
            if (_currentPage < pageCount)
            {
                float width = e.PageBounds.Width - e.PageSettings.HardMarginX * 2;
                float height = e.PageBounds.Height - e.PageSettings.HardMarginY * 2;
                //System.IO.File.WriteAllText(string.Format("Page {0}-{1}.log", _currentPage, DateTime.Now.ToString("HHmmss"))
                //    , $"PageSettings={e.PageBounds.ToString()}, Rows={_settings.MultiPageLayout.Rows}, Columns={_settings.MultiPageLayout.Columns}");
                //e.Cancel = true;
                //return;
                float widthPerPage = (width - (settings.Horizontal - 1) * settings.Margin) / settings.Horizontal;
                float heightPerPage = (height - (settings.Vertical - 1) * settings.Margin) / settings.Vertical;
                //提前根据用户自定义布局，计算好每页pdf内容输出的坐标：
                //StringBuilder sb = new StringBuilder();
                //sb.AppendFormat("PageBounds={0}\r\n", e.PageBounds.ToString());
                if (_settings.MultiPageLayout.MultiPageOrder == PdfMultiPageOrder.Horizontal)
                {
                    if (lstPdfPagePrintArea.Count == 0)
                    {
                        //第一页的内容
                        float x = 0;
                        float y = 0;
                        int drawedPages = 0;
                        for (int physicalPage = 0; physicalPage < pageCount; physicalPage++)
                        {
                            x = 0;// + e.PageSettings.HardMarginX;
                            y = 0;// + e.PageSettings.HardMarginY;

                            for (int i = 0; i < _settings.MultiPageLayout.Rows; i++)
                            {
                                for (int j = 0; j < _settings.MultiPageLayout.Columns; j++)
                                {
                                    lstPdfPagePrintArea.Add(new RectangleF(x, y, widthPerPage, heightPerPage));
                                    //sb.AppendFormat("Page# {0},{1} width={2}, height={3}\r\n", x, y, widthPerPage, heightPerPage);
                                    x += widthPerPage + settings.Margin;
                                    drawedPages++;
                                    if (drawedPages >= _document.PageCount)
                                    {
                                        break;
                                    }
                                }
                                if (drawedPages >= _document.PageCount)
                                {
                                    break;
                                }
                                x = 0;// + e.PageSettings.HardMarginX;
                                y += heightPerPage + settings.Margin;
                            }
                        }

                        //System.IO.File.WriteAllText("PageLayout.log", sb.ToString());
                    }

                    
                }
                else if (_settings.MultiPageLayout.MultiPageOrder == PdfMultiPageOrder.Vertical)
                {
                    //按从上到下顺序输出页面
                    if (lstPdfPagePrintArea.Count == 0)
                    {
                        //第一页的内容
                        float x = 0;
                        float y = 0;
                        int drawedPages = 0;
                        for (int physicalPage = 0; physicalPage < pageCount; physicalPage++)
                        {
                            x = 0;// + e.PageSettings.HardMarginX;
                            y = 0;// + e.PageSettings.HardMarginY;

                            for (int i = 0; i < _settings.MultiPageLayout.Columns; i++)
                            {
                                for (int j = 0; j < _settings.MultiPageLayout.Rows; j++)
                                {
                                    lstPdfPagePrintArea.Add(new RectangleF(x, y, widthPerPage, heightPerPage));
                                    y += heightPerPage + settings.Margin;
                                    drawedPages++;
                                    if (drawedPages >= _document.PageCount)
                                    {
                                        break;
                                    }
                                }
                                if (drawedPages >= _document.PageCount)
                                {
                                    break;
                                }
                                y = 0;// + e.PageSettings.HardMarginY;
                                x += widthPerPage + settings.Margin;
                            }
                            
                        }
                    }
                }

                //读取坐标，模拟画一个边框
                int page = _currentPage * pagesPerPage;
                for (int i = page; i < lstPdfPagePrintArea.Count; i++)
                {
                    var image = _document.Render(i, e.Graphics.DpiX, e.Graphics.DpiY, true);
                    //保持原始page宽高比例
                    var destWidth = (float)lstPdfPagePrintArea[i].Width;
                    var destHeight = (float)lstPdfPagePrintArea[i].Height;

                    var xRatio = destWidth / image.Width;
                    var yRatio = destHeight / image.Height;

                    var ratio = Math.Min(xRatio, yRatio);
                    float printWidth = image.Width;
                    float printHeight = image.Height;
                    if (ratio < 1)
                    {
                        printWidth = printWidth * ratio;
                        printHeight = printHeight * ratio;
                    }
                    //将pdf内容页，放在本区域的中心位置
                    var offsetCenterX = lstPdfPagePrintArea[i].Width - printWidth;
                    var offsetCenterY = lstPdfPagePrintArea[i].Height - printHeight;
                    offsetCenterX = offsetCenterX > 0 ? (offsetCenterX / 2) : 0;
                    offsetCenterY = offsetCenterY > 0 ? (offsetCenterY / 2) : 0;

                    e.Graphics.DrawImage(image, lstPdfPagePrintArea[i].X + offsetCenterX, lstPdfPagePrintArea[i].Y + offsetCenterY, printWidth, printHeight);
                    //System.IO.File.WriteAllText(string.Format("Page {0} Render.log", i)
                    //    , $"x={lstPdfPagePrintArea[i].X} + {offsetCenterX}, y={lstPdfPagePrintArea[i].Y} + {offsetCenterY}, printWidth={printWidth}, {printHeight}. image={image.Width}, {image.Height}");
                    //float centerX = lstPdfPagePrintArea[i].X + lstPdfPagePrintArea[i].Width / 2;
                    //float centerY = lstPdfPagePrintArea[i].Y + lstPdfPagePrintArea[i].Height / 2;
                    //e.Graphics.DrawString(string.Format("#{0}", i + 1), new Font("微软雅黑", 22), Brushes.Red, centerX, centerY);


                    if (_settings.MultiPageLayout.DrawBorder)
                    {
                        e.Graphics.DrawRectangle(Pens.Black, lstPdfPagePrintArea[i].X, lstPdfPagePrintArea[i].Y, lstPdfPagePrintArea[i].Width, lstPdfPagePrintArea[i].Height);
                    }


                    if (i >= page + pagesPerPage - 1)
                    {
                        break;
                    }
                }

                //// Set world transform of graphics object to rotate.
                //e.Graphics.RotateTransform(30.0F);

                //// Then to scale, prepending to world transform.
                //e.Graphics.ScaleTransform(3.0F, 1.0F);

                //// Draw scaled, rotated rectangle to screen.
                //e.Graphics.DrawRectangle(new Pen(Color.Blue, 3), 50, 0, 100, 40);

                _currentPage++;
            }

            if (PrinterSettings.ToPage > 0)
                pageCount = Math.Min(PrinterSettings.ToPage, pageCount);

            e.HasMorePages = _currentPage < pageCount;
        }
        
    }
}
