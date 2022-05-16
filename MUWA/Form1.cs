﻿using System;
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
            roundedCorners(label68, 10);
            roundedCorners(label67, 10);
            roundedCorners(label8, 10);
            roundedCorners(panel7, 10);
            roundedCorners(panel10, 10);
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
            foreachctrl(panel10, 10);
            foreachctrl(panel7, 10);
            foreachctrl(panel8, 10);
            foreachctrl(panel9, 10);
        }

        void foreachctrl(Control c1, int x)
        {
            foreach(Label l in c1.Controls.OfType<Label>())
            {
                roundedCorners(l, x);
            }
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
            }
            else
            {
                panel7.Visible = false;
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
            label11.Text = "Date: " + year + "." + month + "." + day;
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
            }
            else
            {
                panel8.Visible = false;
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
            }
            else
            {
                panel9.Visible = false;
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
            label11.Text = "Date: " + year + "." + month + "." + day;
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
            monthmouseleave("xx", label31);
        }
        private void label30_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("01", label30);
        }
        private void label29_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("02", label29);
        }
        private void label28_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("03", label28);
        }
        private void label27_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("04", label27);
        }
        private void label26_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("05", label26);
        }
        private void label25_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("06", label25);
        }
        private void label24_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("07", label24);
        }
        private void label23_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("08", label23);
        }
        private void label22_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("09", label22);
        }
        private void label32_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("10", label32);
        }
        private void label33_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("11", label33);
        }
        private void label34_MouseLeave(object sender, EventArgs e)
        {
            monthmouseleave("12", label34);
        }
        void monthmouseleave(string m, Control c)
        {
            if(month == m)
            {
                c.ForeColor = Color.White;
            }
            else
            {
                c.ForeColor = Color.Gray;
            }
        }
        private void label47_Click(object sender, EventArgs e)
        {
            day = "xx";
            changedatelabel2();
            label47.ForeColor = Color.White;
        }
        void changedatelabel2()
        {
            label11.Text = "Date: " + year + "." + month + "." + day;
            foreach (Label l in panel9.Controls.OfType<Label>())
            {
                l.ForeColor = Color.Gray;
            }
        }
        private void label46_Click(object sender, EventArgs e)
        {
            day = "01";
            changedatelabel2();
            label46.ForeColor = Color.White;
        }
        private void label35_Click(object sender, EventArgs e)
        {
            day = "02";
            changedatelabel2();
            label35.ForeColor = Color.White;
        }
        private void label36_Click(object sender, EventArgs e)
        {
            day = "03";
            changedatelabel2();
            label36.ForeColor = Color.White;
        }
        private void label37_Click(object sender, EventArgs e)
        {
            day = "04";
            changedatelabel2();
            label37.ForeColor = Color.White;
        }
        private void label43_Click(object sender, EventArgs e)
        {
            day = "05";
            changedatelabel2();
            label43.ForeColor = Color.White;
        }
        private void label42_Click(object sender, EventArgs e)
        {
            day = "06";
            changedatelabel2();
            label42.ForeColor = Color.White;
        }
        private void label38_Click(object sender, EventArgs e)
        {
            day = "07";
            changedatelabel2();
            label38.ForeColor = Color.White;
        }
        private void label49_Click(object sender, EventArgs e)
        {
            day = "08";
            changedatelabel2();
            label49.ForeColor = Color.White;
        }
        private void label48_Click(object sender, EventArgs e)
        {
            day = "09";
            changedatelabel2();
            label48.ForeColor = Color.White;
        }
        private void label39_Click(object sender, EventArgs e)
        {
            day = "10";
            changedatelabel2();
            label39.ForeColor = Color.White;
        }
        private void label45_Click(object sender, EventArgs e)
        {
            day = "11";
            changedatelabel2();
            label45.ForeColor = Color.White;
        }
        private void label44_Click(object sender, EventArgs e)
        {
            day = "12";
            changedatelabel2();
            label44.ForeColor = Color.White;
        }
        private void label40_Click(object sender, EventArgs e)
        {
            day = "13";
            changedatelabel2();
            label40.ForeColor = Color.White;
        }
        private void label53_Click(object sender, EventArgs e)
        {
            day = "14";
            changedatelabel2();
            label53.ForeColor = Color.White;
        }
        private void label52_Click(object sender, EventArgs e)
        {
            day = "15";
            changedatelabel2();
            label52.ForeColor = Color.White;
        }
        private void label41_Click(object sender, EventArgs e)
        {
            day = "16";
            changedatelabel2();
            label41.ForeColor = Color.White;
        }
        private void label51_Click(object sender, EventArgs e)
        {
            day = "17";
            changedatelabel2();
            label51.ForeColor = Color.White;
        }
        private void label50_Click(object sender, EventArgs e)
        {
            day = "18";
            changedatelabel2();
            label50.ForeColor = Color.White;
        }
        private void label62_Click(object sender, EventArgs e)
        {
            day = "19";
            changedatelabel2();
            label62.ForeColor = Color.White;
        }
        private void label59_Click(object sender, EventArgs e)
        {
            day = "20";
            changedatelabel2();
            label59.ForeColor = Color.White;
        }
        private void label58_Click(object sender, EventArgs e)
        {
            day = "21";
            changedatelabel2();
            label58.ForeColor = Color.White;
        }
        private void label61_Click(object sender, EventArgs e)
        {
            day = "22";
            changedatelabel2();
            label61.ForeColor = Color.White;
        }
        private void label57_Click(object sender, EventArgs e)
        {
            day = "23";
            changedatelabel2();
            label57.ForeColor = Color.White;
        }
        private void label56_Click(object sender, EventArgs e)
        {
            day = "24";
            changedatelabel2();
            label56.ForeColor = Color.White;
        }
        private void label60_Click(object sender, EventArgs e)
        {
            day = "25";
            changedatelabel2();
            label60.ForeColor = Color.White;
        }
        private void label55_Click(object sender, EventArgs e)
        {
            day = "26";
            changedatelabel2();
            label55.ForeColor = Color.White;
        }
        private void label54_Click(object sender, EventArgs e)
        {
            day = "27";
            changedatelabel2();
            label54.ForeColor = Color.White;
        }
        private void label65_Click(object sender, EventArgs e)
        {
            day = "28";
            changedatelabel2();
            label65.ForeColor = Color.White;
        }
        private void label64_Click(object sender, EventArgs e)
        {
            day = "29";
            changedatelabel2();
            label64.ForeColor = Color.White;
        }
        private void label63_Click(object sender, EventArgs e)
        {
            day = "30";
            changedatelabel2();
            label63.ForeColor = Color.White;
        }
        private void label66_Click(object sender, EventArgs e)
        {
            day = "31";
            changedatelabel2();
            label66.ForeColor = Color.White;
        }
        private void label46_MouseEnter(object sender, EventArgs e)
        {
            label46.ForeColor = Color.White;
        }
        private void label47_MouseEnter(object sender, EventArgs e)
        {
            label47.ForeColor = Color.White;
        }
        private void label35_MouseEnter(object sender, EventArgs e)
        {
            label35.ForeColor = Color.White;
        }
        private void label36_MouseEnter(object sender, EventArgs e)
        {
            label36.ForeColor = Color.White;
        }
        private void label37_MouseEnter(object sender, EventArgs e)
        {
            label37.ForeColor = Color.White;
        }
        private void label43_MouseEnter(object sender, EventArgs e)
        {
            label43.ForeColor = Color.White;
        }
        private void label42_MouseEnter(object sender, EventArgs e)
        {
            label42.ForeColor = Color.White;
        }
        private void label38_MouseEnter(object sender, EventArgs e)
        {
            label38.ForeColor = Color.White;
        }
        private void label49_MouseEnter(object sender, EventArgs e)
        {
            label49.ForeColor = Color.White;
        }
        private void label48_MouseEnter(object sender, EventArgs e)
        {
            label48.ForeColor = Color.White;
        }
        private void label39_MouseEnter(object sender, EventArgs e)
        {
            label39.ForeColor = Color.White;
        }
        private void label45_MouseEnter(object sender, EventArgs e)
        {
            label45.ForeColor = Color.White;
        }
        private void label44_MouseEnter(object sender, EventArgs e)
        {
            label44.ForeColor = Color.White;
        }
        private void label40_MouseEnter(object sender, EventArgs e)
        {
            label40.ForeColor = Color.White;
        }
        private void label53_MouseEnter(object sender, EventArgs e)
        {
            label53.ForeColor = Color.White;
        }
        private void label52_MouseEnter(object sender, EventArgs e)
        {
            label52.ForeColor = Color.White;
        }
        private void label41_MouseEnter(object sender, EventArgs e)
        {
            label41.ForeColor = Color.White;
        }
        private void label51_MouseEnter(object sender, EventArgs e)
        {
            label51.ForeColor = Color.White;
        }
        private void label50_MouseEnter(object sender, EventArgs e)
        {
            label50.ForeColor = Color.White;
        }
        private void label62_MouseEnter(object sender, EventArgs e)
        {
            label62.ForeColor = Color.White;
        }
        private void label59_MouseEnter(object sender, EventArgs e)
        {
            label59.ForeColor = Color.White;
        }
        private void label58_MouseEnter(object sender, EventArgs e)
        {
            label58.ForeColor = Color.White;
        }
        private void label61_MouseEnter(object sender, EventArgs e)
        {
            label61.ForeColor = Color.White;
        }
        private void label57_MouseEnter(object sender, EventArgs e)
        {
            label57.ForeColor = Color.White;
        }
        private void label56_MouseEnter(object sender, EventArgs e)
        {
            label56.ForeColor = Color.White;
        }
        private void label60_MouseEnter(object sender, EventArgs e)
        {
            label60.ForeColor = Color.White;
        }
        private void label55_MouseEnter(object sender, EventArgs e)
        {
            label55.ForeColor = Color.White;
        }
        private void label54_MouseEnter(object sender, EventArgs e)
        {
            label54.ForeColor = Color.White;
        }
        private void label65_MouseEnter(object sender, EventArgs e)
        {
            label65.ForeColor = Color.White;
        }
        private void label64_MouseEnter(object sender, EventArgs e)
        {
            label64.ForeColor = Color.White;
        }
        private void label63_MouseEnter(object sender, EventArgs e)
        {
            label63.ForeColor = Color.White;
        }
        private void label66_MouseEnter(object sender, EventArgs e)
        {
            label66.ForeColor = Color.White;
        }
        private void label51_MouseEnter_1(object sender, EventArgs e)
        {
            label51.ForeColor = Color.White;
        }
        void checkday(string d, Control c)
        {
            if (day == d)
            {
                c.ForeColor = Color.White;
            }
            else
            {
                c.ForeColor = Color.Gray;
            }
        }
        private void label47_MouseLeave(object sender, EventArgs e)
        {
            checkday("xx", label47);
        } 
        private void label46_MouseLeave(object sender, EventArgs e)
        {
            checkday("01", label46);
        }
        private void label35_MouseLeave(object sender, EventArgs e)
        {
            checkday("02", label35);
        }
        private void label36_MouseLeave(object sender, EventArgs e)
        {
            checkday("03", label36);
        }
        private void label37_MouseLeave(object sender, EventArgs e)
        {
            checkday("04", label37);
        }
        private void label43_MouseLeave(object sender, EventArgs e)
        {
            checkday("05", label43);
        }
        private void label42_MouseLeave(object sender, EventArgs e)
        {
            checkday("06", label42);
        }
        private void label38_MouseLeave(object sender, EventArgs e)
        {
            checkday("07", label38);
        }
        private void label49_MouseLeave(object sender, EventArgs e)
        {
            checkday("08", label49);
        }
        private void label48_MouseLeave(object sender, EventArgs e)
        {
            checkday("09", label48);
        }
        private void label39_MouseLeave(object sender, EventArgs e)
        {
            checkday("10", label39);
        }
        private void label45_MouseLeave(object sender, EventArgs e)
        {
            checkday("11", label45);
        }
        private void label44_MouseLeave(object sender, EventArgs e)
        {
            checkday("12", label44);
        }
        private void label40_MouseLeave(object sender, EventArgs e)
        {
            checkday("13", label40);
        }
        private void label53_MouseLeave(object sender, EventArgs e)
        {
            checkday("14", label53);
        }
        private void label52_MouseLeave(object sender, EventArgs e)
        {
            checkday("15", label52);
        }
        private void label41_MouseLeave(object sender, EventArgs e)
        {
            checkday("16", label41);
        }
        private void label51_MouseLeave(object sender, EventArgs e)
        {
            checkday("17", label51);
        }
        private void label50_MouseLeave(object sender, EventArgs e)
        {
            checkday("18", label50);
        }
        private void label62_MouseLeave(object sender, EventArgs e)
        {
            checkday("19", label62);
        }
        private void label59_MouseLeave(object sender, EventArgs e)
        {
            checkday("20", label59);
        }
        private void label58_MouseLeave(object sender, EventArgs e)
        {
            checkday("21", label58);
        }
        private void label61_MouseLeave(object sender, EventArgs e)
        {
            checkday("22", label61);
        }
        private void label57_MouseLeave(object sender, EventArgs e)
        {
            checkday("23", label57);
        }
        private void label56_MouseLeave(object sender, EventArgs e)
        {
            checkday("24", label56);
        }
        private void label60_MouseLeave(object sender, EventArgs e)
        {
            checkday("25", label60);
        }
        private void label55_MouseLeave(object sender, EventArgs e)
        {
            checkday("26", label55);
        }
        private void label54_MouseLeave(object sender, EventArgs e)
        {
            checkday("27", label54);
        }
        private void label65_MouseLeave(object sender, EventArgs e)
        {
            checkday("28", label65);
        }
        private void label64_MouseLeave(object sender, EventArgs e)
        {
            checkday("29", label64);
        }
        private void label63_MouseLeave(object sender, EventArgs e)
        {
            checkday("30", label63);
        }
        private void label66_MouseLeave(object sender, EventArgs e)
        {
            checkday("31", label66);
        }
        private void label67_MouseEnter(object sender, EventArgs e)
        {
            label67.ForeColor = Color.White;
        }

        private void label67_Click(object sender, EventArgs e)
        {
            if (panel10.Visible == false)
            {
                panel10.Visible = true;
            }
            else
            {
                panel10.Visible = false;
            }
        }

        private void label67_MouseLeave(object sender, EventArgs e)
        {
            if (panel10.Visible == false)
            {
                label67.ForeColor = Color.Gray;
            }
        }
    }
}
