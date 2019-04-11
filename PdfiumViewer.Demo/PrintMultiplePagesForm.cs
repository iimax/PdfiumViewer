using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfiumViewer.Demo
{
    public partial class PrintMultiplePagesForm : Form
    {
        private readonly PdfViewer _viewer;
        private int numberOfPages = 0;

        public PrintMultiplePagesForm(PdfViewer viewer)
        {
            if (viewer == null)
                throw new ArgumentNullException(nameof(viewer));

            _viewer = viewer;

            numberOfPages = _viewer.Document.PageCount;
            
            InitializeComponent();

            nbrFrom.Value = 1;
            nbrTo.Value = numberOfPages;
        }

        private void _acceptButton_Click(object sender, EventArgs e)
        {
            int horizontal = 1; //列
            int vertical = 1;   //行
            bool Horizontal = false; //一张纸打印多页时，排版从左到右，还是从上到下
            bool multiPagePerSheet = false;
            var layout = cboLayout.SelectedIndex;
            if (layout > 1)
            {
                multiPagePerSheet = true;
                switch (layout)
                {
                    case 2: //一张纸打两页
                        horizontal = 1;
                        vertical = 2;
                        Horizontal = true;
                        break;
                    case 3: //一张纸打 3页
                        horizontal = 1;
                        vertical = 3;
                        Horizontal = true;
                        break;
                    case 4:
                        horizontal = 2;
                        vertical = 2;
                        Horizontal = true;
                        break;
                    case 5:
                        horizontal = 2;
                        vertical = 2;
                        Horizontal = false;
                        break;
                    case 6:
                        horizontal = 2;
                        vertical = 3;
                        Horizontal = true;
                        break;
                    case 7:
                        horizontal = 2;
                        vertical = 3;
                        Horizontal = false;
                        break;
                    case 8:
                        horizontal = 3;
                        vertical = 3;
                        Horizontal = true;
                        break;
                    case 9:
                        horizontal = 3;
                        vertical = 3;
                        Horizontal = false;
                        break;
                    default:
                        Console.Write("Unhandled layout, EXIT.");
                        Application.Exit();
                        return;
                        //break;
                }

            }
            else
            {
                if (!int.TryParse(_horizontal.Text, out horizontal))
                {
                    MessageBox.Show(this, "Invalid horizontal");
                    return;
                }
                if (!int.TryParse(_vertical.Text, out vertical))
                {
                    MessageBox.Show(this, "Invalid vertical");
                    return;
                }
            }

            //int horizontal;
            //int vertical;
            float margin;

            int page_from = Convert.ToInt32(nbrFrom.Value);
            int page_to = Convert.ToInt32(nbrTo.Value);

            if (!float.TryParse(_margin.Text, out margin))
            {
                MessageBox.Show(this, "Invalid margin");
                return;
            }
            else
            {
                if (layout > 1)
                {
                    try
                    {
                        if (page_from != 1 || page_to != numberOfPages)
                        {
                            //log.DebugFormat("BEFORE PageCount={0}", document.PageCount);
                            //开始裁剪PDF
                            if (page_to != numberOfPages)
                            {
                                for (int i = numberOfPages; i > page_to; i--)
                                {
                                    //log.DebugFormat("DeletePage #{0}", i);
                                    _viewer.Document.DeletePage(i - 1); //FUCK,删除索引从0开始
                                }
                            }

                            if (page_from != 1)
                            {
                                for (int i = page_from - 1; i >= 1; i--)
                                {
                                    //log.DebugFormat("DeletePage #{0}", i);
                                    _viewer.Document.DeletePage(i - 1);
                                }
                            }
                            //log.DebugFormat("AFTER PageCount={0}", document.PageCount);
                            //printerSetting.PrintRange = PrintRange.AllPages;
                        }
                    }
                    catch (Exception ex)
                    {
                        //log.Error("删除页面失败 {0}", ex);
                    }
                }
                var settings = new PdfPrintSettings(
                    _viewer.DefaultPrintMode,
                    new PdfPrintMultiplePages(
                        horizontal,
                        vertical,
                        _horizontalOrientation.Checked ? Orientation.Horizontal : Orientation.Vertical,
                        margin
                    )
                );
                settings.SetMultiPageLayout(vertical, horizontal, true, _horizontalOrientation.Checked ? PdfMultiPageOrder.Horizontal : PdfMultiPageOrder.Vertical);
                settings.PrinterName = "";
                
                using (var form = new PrintPreviewDialog())
                {
                    form.Document = _viewer.Document.CreatePrintDocument(settings);
                    form.Document.DefaultPageSettings.Landscape = false;
                    form.ShowDialog(this);
                }

                DialogResult = DialogResult.OK;
            }
        }
    }
}
