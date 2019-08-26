using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // BaseForm.SetForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Type myType = typeof(Person);

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

            MessageBox.Show(getFiels + "");

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
            textBox1.BorderStyle = BorderStyle.None;
            Pen p = new Pen(Color.CornflowerBlue);
            Graphics g = e.Graphics;
            int variance = 3;
            g.DrawRectangle(p, new Rectangle(textBox1.Location.X - variance, textBox1.Location.Y - variance, textBox1.Width + variance, textBox1.Height + variance));
        }
    }
}