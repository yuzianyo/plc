using S7.Net;
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

namespace _1_西门子通信
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plc.Write("MD100", 1);//设备红灯亮
        }

        private void button2_Click(object sender, EventArgs e)
        {
            plc.Write("MD100", 2);//设备黄灯亮
        }

        private void button3_Click(object sender, EventArgs e)
        {
            plc.Write("MD100", 0);//全都灭
        }
        Plc plc;
        private void Form1_Load(object sender, EventArgs e)
        {
            //连接plc
            try
            {
                //参数1.cpu类型
                plc = new Plc(CpuType.S71200, "192.168.122.2", 0, 1);
                plc.Open();//打开plc

                //实时读取
                Thread th = new Thread(ReadMD104);
                th.Start();

            }
            catch (Exception)
            {

                MessageBox.Show("plc连接失败！");
            }

        }
        //读取MD104地址的数据是多少 展示到界面
        public async void  ReadMD104()
        {
            while (true)
            {
                int d1 = int.Parse(plc.Read("MD104").ToString());
                if (d1 == 1)//按钮按下时 把1写入地址 
                {
                    Invoke(new Action(() =>
                    {
                        label1.BackColor = Color.Green;
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        label1.BackColor = Color.Red;
                    }));
                    await Task.Delay(1000);
                }

            }
        }



    }
}
