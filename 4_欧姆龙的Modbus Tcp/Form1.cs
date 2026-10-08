using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_欧姆龙的Modbus_Tcp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plc.Write("1000", 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            plc.Write("1000", 2);
        }
        ModbusTcpNet plc;
        private void Form1_Load(object sender, EventArgs e)
        {
            plc = new ModbusTcpNet("192.168.122.4", 502);
            OperateResult r1 = plc.ConnectServer();//连接plc
            if (r1.IsSuccess)
            {
                MessageBox.Show("连接成功");
                Thread t1 = new Thread(f1);
            }
            else
            {
                MessageBox.Show("连接失败！");
            }
        }
        public async void f1()
        {
            while (true)//一直读
            {
                OperateResult<int> a1 = plc.ReadInt32("1000");
                if (a1.IsSuccess)//读取成功
                {
                    Invoke(new Action(() =>
                    {
                        label1.Text = a1.Content.ToString();
                    }));
                    await Task.Delay(1000);
                }



            }
        }
    }
}
