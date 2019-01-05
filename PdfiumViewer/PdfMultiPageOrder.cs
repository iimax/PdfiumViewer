using System;
using System.Collections.Generic;
using System.Text;

namespace PdfiumViewer
{
    /// <summary>
    /// 一张纸打印多页时，pdf每页内容在物理纸张上输出的方向
    /// </summary>
    public enum PdfMultiPageOrder
    {
        Horizontal,
        HorizontalReversed,
        Vertical,
        VerticalReversed
    }

}
