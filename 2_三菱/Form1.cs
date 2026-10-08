using HslCommunication;
using HslCommunication.Profinet.Melsec;
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

namespace _2_三菱
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //写入1
        private void button1_Click(object sender, EventArgs e)
        {
            plc.Write("D100",1);
        }
        //写入2
        private void button2_Click(object sender, EventArgs e)
        {
            plc.Write("D100", 2);
        }
        MelsecMcNet plc;
        private void Form1_Load(object sender, EventArgs e)
        {
            //ip+端口号
            plc = new MelsecMcNet("192.168.122.3", 502);
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
                OperateResult<int> a1 = plc.ReadInt32("D100");
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
