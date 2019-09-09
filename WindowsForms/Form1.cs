using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }



        private string weekday(int d, int m, int y)
        {
            string dayName = string.Empty;
            int LeapYears = (int)y / 4;
            long a = (y - LeapYears) * 365 + LeapYears * 366;
            if (m >= 2) a += 31;
            if (m >= 3 && (int)y / 4 == y / 4) a += 29;
            else if (m >= 3) a += 28;
            if (m >= 4) a += 31;
            if (m >= 5) a += 30;
            if (m >= 6) a += 31;
            if (m >= 7) a += 30;
            if (m >= 8) a += 31;
            if (m >= 9) a += 31;
            if (m >= 10) a += 30;
            if (m >= 11) a += 31;
            if (m == 12) a += 30;
            a += d;
            int b = (int)(a - 2) % 7;
            switch (b)
            {
                case 1:
                    dayName = "Monday";
                    break;

                case 2:
                    dayName = "Tuesday";
                    break;

                case 3:
                    dayName = "Wednesday";
                    break;

                case 4:
                    dayName = "Thursday";
                    break;

                case 5:
                    dayName = "Friday";
                    break;

                case 6:
                    dayName = "Saturday";
                    break;

                case 7:
                    dayName = "Sunday";
                    break;
            }
            return dayName;
        }

        private object getCPU()
        {
            var process = Process.GetCurrentProcess();

            // Preparing variable for application instance name
            var name = string.Empty;

            foreach (var instance in new PerformanceCounterCategory("Process").GetInstanceNames())
            {
                if (instance.StartsWith(process.ProcessName))
                {
                    using (var processId = new PerformanceCounter("Process", "ID Process", instance, true))
                    {
                        if (process.Id == (int)processId.RawValue)
                        {
                            name = instance;
                            break;
                        }
                    }
                }
            }

            var cpu = new PerformanceCounter("Process", "% Processor Time", name, true);
            var ram = new PerformanceCounter("Process", "Private Bytes", name, true);

            // Getting first initial values
            cpu.NextValue();
            ram.NextValue();

            // Creating delay to get correct values of CPU usage during next query
            Thread.Sleep(500);

            dynamic result = new ExpandoObject();

            // If system has multiple cores, that should be taken into account
            result.CPU = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);
            // Returns number of MB consumed by application
            result.RAM = Math.Round(ram.NextValue() / 1024 / 1024, 2);

            return result;
        }

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        private string getCurrentCpuUsage()
        {
            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            return cpuCounter.NextValue() + "%";
        }

        private String GetTotalRAM()
        {
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject item in moc)
            {
                return Convert.ToString(Math.Round(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 0) / 1024, 2)) + " GB";
                //return Convert.ToString(Math.Round(Convert.ToDouble(item.Properties["TotalPhysicalMemory"].Value) / 1048576, 0) / 1024) + " MB";
            }

            return "RAMsize";
        }

        private string GetFreeRAM()
        {
            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return (Math.Round(ramCounter.NextValue() / 1024, 2)) + " GB";
        }

        private string GetPercentRAM()
        {
            string percent = string.Empty;
            double total = double.Parse(GetTotalRAM().Replace("GB", "").Trim());
            double use = double.Parse(GetFreeRAM().Replace("GB", "").Trim());
            double per = 100 - (use * 100) / total;
            int result = Convert.ToInt32(per);
            percent = result.ToString() + "%";
            return percent;
        }

        private string FindPercentRAM()
        {
            var wmiObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem");

            var memoryValues = wmiObject.Get().Cast<ManagementObject>().Select(mo => new
            {
                FreePhysicalMemory = Double.Parse(mo["FreePhysicalMemory"].ToString()),
                TotalVisibleMemorySize = Double.Parse(mo["TotalVisibleMemorySize"].ToString())
            }).FirstOrDefault();
            string percent = string.Empty;
            if (memoryValues != null)
            {
                int result = Convert.ToInt32(((memoryValues.TotalVisibleMemorySize - memoryValues.FreePhysicalMemory) / memoryValues.TotalVisibleMemorySize) * 100);
                percent = result + "%";
            }
            return percent;
        }

        private string GetCPUPercent()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                var usage = obj["PercentProcessorTime"];
                var name = obj["Name"];
                result = usage.ToString();
                // Console.WriteLine(name + " : " + usage);
            }
            return result + "%";
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblRamPercent.Text = FindPercentRAM();
            lblRamFree.Text = GetFreeRAM();
            lblCpuPercent.Text = GetCPUPercent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblRamTotal.Text = GetTotalRAM();
            lblRamPercent.Text = FindPercentRAM();
            lblRamFree.Text = GetFreeRAM();
            lblCpuPercent.Text = GetCPUPercent();
            // string Query = "SELECT Capacity FROM Win32_PhysicalMemory";
            // ManagementObjectSearcher searcher = new ManagementObjectSearcher(Query);

            //MessageBox.Show("Ram Total: " + GetTotalRAM() + "\nRam Using: " + GetFreeRAM() + "\nRAM using (%): " + GetPercentRAM() + "\nRAM using (%): " + FindPercentRAM()
            //    + "\nCPU(%) " + GetCPUPercent());
            //MessageBox.Show("Using C# for Date 25 June 2025 is : " + new DateTime(2025, 6, 25).DayOfWeek);
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("videolesson2907@gmail.com", "Davin01"),
                EnableSsl = true
            };
            //client.Send("videolesson2907@gmail.com", "pheaklesson@gmail.com", "test", "Test from c#");
            //MessageBox.Show("Sent");

            /*  Type myType = typeof(Person);

              // Get the fields of the specified class.
              FieldInfo[] myField = myType.GetFields(BindingFlags.Public | BindingFlags.Instance);
              MemberInfo[] mem = myType.GetDefaultMembers();
              //myType.GetDefaultMembers

              Console.WriteLine("\nDisplaying fields that have SpecialName attributes:\n");
              for (int i = 0; i < myField.Length; i++)
              {
                  // Determine whether or not each field is a special name.
                  if (myField[i].IsSpecialName)
                  {
                      //Console.WriteLine("The field {0} has a SpecialName attribute.", myField[i].Name);
                      MessageBox.Show(myField[i].Name);
                  }
              }

              TypeInfo t = typeof(Person).GetTypeInfo();
              IEnumerable<PropertyInfo> pList = t.DeclaredProperties;
              IEnumerable<MethodInfo> mList = t.DeclaredMethods;
              List<string> getFiels = new List<string>();
              foreach (PropertyInfo p in pList)
              {
                  getFiels.Add(p.Name);
              }
              Person per = new Person();

              MessageBox.Show(getFiels + "");*/

            //string data = "";
            //StreamReader reader = new StreamReader(@"C:\WINDOWS\system32");
            //StreamWriter writer = new StreamWriter(@"C:\WINDOWS\system32");
            //IPAddress address = IPAddress.Parse("10.198.1.8");
            //IPEndPoint ipe = new IPEndPoint(address, 23);
            //Socket telnetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //telnetSocket.Connect(ipe);
            //NetworkStream NsStream = new NetworkStream(telnetSocket, true);
            //if (telnetSocket.Connected)
            //{
            //    NsStream = new NetworkStream(telnetSocket, true);
            //    reader = new StreamReader(NsStream);
            //}

            //while (!reader.EndOfStream)
            //{
            //    data = reader.ReadLine();
            //    if (data.Contains("Password:"))
            //    {
            //        //I want to enter password in cmd here
            //    }
            //}
            //reader.Close();
            //if (NsStream == null)
            //    NsStream.Close();

            /*
            //create a new telnet connection to hostname "gobelijn" on port "23"
            TelnetConnection tc = new TelnetConnection("10.10.248.30", 23);

            //login with user "root",password "rootpassword", using a timeout of 100ms,
            //and show server output
            string s = tc.Login("bidc", "bidc@123", 1000);
            Console.Write(s);

            //Console.Write("ping 10.22.7.1");
            // server output should end with "$" or ">", otherwise the connection failed
            string prompt = s.TrimEnd();
            prompt = s.Substring(prompt.Length - 1, 1);
            try
            {
                if (prompt != "$" && prompt != ">") prompt = "";
            }
            catch (Exception xception)
            {
                MessageBox.Show("Connection failed");
            }

            // throw new Exception("Connection failed");

            // while connected
            while (tc.IsConnected || prompt.Trim() != "exit")
            {
                // display server output
                Console.Write(tc.Read());

                // send client input to server
                prompt = Console.ReadLine();
                tc.WriteLine(prompt);

                // display server output
                Console.Write(tc.Read());
            }

            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();

            */
            /*  WebsitesScreenshot.WebsitesScreenshot _Obj;
              _Obj = new WebsitesScreenshot.WebsitesScreenshot();
              WebsitesScreenshot.WebsitesScreenshot.Result _Result;
              System.Drawing.Bitmap _MyBitmap = null;
              _Result = _Obj.CaptureWebpage("https://www.google.com");
              if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
              {
                  //Get captured image in memory
                  _MyBitmap = _Obj.GetImage();
                  _MyBitmap.Save("D:\\test.png");
                  _Obj.Dispose();
              }*/
            /*
          HtmlCapture capture = new HtmlCapture(@"C:\Users\User\Desktop\Document\WindowsForms\test.jpg");
          capture.HtmlImageCapture += new HtmlCapture.HtmlCaptureEvent(capture_HtmlImageCapture);
          capture.Create("https://www.google.com");*/
            /* Image bit = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

             Graphics gs = Graphics.FromImage(bit);

             gs.CopyFromScreen(new Point(0, 0), new Point(0, 0), bit.Size);

             //bit.Save(@"D:\Test.jpg"); // or stream to memory, whichever you prefer

             string url = "https://www.bidc.com.kh/";
             string content = "";

             WebRequest webRequest = WebRequest.Create(url);
             WebResponse webResponse = webRequest.GetResponse();
             StreamReader sr = new StreamReader(webResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-16"));
             content = sr.ReadToEnd();
             //save to file
             //byte[] b = Convert.FromBase64String(content.Replace("-", ""));// GetBytes(content);
             // Convert.FromBase64String(content);
             //MemoryStream ms = new MemoryStream(b);
             //Image img = Image.FromStream(ms);
             //img.Save(@"D:\pic.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
             //img.Dispose();
             //ms.Close();

             //using (Image image = Image.FromStream(new MemoryStream(b)))
             //{
             //    image.Save(@"D:\pic.jpg", ImageFormat.Jpeg);  // Or Png
             //    image.Dispose();
             //}

             //MemoryStream ms = new MemoryStream(b, 0, b.Length);

             //ms.Write(b, 0, b.Length);
             //Image image = Image.FromStream(ms, true);
             //image.Save(@"D:\pic.jpg", ImageFormat.Jpeg);*/
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (BaseForm.Ask_FormClosing(sender, e).Equals(DialogResult.Yes))
            //{
            //    MessageBox.Show("Yes");
            //}
            //else if (BaseForm.Ask_FormClosing(sender, e).Equals(DialogResult.No))
            //{
            //    MessageBox.Show("No");
            //}
            //else
            //{
            //    e.Cancel = true;
            //}


            //if (BaseForm.Yes_FormClosing(sender, e))
            //{
            //    MessageBox.Show("Yes");
            //}
        }

        private void ContextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // textBox1.BorderStyle = BorderStyle.None;
            // Pen p = new Pen(Color.CornflowerBlue);
            //Graphics g = e.Graphics;
            //int variance = 3;
            // g.DrawRectangle(p, new Rectangle(textBox1.Location.X - variance, textBox1.Location.Y - variance, textBox1.Width + variance, textBox1.Height + variance));
        }
    }
}