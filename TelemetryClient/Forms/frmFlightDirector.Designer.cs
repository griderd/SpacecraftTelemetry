namespace TelemetryClient
{
    partial class frmFlightDirector
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.lblFAO = new System.Windows.Forms.Label();
            this.lblProcedures = new System.Windows.Forms.Label();
            this.lblINCO = new System.Windows.Forms.Label();
            this.lblControl = new System.Windows.Forms.Label();
            this.lblTelmu = new System.Windows.Forms.Label();
            this.lblGNC = new System.Windows.Forms.Label();
            this.lblEECOM = new System.Windows.Forms.Label();
            this.lblCapcom = new System.Windows.Forms.Label();
            this.lblSurgeon = new System.Windows.Forms.Label();
            this.lblGuido = new System.Windows.Forms.Label();
            this.lblFDO = new System.Windows.Forms.Label();
            this.lblRetro = new System.Windows.Forms.Label();
            this.lblBooster = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCountdownReset = new System.Windows.Forms.Button();
            this.btnCountdownHold = new System.Windows.Forms.Button();
            this.btnCountdownStart = new System.Windows.Forms.Button();
            this.nudTime = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTerminal
            // 
            this.lblTerminal.Location = new System.Drawing.Point(12, 68);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblNetwork);
            this.groupBox1.Controls.Add(this.lblFAO);
            this.groupBox1.Controls.Add(this.lblProcedures);
            this.groupBox1.Controls.Add(this.lblINCO);
            this.groupBox1.Controls.Add(this.lblControl);
            this.groupBox1.Controls.Add(this.lblTelmu);
            this.groupBox1.Controls.Add(this.lblGNC);
            this.groupBox1.Controls.Add(this.lblEECOM);
            this.groupBox1.Controls.Add(this.lblCapcom);
            this.groupBox1.Controls.Add(this.lblSurgeon);
            this.groupBox1.Controls.Add(this.lblGuido);
            this.groupBox1.Controls.Add(this.lblFDO);
            this.groupBox1.Controls.Add(this.lblRetro);
            this.groupBox1.Controls.Add(this.lblBooster);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(535, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 550);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GO/NO-GO POLL";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(6, 439);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(102, 48);
            this.btnReset.TabIndex = 35;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // lblNetwork
            // 
            this.lblNetwork.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblNetwork.ForeColor = System.Drawing.Color.White;
            this.lblNetwork.Location = new System.Drawing.Point(114, 377);
            this.lblNetwork.Name = "lblNetwork";
            this.lblNetwork.Size = new System.Drawing.Size(102, 49);
            this.lblNetwork.TabIndex = 34;
            this.lblNetwork.Text = "NETWORK";
            this.lblNetwork.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFAO
            // 
            this.lblFAO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblFAO.ForeColor = System.Drawing.Color.White;
            this.lblFAO.Location = new System.Drawing.Point(6, 377);
            this.lblFAO.Name = "lblFAO";
            this.lblFAO.Size = new System.Drawing.Size(102, 49);
            this.lblFAO.TabIndex = 33;
            this.lblFAO.Text = "FAO";
            this.lblFAO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcedures
            // 
            this.lblProcedures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblProcedures.ForeColor = System.Drawing.Color.White;
            this.lblProcedures.Location = new System.Drawing.Point(114, 319);
            this.lblProcedures.Name = "lblProcedures";
            this.lblProcedures.Size = new System.Drawing.Size(102, 49);
            this.lblProcedures.TabIndex = 32;
            this.lblProcedures.Text = "PROCEDURES";
            this.lblProcedures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblINCO
            // 
            this.lblINCO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblINCO.ForeColor = System.Drawing.Color.White;
            this.lblINCO.Location = new System.Drawing.Point(6, 319);
            this.lblINCO.Name = "lblINCO";
            this.lblINCO.Size = new System.Drawing.Size(102, 49);
            this.lblINCO.TabIndex = 31;
            this.lblINCO.Text = "INCO";
            this.lblINCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblControl
            // 
            this.lblControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblControl.ForeColor = System.Drawing.Color.White;
            this.lblControl.Location = new System.Drawing.Point(114, 261);
            this.lblControl.Name = "lblControl";
            this.lblControl.Size = new System.Drawing.Size(102, 49);
            this.lblControl.TabIndex = 30;
            this.lblControl.Text = "CONTROL";
            this.lblControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTelmu
            // 
            this.lblTelmu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblTelmu.ForeColor = System.Drawing.Color.White;
            this.lblTelmu.Location = new System.Drawing.Point(6, 261);
            this.lblTelmu.Name = "lblTelmu";
            this.lblTelmu.Size = new System.Drawing.Size(102, 49);
            this.lblTelmu.TabIndex = 29;
            this.lblTelmu.Text = "TELMU";
            this.lblTelmu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGNC
            // 
            this.lblGNC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblGNC.ForeColor = System.Drawing.Color.White;
            this.lblGNC.Location = new System.Drawing.Point(114, 203);
            this.lblGNC.Name = "lblGNC";
            this.lblGNC.Size = new System.Drawing.Size(102, 49);
            this.lblGNC.TabIndex = 28;
            this.lblGNC.Text = "GNC";
            this.lblGNC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEECOM
            // 
            this.lblEECOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblEECOM.ForeColor = System.Drawing.Color.White;
            this.lblEECOM.Location = new System.Drawing.Point(6, 203);
            this.lblEECOM.Name = "lblEECOM";
            this.lblEECOM.Size = new System.Drawing.Size(102, 49);
            this.lblEECOM.TabIndex = 27;
            this.lblEECOM.Text = "EECOM";
            this.lblEECOM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCapcom
            // 
            this.lblCapcom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblCapcom.ForeColor = System.Drawing.Color.White;
            this.lblCapcom.Location = new System.Drawing.Point(114, 145);
            this.lblCapcom.Name = "lblCapcom";
            this.lblCapcom.Size = new System.Drawing.Size(102, 49);
            this.lblCapcom.TabIndex = 26;
            this.lblCapcom.Text = "CAPCOM";
            this.lblCapcom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSurgeon
            // 
            this.lblSurgeon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblSurgeon.ForeColor = System.Drawing.Color.White;
            this.lblSurgeon.Location = new System.Drawing.Point(6, 145);
            this.lblSurgeon.Name = "lblSurgeon";
            this.lblSurgeon.Size = new System.Drawing.Size(102, 49);
            this.lblSurgeon.TabIndex = 25;
            this.lblSurgeon.Text = "SURGEON";
            this.lblSurgeon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGuido
            // 
            this.lblGuido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblGuido.ForeColor = System.Drawing.Color.White;
            this.lblGuido.Location = new System.Drawing.Point(114, 86);
            this.lblGuido.Name = "lblGuido";
            this.lblGuido.Size = new System.Drawing.Size(102, 49);
            this.lblGuido.TabIndex = 24;
            this.lblGuido.Text = "GUIDO";
            this.lblGuido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFDO
            // 
            this.lblFDO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblFDO.ForeColor = System.Drawing.Color.White;
            this.lblFDO.Location = new System.Drawing.Point(6, 86);
            this.lblFDO.Name = "lblFDO";
            this.lblFDO.Size = new System.Drawing.Size(102, 49);
            this.lblFDO.TabIndex = 23;
            this.lblFDO.Text = "FDO";
            this.lblFDO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRetro
            // 
            this.lblRetro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblRetro.ForeColor = System.Drawing.Color.White;
            this.lblRetro.Location = new System.Drawing.Point(114, 27);
            this.lblRetro.Name = "lblRetro";
            this.lblRetro.Size = new System.Drawing.Size(102, 49);
            this.lblRetro.TabIndex = 22;
            this.lblRetro.Text = "RETRO";
            this.lblRetro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBooster
            // 
            this.lblBooster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblBooster.ForeColor = System.Drawing.Color.White;
            this.lblBooster.Location = new System.Drawing.Point(6, 27);
            this.lblBooster.Name = "lblBooster";
            this.lblBooster.Size = new System.Drawing.Size(102, 49);
            this.lblBooster.TabIndex = 21;
            this.lblBooster.Text = "BOOSTER";
            this.lblBooster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCountdownReset);
            this.groupBox2.Controls.Add(this.btnCountdownHold);
            this.groupBox2.Controls.Add(this.btnCountdownStart);
            this.groupBox2.Controls.Add(this.nudTime);
            this.groupBox2.Location = new System.Drawing.Point(769, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 85);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "COUNTDOWN TIMER";
            // 
            // btnCountdownReset
            // 
            this.btnCountdownReset.Location = new System.Drawing.Point(179, 24);
            this.btnCountdownReset.Name = "btnCountdownReset";
            this.btnCountdownReset.Size = new System.Drawing.Size(46, 44);
            this.btnCountdownReset.TabIndex = 38;
            this.btnCountdownReset.Text = "Reset";
            this.btnCountdownReset.UseVisualStyleBackColor = true;
            this.btnCountdownReset.Click += new System.EventHandler(this.btnCountdownReset_Click);
            // 
            // btnCountdownHold
            // 
            this.btnCountdownHold.Location = new System.Drawing.Point(128, 24);
            this.btnCountdownHold.Name = "btnCountdownHold";
            this.btnCountdownHold.Size = new System.Drawing.Size(46, 44);
            this.btnCountdownHold.TabIndex = 37;
            this.btnCountdownHold.Text = "Hold";
            this.btnCountdownHold.UseVisualStyleBackColor = true;
            this.btnCountdownHold.Click += new System.EventHandler(this.btnCountdownHold_Click);
            // 
            // btnCountdownStart
            // 
            this.btnCountdownStart.Location = new System.Drawing.Point(76, 24);
            this.btnCountdownStart.Name = "btnCountdownStart";
            this.btnCountdownStart.Size = new System.Drawing.Size(46, 44);
            this.btnCountdownStart.TabIndex = 36;
            this.btnCountdownStart.Text = "Start";
            this.btnCountdownStart.UseVisualStyleBackColor = true;
            this.btnCountdownStart.Click += new System.EventHandler(this.btnCountdownStart_Click);
            // 
            // nudTime
            // 
            this.nudTime.BackColor = System.Drawing.Color.Black;
            this.nudTime.Font = new System.Drawing.Font("Digital-7", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudTime.ForeColor = System.Drawing.Color.Red;
            this.nudTime.Location = new System.Drawing.Point(6, 24);
            this.nudTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudTime.Name = "nudTime";
            this.nudTime.Size = new System.Drawing.Size(64, 44);
            this.nudTime.TabIndex = 22;
            this.nudTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // frmFlightDirector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1028, 570);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmFlightDirector";
            this.Text = "Flight Director";
            this.Controls.SetChildIndex(this.lblTerminal, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.Label lblFAO;
        private System.Windows.Forms.Label lblProcedures;
        private System.Windows.Forms.Label lblINCO;
        private System.Windows.Forms.Label lblControl;
        private System.Windows.Forms.Label lblTelmu;
        private System.Windows.Forms.Label lblGNC;
        private System.Windows.Forms.Label lblEECOM;
        private System.Windows.Forms.Label lblCapcom;
        private System.Windows.Forms.Label lblSurgeon;
        private System.Windows.Forms.Label lblGuido;
        private System.Windows.Forms.Label lblFDO;
        private System.Windows.Forms.Label lblRetro;
        private System.Windows.Forms.Label lblBooster;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCountdownStart;
        private System.Windows.Forms.NumericUpDown nudTime;
        private System.Windows.Forms.Button btnCountdownHold;
        private System.Windows.Forms.Button btnCountdownReset;


    }
}
