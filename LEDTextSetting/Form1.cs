using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using httpHelper;
using fastJSON;

namespace LEDTextSetting
{
    delegate void deleControlInvoke(object o);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.lblStatus.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.txtIP.Text == null || this.txtIP.Text == string.Empty)
            {
                MessageBox.Show("必须填写读写器IP地址!", "异常提示");
                return;
            }
            else
            {
                try
                {
                    string str = this.txtIP.Text;
                    IPAddress ip = IPAddress.Parse(str);
                    //MessageBox.Show("IP地址填写不符合规定!", "异常提示");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("IP地址填写不符合规定，" + ex.Message, "异常提示");
                    return;
                }

            }
            if (this.txtPort.Text == string.Empty)
            {
                MessageBox.Show("必须填写读写器IP地址!", "异常提示");
                return;
            }
            else
            {
                try
                {
                    string str = this.txtPort.Text;
                    int port = int.Parse(str);
                    //MessageBox.Show("端口填写不符合规定!", "异常提示");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("端口填写不符合规定，" + ex.Message, "异常提示");
                    return;
                }
            }

            nsConfigDB.ConfigDB.saveConfig("restIP", this.txtIP.Text);
            nsConfigDB.ConfigDB.saveConfig("restPort", this.txtPort.Text);

            staticClass.restPort = this.txtPort.Text;
            staticClass.restIP = this.txtIP.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length > 0)
            {
                string strToSpeakOut = this.textBox1.Text;
                CommandInfo c = new CommandInfo("led", strToSpeakOut, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "172.16.13.99");
                string jsonString = string.Empty;
                jsonString = JSON.Instance.ToJSON(c);
                HttpWebConnect helper = new HttpWebConnect();
                helper.RequestCompleted += new deleGetRequestObject(helper_RequestCompleted_addLedInfo);
                string url = staticClass.get_set_led_url();
                helper.TryPostData(url, jsonString);
            }
        }
        void helper_RequestCompleted_addLedInfo(object o)
        {
            string strLedInfo = (string)o;

            CommandInfo u2 = fastJSON.JSON.Instance.ToObject<CommandInfo>(strLedInfo);

            deleControlInvoke dele = delegate(object oNull)
            {
                CommandInfo c = oNull as CommandInfo;
                if (c.state == "ok")
                {
                    this.lblStatus.Text = "设置成功！";
                }
                else
                {
                    this.lblStatus.Text = "设置失败！";
                }
            };

        }
    }
}
