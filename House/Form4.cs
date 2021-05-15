using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace House
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            timerForm();
        }
        private void Wait(double seconds)
        {
            int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            while (System.Environment.TickCount < ticks)
            {
                Application.DoEvents();
            }
        }
        public void timerForm()
        {
            Show();
            while (this.Opacity > 0)
            {
                Wait(0.045);
                this.Opacity -= 0.01;
           
                if (this.Opacity == 0)
                {
                    Close();
                }
            }

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
