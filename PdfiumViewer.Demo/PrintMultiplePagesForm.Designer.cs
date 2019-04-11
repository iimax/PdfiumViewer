namespace PdfiumViewer.Demo
{
    partial class PrintMultiplePagesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this._horizontal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._vertical = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._margin = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._verticalOrientation = new System.Windows.Forms.RadioButton();
            this._horizontalOrientation = new System.Windows.Forms.RadioButton();
            this._acceptButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.nbrFrom = new System.Windows.Forms.NumericUpDown();
            this.nbrTo = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLayout = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrTo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Horizontal:";
            // 
            // _horizontal
            // 
            this._horizontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._horizontal.Location = new System.Drawing.Point(86, 11);
            this._horizontal.Name = "_horizontal";
            this._horizontal.Size = new System.Drawing.Size(308, 21);
            this._horizontal.TabIndex = 1;
            this._horizontal.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vertical:";
            // 
            // _vertical
            // 
            this._vertical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._vertical.Location = new System.Drawing.Point(86, 35);
            this._vertical.Name = "_vertical";
            this._vertical.Size = new System.Drawing.Size(308, 21);
            this._vertical.TabIndex = 3;
            this._vertical.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Margin:";
            // 
            // _margin
            // 
            this._margin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._margin.Location = new System.Drawing.Point(86, 59);
            this._margin.Name = "_margin";
            this._margin.Size = new System.Drawing.Size(308, 21);
            this._margin.TabIndex = 5;
            this._margin.Text = "5";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._verticalOrientation);
            this.groupBox1.Controls.Add(this._horizontalOrientation);
            this.groupBox1.Location = new System.Drawing.Point(86, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 65);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Orientation";
            // 
            // _verticalOrientation
            // 
            this._verticalOrientation.AutoSize = true;
            this._verticalOrientation.Location = new System.Drawing.Point(13, 40);
            this._verticalOrientation.Name = "_verticalOrientation";
            this._verticalOrientation.Size = new System.Drawing.Size(71, 16);
            this._verticalOrientation.TabIndex = 1;
            this._verticalOrientation.Text = "Vertical";
            this._verticalOrientation.UseVisualStyleBackColor = true;
            // 
            // _horizontalOrientation
            // 
            this._horizontalOrientation.AutoSize = true;
            this._horizontalOrientation.Checked = true;
            this._horizontalOrientation.Location = new System.Drawing.Point(13, 18);
            this._horizontalOrientation.Name = "_horizontalOrientation";
            this._horizontalOrientation.Size = new System.Drawing.Size(83, 16);
            this._horizontalOrientation.TabIndex = 0;
            this._horizontalOrientation.TabStop = true;
            this._horizontalOrientation.Text = "Horizontal";
            this._horizontalOrientation.UseVisualStyleBackColor = true;
            // 
            // _acceptButton
            // 
            this._acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._acceptButton.Location = new System.Drawing.Point(238, 159);
            this._acceptButton.Name = "_acceptButton";
            this._acceptButton.Size = new System.Drawing.Size(75, 21);
            this._acceptButton.TabIndex = 7;
            this._acceptButton.Text = "Print";
            this._acceptButton.UseVisualStyleBackColor = true;
            this._acceptButton.Click += new System.EventHandler(this._acceptButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(319, 159);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 21);
            this._cancelButton.TabIndex = 8;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // nbrFrom
            // 
            this.nbrFrom.Location = new System.Drawing.Point(13, 159);
            this.nbrFrom.Name = "nbrFrom";
            this.nbrFrom.Size = new System.Drawing.Size(46, 21);
            this.nbrFrom.TabIndex = 9;
            this.nbrFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nbrTo
            // 
            this.nbrTo.Location = new System.Drawing.Point(81, 158);
            this.nbrTo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nbrTo.Name = "nbrTo";
            this.nbrTo.Size = new System.Drawing.Size(46, 21);
            this.nbrTo.TabIndex = 10;
            this.nbrTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "-";
            // 
            // cboLayout
            // 
            this.cboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayout.FormattingEnabled = true;
            this.cboLayout.Items.AddRange(new object[] {
            "请选择多页布局",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cboLayout.Location = new System.Drawing.Point(144, 159);
            this.cboLayout.Name = "cboLayout";
            this.cboLayout.Size = new System.Drawing.Size(68, 20);
            this.cboLayout.TabIndex = 12;
            // 
            // PrintMultiplePagesForm
            // 
            this.AcceptButton = this._acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(406, 191);
            this.Controls.Add(this.cboLayout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nbrTo);
            this.Controls.Add(this.nbrFrom);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._acceptButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._margin);
            this.Controls.Add(this._vertical);
            this.Controls.Add(this._horizontal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PrintMultiplePagesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Multiple Pages";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbrTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _horizontal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _vertical;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _margin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _verticalOrientation;
        private System.Windows.Forms.RadioButton _horizontalOrientation;
        private System.Windows.Forms.Button _acceptButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.NumericUpDown nbrFrom;
        private System.Windows.Forms.NumericUpDown nbrTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLayout;
    }
}