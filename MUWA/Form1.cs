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
        int i = 0;

        string year = "xxxx";
        string month = "xx";
        string day = "xx";
        string location = "Any";
        string type = "Any";

        string date = "";

        string[] linenrs = new string[1000];
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
            roundedCorners(webBrowser1, 20);
            roundedCorners(webBrowser2, 20);
            roundedCorners(panel6, 40);
            roundedCorners(dataGridView1, 40);
            roundedCorners(panel7, 10);
            roundedCorners(panel10, 10);
            roundedCorners(panel11, 10);
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
            FileDownloader fd = new FileDownloader();
            fd.DownloadFileAsync("https://drive.google.com/file/d/14o452mLGhshdSb5PP2tiOlXIgfYziCFk/view?usp=sharing", @"C:\Temp\muwaIOTD.txt");
            fd.DownloadFileCompleted += (sender1, e1) => applicationFade.Start();
            fd.Dispose();

            applicationFade.Start();

            foreachctrl(panel10, 10);
            foreachctrl(panel7, 10);
            foreachctrl(panel8, 10);
            foreachctrl(panel9, 10);
            foreachctrl(panel11, 10);
            foreachctrl(panel6, 10);

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Date";
            dataGridView1.Columns[1].Name = "Type";
            dataGridView1.Columns[2].Name = "Title";
            dataGridView1.Columns[3].Name = "Location";

            FileDownloader fd6 = new FileDownloader();
            FileDownloader fd3 = new FileDownloader();
            FileDownloader fd4 = new FileDownloader();
            FileDownloader fd5 = new FileDownloader();
            FileDownloader fd7 = new FileDownloader();

            fd6.DownloadFileAsync("https://drive.google.com/file/d/10KkARXjcluugBz8ngC2csjlJARMFN9A3/view?usp=sharing", @"C:\Temp\muwaDATE.txt");
            fd6.DownloadFileCompleted += (sender1, e1) => i += 1;
            fd6.Dispose();

            fd7.DownloadFileAsync("https://drive.google.com/file/d/194i8Af_IXf4YUrtnFCrqDUIpyMXR1qw7/view?usp=sharing", @"C:\Temp\muwaLINK.txt");
            fd7.DownloadFileCompleted += (sender1, e1) => i += 1;
            fd7.Dispose();

            fd3.DownloadFileAsync("https://drive.google.com/file/d/1COi0bakKqacnkVLk5XkWuqm6PdlAscYg/view?usp=sharing", @"C:\Temp\muwaTYPE.txt");
            fd3.DownloadFileCompleted += (sender1, e1) => i += 1;
            fd3.Dispose();

            fd4.DownloadFileAsync("https://drive.google.com/file/d/1CVOY7hhprNV3TtjoVQdupIIxgAFLOSzR/view?usp=sharing", @"C:\Temp\muwaTITLE.txt");
            fd4.DownloadFileCompleted += (sender1, e1) => i += 1;
            fd4.Dispose();

            fd5.DownloadFileAsync("https://drive.google.com/file/d/1_ofDyZ-nxxDMaBgJu-NrSN6Uj08-PSNt/view?usp=sharing", @"C:\Temp\muwaLOC.txt");
            fd5.DownloadFileCompleted += (sender1, e1) => i += 1;
            fd5.Dispose();
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
                    this.Opacity += 0.02;
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
            month = "xx";
            day = "xx";
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
            year = "xxxx";
            month = "xx";
            day = "xx";
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
            year = "xxxx";
            month = "xx";
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
        private void label98_MouseEnter(object sender, EventArgs e)
        {
            label98.ForeColor = Color.White;
        }
        private void label98_MouseLeave(object sender, EventArgs e)
        {
            if (panel11.Visible == false)
            {
                label98.ForeColor = Color.Gray;
            }
        }
        private void label98_Click(object sender, EventArgs e)
        {
            if (panel11.Visible == false)
            {
                panel11.Visible = true;
            }
            else
            {
                panel11.Visible = false;
            }
        }
        private void label70_MouseEnter(object sender, EventArgs e)
        {
            label70.ForeColor = Color.White;
        }

        private void label69_MouseEnter(object sender, EventArgs e)
        {
            label69.ForeColor = Color.White;
        }

        private void label80_MouseEnter(object sender, EventArgs e)
        {
            label80.ForeColor = Color.White;
        }

        private void label71_MouseEnter(object sender, EventArgs e)
        {
            label71.ForeColor = Color.White;
        }

        private void label72_MouseEnter(object sender, EventArgs e)
        {
            label72.ForeColor = Color.White;
        }

        private void label73_MouseEnter(object sender, EventArgs e)
        {
            label73.ForeColor = Color.White;
        }

        private void label74_MouseEnter(object sender, EventArgs e)
        {
            label74.ForeColor = Color.White;
        }

        private void label75_MouseEnter(object sender, EventArgs e)
        {
            label75.ForeColor = Color.White;
        }

        private void label76_MouseEnter(object sender, EventArgs e)
        {
            label76.ForeColor = Color.White;
        }

        private void label77_MouseEnter(object sender, EventArgs e)
        {
            label77.ForeColor = Color.White;
        }

        private void label78_MouseEnter(object sender, EventArgs e)
        {
            label78.ForeColor = Color.White;
        }

        private void label79_MouseEnter(object sender, EventArgs e)
        {
            label79.ForeColor = Color.White;
        }

        private void label90_MouseEnter(object sender, EventArgs e)
        {
            label90.ForeColor = Color.White;
        }

        private void label92_MouseEnter(object sender, EventArgs e)
        {
            label92.ForeColor = Color.White;
        }

        private void label94_MouseEnter(object sender, EventArgs e)
        {
            label94.ForeColor = Color.White;
        }

        private void label96_MouseEnter(object sender, EventArgs e)
        {
            label96.ForeColor = Color.White;
        }

        private void label81_MouseEnter(object sender, EventArgs e)
        {
            label81.ForeColor = Color.White;
        }

        private void label82_MouseEnter(object sender, EventArgs e)
        {
            label82.ForeColor = Color.White;
        }

        private void label83_MouseEnter(object sender, EventArgs e)
        {
            label83.ForeColor = Color.White;
        }

        private void label84_MouseEnter(object sender, EventArgs e)
        {
            label84.ForeColor = Color.White;
        }

        private void label85_MouseEnter(object sender, EventArgs e)
        {
            label85.ForeColor = Color.White;
        }

        private void label86_MouseEnter(object sender, EventArgs e)
        {
            label86.ForeColor = Color.White;
        }

        private void label89_MouseEnter(object sender, EventArgs e)
        {
            label89.ForeColor = Color.White;
        }

        private void label88_MouseEnter(object sender, EventArgs e)
        {
            label88.ForeColor = Color.White;
        }

        private void label87_MouseEnter(object sender, EventArgs e)
        {
            label87.ForeColor = Color.White;
        }

        private void label91_MouseEnter(object sender, EventArgs e)
        {
            label91.ForeColor = Color.White;
        }

        private void label93_MouseEnter(object sender, EventArgs e)
        {
            label93.ForeColor = Color.White;
        }

        private void label95_MouseEnter(object sender, EventArgs e)
        {
            label95.ForeColor = Color.White;
        }

        private void label97_MouseEnter(object sender, EventArgs e)
        {
            label97.ForeColor = Color.White;
        }

        private void label127_MouseEnter(object sender, EventArgs e)
        {
            label127.ForeColor = Color.White;
        }

        private void label126_MouseEnter(object sender, EventArgs e)
        {
            label126.ForeColor = Color.White;
        }

        private void label99_MouseEnter(object sender, EventArgs e)
        {
            label99.ForeColor = Color.White;
        }

        private void label100_MouseEnter(object sender, EventArgs e)
        {
            label100.ForeColor = Color.White;
        }

        private void label101_MouseEnter(object sender, EventArgs e)
        {
            label101.ForeColor = Color.White;
        }

        private void label102_MouseEnter(object sender, EventArgs e)
        {
            label102.ForeColor = Color.White;
        }

        private void label103_MouseEnter(object sender, EventArgs e)
        {
            label103.ForeColor = Color.White;
        }

        private void label104_MouseEnter(object sender, EventArgs e)
        {
            label104.ForeColor = Color.White;
        }

        private void label105_MouseEnter(object sender, EventArgs e)
        {
            label105.ForeColor = Color.White;
        }

        private void label106_MouseEnter(object sender, EventArgs e)
        {
            label106.ForeColor = Color.White;
        }

        private void label70_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Donetsk", label70);
        }

        private void label69_Click(object sender, EventArgs e)
        {
            location = "Any";
            changeloclabel();
            label69.ForeColor = Color.White;
        }
        
        void changeloclabel()
        {
            label68.Text = "Location: " + location;
            foreach (Label l in panel10.Controls.OfType<Label>())
            {
                l.ForeColor = Color.Gray;
            }
        }

        private void label70_Click(object sender, EventArgs e)
        {
            location = "Donetsk";
            changeloclabel();
            label70.ForeColor = Color.White;
        }

        private void label71_Click(object sender, EventArgs e)
        {
            location = "Dnipropetrovsk";
            changeloclabel();
            label71.ForeColor = Color.White;
        }

        private void label72_Click(object sender, EventArgs e)
        {
            location = "Kyiv";
            changeloclabel();
            label72.ForeColor = Color.White;
        }

        private void label73_Click(object sender, EventArgs e)
        {
            location = "Kharkiv";
            changeloclabel();
            label73.ForeColor = Color.White;
        }

        private void label74_Click(object sender, EventArgs e)
        {
            location = "Lviv";
            changeloclabel();
            label74.ForeColor = Color.White;
        }

        private void label75_Click(object sender, EventArgs e)
        {
            location = "Odessa";
            changeloclabel();
            label75.ForeColor = Color.White;
        }

        private void label76_Click(object sender, EventArgs e)
        {
            location = "Luhansk";
            changeloclabel();
            label76.ForeColor = Color.White;
        }

        private void label77_Click(object sender, EventArgs e)
        {
            location = "Crimea";
            changeloclabel();
            label77.ForeColor = Color.White;
        }

        private void label78_Click(object sender, EventArgs e)
        {
            location = "Zaporizhzhia";
            changeloclabel();
            label78.ForeColor = Color.White;
        }

        private void label79_Click(object sender, EventArgs e)
        {
            location = "Vinnytsia";
            changeloclabel();
            label79.ForeColor = Color.White;
        }

        private void label90_Click(object sender, EventArgs e)
        {
            location = "Kherson";
            changeloclabel();
            label90.ForeColor = Color.White;
        }

        private void label92_Click(object sender, EventArgs e)
        {
            location = "Volyn";
            changeloclabel();
            label92.ForeColor = Color.White;
        }

        private void label94_Click(object sender, EventArgs e)
        {
            location = "Chernivtsi";
            changeloclabel();
            label94.ForeColor = Color.White;
        }

        private void label96_Click(object sender, EventArgs e)
        {
            location = "Russia";
            changeloclabel();
            label96.ForeColor = Color.White;
        }

        private void label80_Click(object sender, EventArgs e)
        {
            location = "Poltava";
            changeloclabel();
            label80.ForeColor = Color.White;
        }

        private void label81_Click(object sender, EventArgs e)
        {
            location = "Ivano-Frankivsk";
            changeloclabel();
            label81.ForeColor = Color.White;
        }

        private void label82_Click(object sender, EventArgs e)
        {
            location = "Khmelnytskyi";
            changeloclabel();
            label82.ForeColor = Color.White;
        }

        private void label83_Click(object sender, EventArgs e)
        {
            location = "Zakarpattia";
            changeloclabel();
            label83.ForeColor = Color.White;
        }

        private void label84_Click(object sender, EventArgs e)
        {
            location = "Zhytomyr";
            changeloclabel();
            label84.ForeColor = Color.White;
        }

        private void label85_Click(object sender, EventArgs e)
        {
            location = "Cherkasy";
            changeloclabel();
            label85.ForeColor = Color.White;
        }

        private void label86_Click(object sender, EventArgs e)
        {
            location = "Rivne";
            changeloclabel();
            label86.ForeColor = Color.White;
        }

        private void label89_Click(object sender, EventArgs e)
        {
            location = "Mykolaiv";
            changeloclabel();
            label89.ForeColor = Color.White;
        }

        private void label88_Click(object sender, EventArgs e)
        {
            location = "Sumy";
            changeloclabel();
            label88.ForeColor = Color.White;
        }

        private void label87_Click(object sender, EventArgs e)
        {
            location = "Ternopil";
            changeloclabel();
            label87.ForeColor = Color.White;
        }

        private void label91_Click(object sender, EventArgs e)
        {
            location = "Chernihiv";
            changeloclabel();
            label91.ForeColor = Color.White;
        }

        private void label93_Click(object sender, EventArgs e)
        {
            location = "Kirovohrad";
            changeloclabel();
            label93.ForeColor = Color.White;
        }

        private void label95_Click(object sender, EventArgs e)
        {
            location = "Sevastopol";
            changeloclabel();
            label95.ForeColor = Color.White;
        }

        private void label97_Click(object sender, EventArgs e)
        {
            location = "Unknown location";
            changeloclabel();
            label97.ForeColor = Color.White;
        }

        private void label69_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Any", label69);
        }

        void checkloc(string d, Control c)
        {
            if (location == d)
            {
                c.ForeColor = Color.White;
            }
            else
            {
                c.ForeColor = Color.Gray;
            }
        }

        private void label71_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Dnipropetrovsk", label71);
        }

        private void label72_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Kyiv", label72);
        }

        private void label80_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Poltava", label80);
        }

        private void label81_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Ivano-Frankivsk", label81);
        }

        private void label82_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Khmelnytskyi", label82);
        }

        private void label73_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Kharkiv", label73);
        }

        private void label83_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Zakarpattia", label83);
        }

        private void label74_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Lviv", label74);
        }

        private void label84_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Zhytomyr", label84);
        }

        private void label75_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Odessa", label75);
        }

        private void label85_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Cherkasy", label85);
        }

        private void label76_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Luhansk", label76);
        }

        private void label86_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Rivne", label86);
        }

        private void label77_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Crimea", label77);
        }

        private void label89_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Mykolaiv", label89);
        }

        private void label78_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Zaporizhzhia", label78);
        }

        private void label88_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Sumy", label88);
        }

        private void label79_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Vinnytsia", label79);
        }

        private void label87_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Ternopil", label87);
        }

        private void label90_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Kherson", label90);
        }

        private void label91_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Chernihiv", label91);
        }

        private void label92_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Volyn", label92);
        }

        private void label93_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Kirovohrad", label93);
        }

        private void label94_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Chernivtsi", label94);
        }

        private void label95_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Sevastopol", label95);
        }

        private void label96_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Russia", label96);
        }

        private void label97_MouseLeave(object sender, EventArgs e)
        {
            checkloc("Unknown location", label97);
        }

        private void label127_Click(object sender, EventArgs e)
        {
            type = "Any";
            changetypelabel();
            label127.ForeColor = Color.White;
        }

        void checktype(string d, Control c)
        {
            if (type == d)
            {
                c.ForeColor = Color.White;
            }
            else
            {
                c.ForeColor = Color.Gray;
            }
        }

        private void label126_Click(object sender, EventArgs e)
        {
            type = "Russian losses";
            changetypelabel();
            label126.ForeColor = Color.White;
        }

        void changetypelabel()
        {
            label107.Text = "Type: " + type;
            foreach (Label l in panel11.Controls.OfType<Label>())
            {
                l.ForeColor = Color.Gray;
            }
        }

        private void label99_Click(object sender, EventArgs e)
        {
            type = "Russian forces";
            changetypelabel();
            label99.ForeColor = Color.White;
        }

        private void label100_Click(object sender, EventArgs e)
        {
            type = "Ukrainian losses";
            changetypelabel();
            label100.ForeColor = Color.White;
        }

        private void label101_Click(object sender, EventArgs e)
        {
            type = "Ukrainian forces";
            changetypelabel();
            label101.ForeColor = Color.White;
        }

        private void label102_Click(object sender, EventArgs e)
        {
            type = "Civilian losses";
            changetypelabel();
            label102.ForeColor = Color.White;
        }

        private void label103_Click(object sender, EventArgs e)
        {
            type = "Aftermath footage";
            changetypelabel();
            label103.ForeColor = Color.White;
        }

        private void label104_Click(object sender, EventArgs e)
        {
            type = "Warcrimes";
            changetypelabel();
            label104.ForeColor = Color.White;
        }

        private void label105_Click(object sender, EventArgs e)
        {
            type = "Misc. footage";
            changetypelabel();
            label105.ForeColor = Color.White;
        }

        private void label106_Click(object sender, EventArgs e)
        {
            type = "Combat footage";
            changetypelabel();
            label106.ForeColor = Color.White;
        }

        private void label127_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Any", label127);
        }

        void checktypelabel(string d, Control c)
        {
            if (type == d)
            {
                c.ForeColor = Color.White;
            }
            else
            {
                c.ForeColor = Color.Gray;
            }
        }

        private void label126_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Russian losses", label126);
        }

        private void label99_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Russian forces", label99);
        }

        private void label100_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Ukrainian losses", label100);
        }

        private void label101_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Ukrainian forces", label101);
        }

        private void label102_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Civilian losses", label102);
        }

        private void label103_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Aftermath footage", label103);
        }

        private void label104_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Warcrimes", label104);
        }

        private void label105_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Misc. footage", label105);
        }

        private void label106_MouseLeave(object sender, EventArgs e)
        {
            checktypelabel("Combat footage", label106);
        }

        private void label108_MouseEnter(object sender, EventArgs e)
        {
            label108.ForeColor = Color.White;
        }

        private void label108_MouseLeave(object sender, EventArgs e)
        {
            label108.ForeColor = Color.Gray;
        }

        private void label109_MouseEnter(object sender, EventArgs e)
        {
            label109.ForeColor = Color.White;
        }

        private void label109_MouseLeave(object sender, EventArgs e)
        {
            label109.ForeColor = Color.Gray;
        }

        private void label108_Click(object sender, EventArgs e)
        {
            date = year + "." + month + "." + day;

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            if(i == 5)
            {
                updatedatagrid();
            }
        }

        void updatedatagrid()
        {
            string[] muwaDATE = File.ReadAllLines(@"C:\Temp\muwaDATE.txt");
            string[] muwaTYPE = File.ReadAllLines(@"C:\Temp\muwaTYPE.txt");
            string[] muwaTITLE = File.ReadAllLines(@"C:\Temp\muwaTITLE.txt");
            string[] muwaLOC = File.ReadAllLines(@"C:\Temp\muwaLOC.txt");

            for (int i = muwaDATE.Length; i > 0; i--)
            {
                string[] row =
                {
                    muwaDATE[i - 1],
                    muwaTYPE[i - 1],
                    muwaTITLE[i - 1],
                    muwaLOC[i - 1]
                };

                if(row[0] == date || date == "xxxx.xx.xx")
                {
                    if(row[1] == type || type == "Any")
                    {
                        if(row[3] == location || location == "Any")
                        {
                            dataGridView1.Rows.Add(row);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] muwaLINK = File.ReadAllLines(@"C:\Temp\muwaLINK.txt");

            System.Uri uri2 = new System.Uri(muwaLINK[e.RowIndex + 1]);
            webBrowser2.Url = uri2;
        }
    }
}
