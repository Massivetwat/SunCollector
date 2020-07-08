using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using AutoItX3Lib;

namespace SunCollector
{
    public partial class Form1 : Form
    {
        public static string WINDOW_NAME = "Plants vs. Zombies";

        public static IntPtr handle = FindWindow(null, WINDOW_NAME);


        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string IpClassName, string IpWindowName);

        public static RECT rect;
        public struct RECT
        {
            public int left, top, right, bottom;
        }

        
        


        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vkey);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeConsole();

        public int x = 0;
        public int y = 0;

        public static int SunColor = 0xFEF601;
        public static int MoneyColor = 0xECECEC;

        Object faggot;
        public bool doWork = true;

        object[] pixelCoord;

        AutoItX3 au3 = new AutoItX3();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            AllocConsole();
            backgroundWorker1.RunWorkerAsync();

            GetWindowRect(handle, out rect);
        }

        private void sunSearch()
        {
             
            try {
                faggot = au3.PixelSearch(rect.left, rect.bottom, rect.right, rect.top + 120, SunColor);

                if (faggot.ToString() != "1")
                {
                    pixelCoord = (object[])faggot;
                    x = (int)pixelCoord[0];
                    y = (int)pixelCoord[1];
                    au3.MouseMove(x, y, 1);
                    Console.Clear();
                    Console.WriteLine("Found sun!");
                    Console.WriteLine(" x = "+x.ToString()+ " y = " + y.ToString());
                    au3.MouseClick("LEFT");
                    au3.MouseClick("LEFT");



                }
                
                    Thread.Sleep(50);
            } catch  

            {
                Console.Clear();
                Console.WriteLine("Looking for sun..");
                Thread.Sleep(50);
                
            }

            Thread.Sleep(50);



        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (doWork == true)
            {
                if (GetAsyncKeyState(Keys.XButton2)<0)
                {
                    sunSearch();
                    Thread.Sleep(5);
                }
                Thread.Sleep(5);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sunSearch();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GetWindowRect(handle, out rect);
            MessageBox.Show(rect.left.ToString(), rect.top.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doWork = false;
            Application.Exit();
        }
    }
}
