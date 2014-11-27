namespace TelemetryClient
{
    partial class frmInco
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
            this.chkRecord = new System.Windows.Forms.CheckBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblTerminal
            // 
            this.lblTerminal.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkRecord
            // 
            this.chkRecord.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRecord.Location = new System.Drawing.Point(12, 566);
            this.chkRecord.Name = "chkRecord";
            this.chkRecord.Size = new System.Drawing.Size(88, 36);
            this.chkRecord.TabIndex = 6;
            this.chkRecord.Text = "Record";
            this.chkRecord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRecord.UseVisualStyleBackColor = true;
            this.chkRecord.CheckedChanged += new System.EventHandler(this.chkRecord_CheckedChanged);
            // 
            // frmInco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(543, 611);
            this.Controls.Add(this.chkRecord);
            this.Name = "frmInco";
            this.Text = "Instrumentation and Communications Officer";
            this.Controls.SetChildIndex(this.lblTerminal, 0);
            this.Controls.SetChildIndex(this.chkRecord, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRecord;
        private System.Windows.Forms.SaveFileDialog dlgSave;
    }
}
