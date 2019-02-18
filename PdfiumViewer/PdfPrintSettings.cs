using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace PdfiumViewer
{
    /// <summary>
    /// Configures the print document.
    /// </summary>
    public class PdfPrintSettings
    {
        /// <summary>
        /// Gets the mode used to print margins.
        /// </summary>
        public PdfPrintMode Mode { get; }


        /// <summary>
        /// Gets configuration for printing multiple PDF pages on a single page.
        /// </summary>
        public PdfPrintMultiplePages MultiplePages { get; }

        /// <summary>
        /// Creates a new instance of the PdfPrintSettings class.
        /// </summary>
        /// <param name="mode">The mode used to print margins.</param>
        /// <param name="multiplePages">Configuration for printing multiple PDF
        /// pages on a single page.</param>
        public PdfPrintSettings(PdfPrintMode mode, PdfPrintMultiplePages multiplePages)
        {
            Mode = mode;
            MultiplePages = multiplePages;

            MultiPageLayout = new PdfPrintMultiPageLayout();
        }

        /// <summary>
        /// Creates a new instance of the PdfPrintSettings class.
        /// </summary>
        /// <param name="mode">The mode used to print margins.</param>
        /// <param name="multiplePages">Configuration for printing multiple PDF
        /// pages on a single page.</param>
        /// <param name="multiplePage">MultiPagePerSheet config</param>
        public PdfPrintSettings(PdfPrintMode mode, PdfPrintMultiplePages multiplePages, PdfPrintMultiPageLayout multiplePage)
        {
            Mode = mode;
            MultiplePages = multiplePages;

            MultiPageLayout = multiplePage;
        }

        public string PrinterName { get; set; }

        public string DocumentName { get; set; }

        public PaperSize PaperSize { get; set; }

        public short Copies { get; set; }

        public bool Collate { get; set; }

        public bool Landscape { get; set; }

        public PrintController PrintController { get; set; }

        public Duplex Duplex { get; set; }

        public int PrintFromPage { get; set; }

        public int PrintToPage { get; set; }

        public int[] PrintPages { get; set; }

        public PdfPageLayoutMode PageLayoutMode { get; set; } = PdfPageLayoutMode.SinglePage;

        public PdfPrintMultiPageLayout MultiPageLayout { get; set; }


        public void SetMultiPageLayout(int rows, int columns, bool hasPageBorder, PdfMultiPageOrder pageOrder)
        {
            this.PageLayoutMode = PdfPageLayoutMode.MultiPage;
            this.MultiPageLayout.Rows = rows;
            this.MultiPageLayout.Columns = columns;
            this.MultiPageLayout.DrawBorder = hasPageBorder;
            this.MultiPageLayout.MultiPageOrder = pageOrder;
        }

    }
}
