using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;

namespace PdfiumViewer
{
    public class PdfPrintMultiPageLayout
    {
        /// <summary>
        /// 每页pdf内容在物理纸张上输出方向
        /// </summary>
        public PdfMultiPageOrder MultiPageOrder { get; set; }

        /// <summary>
        /// 是否输出每页pdf的边框
        /// </summary>
        public bool DrawBorder { get; set; }

        /// <summary>
        /// 一张纸上打印输出多少行
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 一张纸上打印输出多少列
        /// </summary>
        public int Columns { get; set; }

        private float float_0;
        

        /// <summary>
        /// 设置每页pdf内容在物理纸张上输出方向
        /// </summary>
        /// <param name="pdfMultiPageOrder_1"></param>
        public void method_6(PdfMultiPageOrder pdfMultiPageOrder_1)
        {
            MultiPageOrder = pdfMultiPageOrder_1;
        }
        

        internal PdfPrintMultiPageLayout()
        {
            this.MultiPageOrder = PdfMultiPageOrder.Horizontal;
            this.DrawBorder = false;
            this.Rows = 2;
            this.Columns = 2;
            this.float_0 = 10f;
        }

        internal List<RectangleF> method_8(RectangleF rectangleF_0)
        {
            List<RectangleF> list_ = new List<RectangleF>();
            if (this.MultiPageOrder == PdfMultiPageOrder.Horizontal)
            {
                list_ = this.method_9(rectangleF_0);
            }
            if (this.MultiPageOrder == PdfMultiPageOrder.HorizontalReversed)
            {
                list_ = this.method_10(rectangleF_0);
            }
            if (this.MultiPageOrder == PdfMultiPageOrder.Vertical)
            {
                list_ = this.method_11(rectangleF_0);
            }
            if (this.MultiPageOrder == PdfMultiPageOrder.VerticalReversed)
            {
                list_ = this.method_12(rectangleF_0);
            }
            return this.method_13(list_);
        }

        /// <summary>
        /// 水平方向输出
        /// </summary>
        /// <param name="rectangleF_0"></param>
        /// <returns></returns>
        private List<RectangleF> method_9(RectangleF rectangleF_0)
        {
            List<RectangleF> list = new List<RectangleF>();
            float num = rectangleF_0.Y;
            for (int i = 0; i < this.Rows; i++)
            {
                float num2 = rectangleF_0.X;
                for (int j = 0; j < Columns; j++)
                {
                    list.Add(new RectangleF(num2, num, rectangleF_0.Width / (float)Columns, rectangleF_0.Height / (float)this.Rows));
                    num2 += rectangleF_0.Width / (float)this.Columns;
                }
                num += rectangleF_0.Height / (float)this.Rows;
            }
            return list;
        }

        private List<RectangleF> method_10(RectangleF rectangleF_0)
        {
            List<RectangleF> list = new List<RectangleF>();
            float num = rectangleF_0.Y;
            for (int i = 0; i < this.Rows; i++)
            {
                float num2 = rectangleF_0.X + rectangleF_0.Width - rectangleF_0.Width / (float)this.Columns;
                for (int j = 0; j < this.Columns; j++)
                {
                    list.Add(new RectangleF(num2, num, rectangleF_0.Width / (float)this.Columns, rectangleF_0.Height / (float)this.Rows));
                    num2 -= rectangleF_0.Width / (float)this.Columns;
                }
                num += rectangleF_0.Height / (float)this.Rows;
            }
            return list;
        }

        private List<RectangleF> method_11(RectangleF rectangleF_0)
        {
            List<RectangleF> list = new List<RectangleF>();
            float num = rectangleF_0.X;
            for (int i = 0; i < this.Columns; i++)
            {
                float num2 = rectangleF_0.Y;
                for (int j = 0; j < this.Rows; j++)
                {
                    list.Add(new RectangleF(num, num2, rectangleF_0.Width / (float)this.Columns, rectangleF_0.Height / (float)this.Rows));
                    num2 += rectangleF_0.Height / (float)this.Rows;
                }
                num += rectangleF_0.Width / (float)this.Columns;
            }
            return list;
        }

        private List<RectangleF> method_12(RectangleF rectangleF_0)
        {
            List<RectangleF> list = new List<RectangleF>();
            float num = rectangleF_0.X + rectangleF_0.Width - rectangleF_0.Width / (float)this.Columns;
            for (int i = 0; i < this.Columns; i++)
            {
                float num2 = rectangleF_0.Y;
                for (int j = 0; j < this.Rows; j++)
                {
                    list.Add(new RectangleF(num, num2, rectangleF_0.Width / (float)this.Columns, rectangleF_0.Height / (float)this.Rows));
                    num2 += rectangleF_0.Height / (float)this.Rows;
                }
                num -= rectangleF_0.Width / (float)this.Columns;
            }
            return list;
        }

        private List<RectangleF> method_13(List<RectangleF> list_0)
        {
            float num = (float)PrinterUnitConvert.Convert((double)(this.float_0 * 10f), PrinterUnit.ThousandthsOfAnInch, PrinterUnit.Display);
            List<RectangleF> list = new List<RectangleF>();
            foreach (RectangleF current in list_0)
            {
                float x = current.X + num;
                float y = current.Y + num;
                list.Add(new RectangleF(x, y, current.Width - 2f * num, current.Height - 2f * num));
            }
            return list;
        }
    }
}
