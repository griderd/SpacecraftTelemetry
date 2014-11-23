namespace TelemetryClient
{
    partial class frmFAO
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
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.lstDocuments = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblTerminal
            // 
            this.lblTerminal.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminal.Size = new System.Drawing.Size(659, 531);
            this.lblTerminal.Text = "This terminal has a resolution of 80 columns by 40 rows.";
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(783, 538);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(105, 65);
            this.btnNextPage.TabIndex = 12;
            this.btnNextPage.Text = "Next Page";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Location = new System.Drawing.Point(677, 538);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(100, 65);
            this.btnPreviousPage.TabIndex = 11;
            this.btnPreviousPage.Text = "Previous Page";
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // lstDocuments
            // 
            this.lstDocuments.FormattingEnabled = true;
            this.lstDocuments.Location = new System.Drawing.Point(677, 7);
            this.lstDocuments.Name = "lstDocuments";
            this.lstDocuments.Size = new System.Drawing.Size(211, 524);
            this.lstDocuments.TabIndex = 10;
            this.lstDocuments.SelectedIndexChanged += new System.EventHandler(this.lstDocuments_SelectedIndexChanged);
            // 
            // frmFAO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(897, 612);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPreviousPage);
            this.Controls.Add(this.lstDocuments);
            this.Name = "frmFAO";
            this.Text = "Flight Activities Officer";
            this.Load += new System.EventHandler(this.frmFAO_Load);
            this.Controls.SetChildIndex(this.lblTerminal, 0);
            this.Controls.SetChildIndex(this.lstDocuments, 0);
            this.Controls.SetChildIndex(this.btnPreviousPage, 0);
            this.Controls.SetChildIndex(this.btnNextPage, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.ListBox lstDocuments;
    }
}
