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
            roundedCorners(label10, 10);
            roundedCorners(label8, 10);
            roundedCorners(panel7, 10);
            roundedCorners(panel8, 10);
            roundedCorners(panel9, 10);
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

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            label15.ForeColor = Color.White;
        }

        private void label15_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2024")
            {
                label15.ForeColor = Color.White;
            }
            else
            {
                label15.ForeColor = Color.Gray;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            year = "2024";
            changedatelabel();
            label15.ForeColor = Color.White;
        }

        private void label16_MouseEnter(object sender, EventArgs e)
        {
            label16.ForeColor = Color.White;
        }

        private void label16_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2025")
            {
                label16.ForeColor = Color.White;
            }
            else
            {
                label16.ForeColor = Color.Gray;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            year = "2025";
            changedatelabel();
            label16.ForeColor = Color.White;
        }

        private void label17_MouseEnter(object sender, EventArgs e)
        {
            label17.ForeColor = Color.White;
        }

        private void label17_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2026")
            {
                label17.ForeColor = Color.White;
            }
            else
            {
                label17.ForeColor = Color.Gray;
            }
        }

        private void label17_Click(object sender, EventArgs e)
        {
            year = "2026";
            changedatelabel();
            label17.ForeColor = Color.White;
        }

        private void label18_MouseEnter(object sender, EventArgs e)
        {
            label18.ForeColor = Color.White;
        }

        private void label18_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2027")
            {
                label18.ForeColor = Color.White;
            }
            else
            {
                label18.ForeColor = Color.Gray;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            year = "2027";
            changedatelabel();
            label18.ForeColor = Color.White;
        }

        private void label19_MouseEnter(object sender, EventArgs e)
        {
            label19.ForeColor = Color.White;
        }

        private void label19_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2028")
            {
                label19.ForeColor = Color.White;
            }
            else
            {
                label19.ForeColor = Color.Gray;
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            year = "2028";
            changedatelabel();
            label19.ForeColor = Color.White;
        }

        private void label20_MouseEnter(object sender, EventArgs e)
        {
            label20.ForeColor = Color.White;
        }

        private void label20_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2029")
            {
                label20.ForeColor = Color.White;
            }
            else
            {
                label20.ForeColor = Color.Gray;
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            year = "2029";
            changedatelabel();
            label20.ForeColor = Color.White;
        }

        private void label21_MouseEnter(object sender, EventArgs e)
        {
            label21.ForeColor = Color.White;
        }

        private void label21_MouseLeave(object sender, EventArgs e)
        {
            if (year == "2030")
            {
                label21.ForeColor = Color.White;
            }
            else
            {
                label21.ForeColor = Color.Gray;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            year = "2030";
            changedatelabel();
            label21.ForeColor = Color.White;
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (panel8.Visible == false)
            {
                panel8.Visible = true;
                label8.Text = "Month ↓";
            }
            else
            {
                panel8.Visible = false;
                label8.Text = "Month ↑";
            }
        }

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            if (panel8.Visible == false)
            {
                label8.ForeColor = Color.Gray;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (panel9.Visible == false)
            {
                panel9.Visible = true;
                label10.Text = "Month ↓";
            }
            else
            {
                panel9.Visible = false;
                label10.Text = "Month ↑";
            }
        }

        private void label10_MouseEnter(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            if (panel9.Visible == false)
            {
                label10.ForeColor = Color.Gray;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            month = "01";
            changedatelabel1();
            label30.ForeColor = Color.White;
        }
        void changedatelabel1()
        {
            label11.Text = "Applied date: " + year + "." + month + "." + day;
            foreach (Label l in panel8.Controls.OfType<Label>())
            {
                l.ForeColor = Color.Gray;
            }
        }

        private void label31_Click(object sender, EventArgs e)
        {
            month = "xx";
            changedatelabel1();
            label31.ForeColor = Color.White;
        }

        private void label29_Click(object sender, EventArgs e)
        {
            month = "02";
            changedatelabel1();
            label29.ForeColor = Color.White;
        }

        private void label28_Click(object sender, EventArgs e)
        {
            month = "03";
            changedatelabel1();
            label28.ForeColor = Color.White;
        }

        private void label27_Click(object sender, EventArgs e)
        {
            month = "04";
            changedatelabel1();
            label27.ForeColor = Color.White;
        }

        private void label26_Click(object sender, EventArgs e)
        {
            month = "05";
            changedatelabel1();
            label26.ForeColor = Color.White;
        }

        private void label25_Click(object sender, EventArgs e)
        {
            month = "06";
            changedatelabel1();
            label25.ForeColor = Color.White;
        }

        private void label24_Click(object sender, EventArgs e)
        {
            month = "07";
            changedatelabel1();
            label24.ForeColor = Color.White;
        }

        private void label23_Click(object sender, EventArgs e)
        {
            month = "08";
            changedatelabel1();
            label23.ForeColor = Color.White;
        }

        private void label22_Click(object sender, EventArgs e)
        {
            month = "09";
            changedatelabel1();
            label22.ForeColor = Color.White;
        }

        private void label32_Click(object sender, EventArgs e)
        {
            month = "10";
            changedatelabel1();
            label32.ForeColor = Color.White;
        }

        private void label33_Click(object sender, EventArgs e)
        {
            month = "11";
            changedatelabel1();
            label33.ForeColor = Color.White;
        }

        private void label34_Click(object sender, EventArgs e)
        {
            month = "12";
            changedatelabel1();
            label34.ForeColor = Color.White;
        }

        private void label31_MouseEnter(object sender, EventArgs e)
        {
            label31.ForeColor = Color.White;
        }

        private void label30_MouseEnter(object sender, EventArgs e)
        {
            label30.ForeColor = Color.White;
        }

        private void label29_MouseEnter(object sender, EventArgs e)
        {
            label29.ForeColor = Color.White;
        }

        private void label28_MouseEnter(object sender, EventArgs e)
        {
            label28.ForeColor = Color.White;
        }

        private void label27_MouseEnter(object sender, EventArgs e)
        {
            label27.ForeColor = Color.White;
        }

        private void label26_MouseEnter(object sender, EventArgs e)
        {
            label26.ForeColor = Color.White;
        }

        private void label25_MouseEnter(object sender, EventArgs e)
        {
            label25.ForeColor = Color.White;
        }

        private void label24_MouseEnter(object sender, EventArgs e)
        {
            label24.ForeColor = Color.White;
        }

        private void label23_MouseEnter(object sender, EventArgs e)
        {
            label23.ForeColor = Color.White;
        }

        private void label22_MouseEnter(object sender, EventArgs e)
        {
            label22.ForeColor = Color.White;
        }

        private void label32_MouseEnter(object sender, EventArgs e)
        {
            label32.ForeColor = Color.White;
        }

        private void label33_MouseEnter(object sender, EventArgs e)
        {
            label33.ForeColor = Color.White;
        }

        private void label34_MouseEnter(object sender, EventArgs e)
        {
            label34.ForeColor = Color.White;
        }

        private void label31_MouseLeave(object sender, EventArgs e)
        {
            if (month == "xx")
            {
                label31.ForeColor = Color.White;
            }
            else
            {
                label31.ForeColor = Color.Gray;
            }
        }

        private void label30_MouseLeave(object sender, EventArgs e)
        {
            if (month == "01")
            {
                label30.ForeColor = Color.White;
            }
            else
            {
                label30.ForeColor = Color.Gray;
            }
        }

        private void label29_MouseLeave(object sender, EventArgs e)
        {
            if (month == "02")
            {
                label29.ForeColor = Color.White;
            }
            else
            {
                label29.ForeColor = Color.Gray;
            }
        }

        private void label28_MouseLeave(object sender, EventArgs e)
        {
            if (month == "03")
            {
                label28.ForeColor = Color.White;
            }
            else
            {
                label28.ForeColor = Color.Gray;
            }
        }

        private void label27_MouseLeave(object sender, EventArgs e)
        {
            if (month == "04")
            {
                label27.ForeColor = Color.White;
            }
            else
            {
                label27.ForeColor = Color.Gray;
            }
        }

        private void label26_MouseLeave(object sender, EventArgs e)
        {
            if (month == "05")
            {
                label26.ForeColor = Color.White;
            }
            else
            {
                label26.ForeColor = Color.Gray;
            }
        }

        private void label25_MouseLeave(object sender, EventArgs e)
        {
            if (month == "06")
            {
                label25.ForeColor = Color.White;
            }
            else
            {
                label25.ForeColor = Color.Gray;
            }
        }

        private void label24_MouseLeave(object sender, EventArgs e)
        {
            if (month == "07")
            {
                label24.ForeColor = Color.White;
            }
            else
            {
                label24.ForeColor = Color.Gray;
            }
        }

        private void label23_MouseLeave(object sender, EventArgs e)
        {
            if (month == "08")
            {
                label23.ForeColor = Color.White;
            }
            else
            {
                label23.ForeColor = Color.Gray;
            }
        }

        private void label22_MouseLeave(object sender, EventArgs e)
        {
            if (month == "09")
            {
                label22.ForeColor = Color.White;
            }
            else
            {
                label22.ForeColor = Color.Gray;
            }
        }

        private void label32_MouseLeave(object sender, EventArgs e)
        {
            if (month == "10")
            {
                label32.ForeColor = Color.White;
            }
            else
            {
                label32.ForeColor = Color.Gray;
            }
        }

        private void label33_MouseLeave(object sender, EventArgs e)
        {
            if (month == "11")
            {
                label33.ForeColor = Color.White;
            }
            else
            {
                label33.ForeColor = Color.Gray;
            }
        }

        private void label34_MouseLeave(object sender, EventArgs e)
        {
            if (month == "12")
            {
                label34.ForeColor = Color.White;
            }
            else
            {
                label34.ForeColor = Color.Gray;
            }
        }
    }
}
