namespace TelemetryClient
{
    partial class SetResetButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkGo = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkGo
            // 
            this.chkGo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.chkGo.ForeColor = System.Drawing.Color.White;
            this.chkGo.Location = new System.Drawing.Point(0, 0);
            this.chkGo.Name = "chkGo";
            this.chkGo.Size = new System.Drawing.Size(86, 38);
            this.chkGo.TabIndex = 0;
            this.chkGo.Text = "GO";
            this.chkGo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGo.UseVisualStyleBackColor = false;
            this.chkGo.CheckedChanged += new System.EventHandler(this.chkGo_CheckedChanged);
            this.chkGo.Click += new System.EventHandler(this.chkGo_Click);
            // 
            // SetResetButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkGo);
            this.Name = "SetResetButton";
            this.Size = new System.Drawing.Size(130, 78);
            this.Load += new System.EventHandler(this.GoButton_Load);
            this.BackColorChanged += new System.EventHandler(this.SetResetButton_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.SetResetButton_ForeColorChanged);
            this.Resize += new System.EventHandler(this.GoButton_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGo;
    }
}
