namespace TelemetryClient.Forms
{
    partial class frmPAO
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
            this.txtFlightEvents = new System.Windows.Forms.TextBox();
            this.txtCommentary = new System.Windows.Forms.TextBox();
            this.btnAppend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtFlightEvents
            // 
            this.txtFlightEvents.BackColor = System.Drawing.Color.Black;
            this.txtFlightEvents.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFlightEvents.ForeColor = System.Drawing.Color.LightGray;
            this.txtFlightEvents.Location = new System.Drawing.Point(535, 72);
            this.txtFlightEvents.Multiline = true;
            this.txtFlightEvents.Name = "txtFlightEvents";
            this.txtFlightEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFlightEvents.Size = new System.Drawing.Size(535, 491);
            this.txtFlightEvents.TabIndex = 6;
            this.txtFlightEvents.Text = "Flight Events";
            // 
            // txtCommentary
            // 
            this.txtCommentary.BackColor = System.Drawing.Color.Black;
            this.txtCommentary.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentary.ForeColor = System.Drawing.Color.LightGray;
            this.txtCommentary.Location = new System.Drawing.Point(12, 569);
            this.txtCommentary.Multiline = true;
            this.txtCommentary.Name = "txtCommentary";
            this.txtCommentary.Size = new System.Drawing.Size(928, 61);
            this.txtCommentary.TabIndex = 7;
            this.txtCommentary.Text = "Write Commentary Here";
            this.txtCommentary.Click += new System.EventHandler(this.txtCommentary_Click);
            this.txtCommentary.TextChanged += new System.EventHandler(this.txtCommentary_TextChanged);
            // 
            // btnAppend
            // 
            this.btnAppend.Location = new System.Drawing.Point(946, 569);
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.Size = new System.Drawing.Size(105, 60);
            this.btnAppend.TabIndex = 8;
            this.btnAppend.Text = "Append Commentary";
            this.btnAppend.UseVisualStyleBackColor = true;
            this.btnAppend.Click += new System.EventHandler(this.btnAppend_Click);
            // 
            // frmPAO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1078, 636);
            this.Controls.Add(this.btnAppend);
            this.Controls.Add(this.txtCommentary);
            this.Controls.Add(this.txtFlightEvents);
            this.Name = "frmPAO";
            this.Text = "Public Affairs Officer";
            this.Controls.SetChildIndex(this.lblTerminal, 0);
            this.Controls.SetChildIndex(this.txtFlightEvents, 0);
            this.Controls.SetChildIndex(this.txtCommentary, 0);
            this.Controls.SetChildIndex(this.btnAppend, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFlightEvents;
        private System.Windows.Forms.TextBox txtCommentary;
        private System.Windows.Forms.Button btnAppend;
    }
}
