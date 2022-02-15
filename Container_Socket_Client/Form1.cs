using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContainelDll;
using Container = ContainelDll.Container;

namespace Container_Socket_Client
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 初始化动态库
        /// 服务地址
        /// 服务端口
        /// 重连间隔
        /// </summary>
        private Container _Container = new Container("127.0.0.1", 12011,10);
        private delegate void MessageDelegate(string message);

        public Form1()
        {
            InitializeComponent();

            //订阅相关事件
            _Container.MessageEvent += HandleMessageEvent;
            _Container.DataEvent += HandleDataEvent;
        }

        /// <summary>
        /// 日志事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMessageEvent(object sender, ContainelDll.MessageEventArgs e)
        {
            MessageForm(e.Message);
        }

        /// <summary>
        /// 箱号事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDataEvent(object sender, ContainelDll.DataEventArgs e)
        {
            MessageForm(e.Data);
        }

        private void MessageForm(string obj)
        {            
            if(textBox1.InvokeRequired)
            {
                textBox1.Invoke(new MessageDelegate(MessageForm), new object[] { obj });
            }
            else
            {
                if (textBox1.Lines.Count() > 100)
                {
                    textBox1.Clear();
                }
                textBox1.AppendText(obj);
                textBox1.AppendText("\r\n");
            }
        }
    }
}
