namespace TelemetryClient
{
    partial class frmBooster
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
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnResetDeltaV = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAbort
            // 
            this.btnAbort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAbort.ForeColor = System.Drawing.Color.White;
            this.btnAbort.Location = new System.Drawing.Point(535, 498);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(124, 64);
            this.btnAbort.TabIndex = 6;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = false;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // btnResetDeltaV
            // 
            this.btnResetDeltaV.Location = new System.Drawing.Point(535, 72);
            this.btnResetDeltaV.Name = "btnResetDeltaV";
            this.btnResetDeltaV.Size = new System.Drawing.Size(117, 48);
            this.btnResetDeltaV.TabIndex = 7;
            this.btnResetDeltaV.Text = "Reset ΔV";
            this.btnResetDeltaV.UseVisualStyleBackColor = true;
            this.btnResetDeltaV.Click += new System.EventHandler(this.btnResetDeltaV_Click);
            // 
            // frmBooster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(664, 574);
            this.Controls.Add(this.btnResetDeltaV);
            this.Controls.Add(this.btnAbort);
            this.Name = "frmBooster";
            this.Text = "Booster Systems Engineer";
            this.Load += new System.EventHandler(this.frmBooster_Load);
            this.Controls.SetChildIndex(this.btnAbort, 0);
            this.Controls.SetChildIndex(this.lblTerminal, 0);
            this.Controls.SetChildIndex(this.btnResetDeltaV, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnResetDeltaV;
    }
}
