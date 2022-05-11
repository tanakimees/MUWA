using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUWA
{
    public partial class Form1 : Form
    {
        //data
        bool fadeInOrOut = false;
        int mainmenub = 1;
        string year = "xxxx";
        string month = "xx";
        string day = "xx";

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
            roundedCorners(label3, 20);
            roundedCorners(label5, 20);
            roundedCorners(label6, 20);
            roundedCorners(label7, 20);
            roundedCorners(panel5, 40);
            roundedCorners(webBrowser1, 20);
            roundedCorners(panel6, 40);
            roundedCorners(label9, 10);
            roundedCorners(label11, 10);
            roundedCorners(panel7, 10);
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
            FileDownloader fd = new FileDownloader();
            fd.DownloadFileAsync("https://drive.google.com/file/d/14o452mLGhshdSb5PP2tiOlXIgfYziCFk/view?usp=sharing", @"C:\Temp\muwaIOTD.txt");
            fd.Dispose();
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
                    this.Opacity += 0.005;
                    if(this.Opacity == 1)
                    {
                        string[] IOTD = File.ReadAllLines(@"C:\Temp\muwaIOTD.txt");
                        label7.Text = IOTD[1] + " Image of the day." + Environment.NewLine + Environment.NewLine + IOTD[2];
                        label6.Text = "As of " + IOTD[1] + ", there are " + IOTD[3] + " files stored in the archive, they are " + IOTD[4] + "GB in size." + Environment.NewLine + Environment.NewLine + "Approx. " + IOTD[5] + " coffes drinked while developing this application." + Environment.NewLine + Environment.NewLine + "Approx. " + IOTD[6] + " hours have been put into developing this application.";
                        System.Uri uri1 = new System.Uri(IOTD[0]);
                        webBrowser1.Url = uri1;
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

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            if(panel7.Visible == false)
            {
                label9.ForeColor = Color.Gray;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if(panel7.Visible == false)
            {
                panel7.Visible = true;
                label9.Text = "Year ↓";
            }
            else
            {
                panel7.Visible = false;
                label9.Text = "Year ↑";
            }
        }

        private void label12_MouseEnter(object sender, EventArgs e)
        {
            label12.ForeColor = Color.White;
        }

        private void label12_MouseLeave(object sender, EventArgs e)
        {
            if(year == "xxxx")
            {
                label12.ForeColor = Color.White;
            }
            else
            {
                label12.ForeColor = Color.Gray;
            }
        }

        void changedatelabel()
        {
            label11.Text = "Applied date: " + year + "." + month + "." + day;
            foreach(Label l in panel7.Controls.OfType<Label>())
            {
                l.ForeColor = Color.Gray;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            year = "xxxx";
            changedatelabel();
            label12.ForeColor = Color.White;
        }

        private void label13_MouseEnter(object sender, EventArgs e)
        {
            label13.ForeColor = Color.White;
        }

        private void label13_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2022")
            {
                label13.ForeColor = Color.White;
            }
            else
            {
                label13.ForeColor = Color.Gray;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            year = "2022";
            changedatelabel();
            label13.ForeColor = Color.White;
        }

        private void label14_MouseEnter(object sender, EventArgs e)
        {
            label14.ForeColor = Color.White;
        }

        private void label14_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2023")
            {
                label14.ForeColor = Color.White;
            }
            else
            {
                label14.ForeColor = Color.Gray;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            year = "2023";
            changedatelabel();
            label14.ForeColor = Color.White;
        }
    }
}
