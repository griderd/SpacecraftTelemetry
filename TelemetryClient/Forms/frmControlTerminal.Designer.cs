namespace TelemetryClient
{
    partial class frmControlTerminal
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
            this.components = new System.ComponentModel.Container();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.lblMissionTime = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnNoGo = new TelemetryClient.SetResetButton();
            this.btnGo = new TelemetryClient.SetResetButton();
            this.SuspendLayout();
            // 
            // lblTerminal
            // 
            this.lblTerminal.BackColor = System.Drawing.Color.Black;
            this.lblTerminal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminal.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminal.ForeColor = System.Drawing.Color.LightGray;
            this.lblTerminal.Location = new System.Drawing.Point(12, 72);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Padding = new System.Windows.Forms.Padding(5);
            this.lblTerminal.Size = new System.Drawing.Size(517, 491);
            this.lblTerminal.TabIndex = 2;
            this.lblTerminal.Text = "This terminal screen has a character resolution of 45 columns by 30 rows.";
            // 
            // lblMissionTime
            // 
            this.lblMissionTime.AutoSize = true;
            this.lblMissionTime.BackColor = System.Drawing.Color.Black;
            this.lblMissionTime.Font = new System.Drawing.Font("Digital-7 Mono", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMissionTime.ForeColor = System.Drawing.Color.Red;
            this.lblMissionTime.Location = new System.Drawing.Point(12, 9);
            this.lblMissionTime.Name = "lblMissionTime";
            this.lblMissionTime.Size = new System.Drawing.Size(242, 49);
            this.lblMissionTime.TabIndex = 4;
            this.lblMissionTime.Text = "0:00:00:00";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // btnNoGo
            // 
            this.btnNoGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNoGo.Checked = false;
            this.btnNoGo.ForeColor = System.Drawing.Color.White;
            this.btnNoGo.Location = new System.Drawing.Point(427, 9);
            this.btnNoGo.Name = "btnNoGo";
            this.btnNoGo.SetBackColor = System.Drawing.Color.Red;
            this.btnNoGo.SetForeColor = System.Drawing.Color.White;
            this.btnNoGo.Size = new System.Drawing.Size(102, 49);
            this.btnNoGo.TabIndex = 5;
            this.btnNoGo.Load += new System.EventHandler(this.btnNoGo_Load);
            this.btnNoGo.Click += new System.EventHandler(this.btnNoGo_Click);
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnGo.Checked = false;
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Location = new System.Drawing.Point(319, 9);
            this.btnGo.Name = "btnGo";
            this.btnGo.SetBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGo.SetForeColor = System.Drawing.Color.White;
            this.btnGo.Size = new System.Drawing.Size(102, 49);
            this.btnGo.TabIndex = 3;
            this.btnGo.Load += new System.EventHandler(this.btnGo_Load);
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // frmControlTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 572);
            this.Controls.Add(this.btnNoGo);
            this.Controls.Add(this.lblMissionTime);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblTerminal);
            this.Name = "frmControlTerminal";
            this.Text = "Screen is 45x30";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControlTerminal_FormClosing);
            this.Load += new System.EventHandler(this.frmControlTerminal_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SetResetButton btnGo;
        private System.Windows.Forms.Label lblMissionTime;
        private SetResetButton btnNoGo;
        protected System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}