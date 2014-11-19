using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TelemetryClient
{
    public partial class SetResetButton : UserControl
    {
        public bool Set
        {
            get;
            private set;
        }

        [Browsable(true)]
        public bool Checked
        {
            get
            {
                return chkGo.Checked;
            }
            set
            {
                chkGo.Checked = value;
            }
        }

        [Browsable(true)]
        public Color SetForeColor
        {
            get;
            set;
        }

        [Browsable(true)]
        public Color SetBackColor
        {
            get;
            set;
        }

        [Browsable(true)]
        public new string Text
        {
            get
            {
                return chkGo.Text;
            }
            set
            {
                if (value == null) throw new NullReferenceException();

                chkGo.Text = value;
            }
        }

        public SetResetButton()
        {
            InitializeComponent();
        }

        private void GoButton_Load(object sender, EventArgs e)
        {
            chkGo.Width = this.Width;
            chkGo.Height = this.Height;
            chkGo.ForeColor = ForeColor;
            chkGo.BackColor = BackColor;
        }

        private void GoButton_Resize(object sender, EventArgs e)
        {
            chkGo.Width = this.Width;
            chkGo.Height = this.Height;
        }

        private void chkGo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGo.Checked)
            {
                Set = true;
                chkGo.ForeColor = SetForeColor;
                chkGo.BackColor = SetBackColor;
            }
            else if (Set)
            {
                chkGo.Checked = true;
            }
        }

        private void chkGo_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, new EventArgs());
        }

        private void SetResetButton_ForeColorChanged(object sender, EventArgs e)
        {
            chkGo.ForeColor = ForeColor;
        }

        private void SetResetButton_BackColorChanged(object sender, EventArgs e)
        {
            chkGo.BackColor = BackColor;
        }

        public void ResetState()
        {
            Set = false;
            chkGo.ForeColor = ForeColor;
            chkGo.BackColor = BackColor;
            chkGo.Checked = false;            
        }
    }
}
