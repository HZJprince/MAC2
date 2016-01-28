using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MAC2
{
    public partial class 中华MACIP获取程序 : Form
    {
        public 中华MACIP获取程序()
        {
            InitializeComponent();
        }

        public static List<string> GetMacByIPConfig()
        {
            List<string> macs = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            Process p = Process.Start(startInfo);
            //截取输出流
            StreamReader reader = p.StandardOutput;
            string line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Trim();

                    if (line.StartsWith("物理地址") || line.StartsWith("Physical Address"))
                    {
                        macs.Add(line);
                    }
                }

                line = reader.ReadLine();
            }

            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            reader.Close();

            return macs;
        }


        public static List<string> GetIPByIPConfig()
        {
            List<string> ip = new List<string>();
            ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            Process p = Process.Start(startInfo);
            //截取输出流
            StreamReader reader = p.StandardOutput;
            string line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Trim();

                    if (line.Contains("44"))
                    {
                        ip.Add(line);
                    }
                }
                line = reader.ReadLine();
            }
            line = "ip:获取不到中华内部地";
            ip.Add(line);
            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            reader.Close();

            return ip;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> mac = 中华MACIP获取程序.GetMacByIPConfig();
            List<string> ip = 中华MACIP获取程序.GetIPByIPConfig();
            string[] arymac = (string[])mac[0].Split(':');
            string[] aryip = (string[])ip[0].Split(':');
            textBox1.Text = arymac[1];
            textBox2.Text = aryip[1];
        }
    }
}
