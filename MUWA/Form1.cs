using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUWA
{
    public partial class Form1 : Form
    {
        //data
        bool fadeInOrOut = false;
        int mainmenub = 1;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
            roundedCorners(pictureBox1, 20);
            roundedCorners(pictureBox2, 20);
            roundedCorners(pictureBox3, 40);
            roundedCorners(panel3, 40);
            roundedCorners(panel4, 40);
            roundedCorners(label4, 20);
            roundedCorners(label5, 20);
            roundedCorners(label6, 20);
            roundedCorners(label7, 20);
            roundedCorners(panel5, 40);
            roundedCorners(pictureBox4, 20);
        }

        void roundedCorners(Control c, int x)
        {
            c.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, c.Width, c.Height, x, x));
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }

            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            applicationFade.Start();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(60, 60, 60);
            pictureBox1.Image = Properties.Resources.closeselected;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(40, 40, 40);
            pictureBox1.Image = Properties.Resources.closenone;
        }

        private void applicationFade_Tick(object sender, EventArgs e)
        {
            switch(fadeInOrOut)
            {
                case true:
                    this.Opacity -= 0.02;
                    if(this.Opacity == 0)
                    {
                        Application.Exit();
                        this.Close();
                    }
                    break;
                case false:
                    this.Opacity += 0.02;
                    if(this.Opacity == 1)
                    {
                        applicationFade.Stop();
                    }
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            applicationFade.Stop();
            fadeInOrOut = true;
            applicationFade.Start();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(60, 60, 60);
            pictureBox2.Image = Properties.Resources.lineselected;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.FromArgb(40, 40, 40);
            pictureBox2.Image = Properties.Resources.linenone;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            if(mainmenub != 1)
            {
                label1.ForeColor = Color.White;
                label1.BackColor = Color.FromArgb(30, 30, 30);
            }
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if(mainmenub != 1)
            {
                label1.ForeColor = Color.Gray;
                label1.BackColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            if (mainmenub != 2)
            {
                label2.ForeColor = Color.White;
                label2.BackColor = Color.FromArgb(30, 30, 30);
            }
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            if (mainmenub != 2)
            {
                label2.ForeColor = Color.Gray;
                label2.BackColor = Color.FromArgb(40, 40, 40);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if(mainmenub != 1)
            {
                label1.BackColor = Color.FromArgb(30, 30, 30);
                label2.BackColor = Color.FromArgb(40, 40, 40);
                label2.ForeColor = Color.Gray;
                panel1.Visible = true;
                panel2.Visible = false;
                mainmenub = 1;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (mainmenub != 2)
            {
                label2.BackColor = Color.FromArgb(30, 30, 30);
                label1.BackColor = Color.FromArgb(40, 40, 40);
                label1.ForeColor = Color.Gray;
                panel2.Visible = true;
                mainmenub = 2;
                panel1.Visible = false;
            }
        }
    }
}
