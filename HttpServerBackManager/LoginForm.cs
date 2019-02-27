using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpServerBackManager
{
    public partial class LoginForm : Form
    {
        Timer Tick;
        bool isRequestDone = false;
        string reString;
        public LoginForm()
        {
            InitializeComponent();
            Tick = new Timer();
            Tick.Tick += TickEvent;
            Tick.Interval = 500;
            Tick.Start();
        }

        void TickEvent(object sender, EventArgs e)
        {
            if(isRequestDone)
            {
                if(reString == "OK")
                {
                    this.Close();
                }
                isRequestDone = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userName = textBox1.Text;
            var password = textBox2.Text;
            HttpRequestManager.GetInstance().SendHttpRequest("http://192.168.50.53:5656?method=adminLogin&userName=" + userName + "&password=" + password,
                "POST",LoginCallback);
        }

        private void LoginCallback(string re)
        {
           // if(re == "OK")
            {
                reString = re;
                isRequestDone = true;
            }
        }
    }
}
