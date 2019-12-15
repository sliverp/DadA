using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;

namespace Edit
{
    class SystemCall//系统调用
    {
        public static void Print(params Sign[] datas)
        {  //打印
            String s = "";
            for(int i = 0; i < datas.Length; i++)
            {
                s += datas[i].content.toString() + "\r\n";
            }
            Form1.output.Invoke(new Action(() =>
            {
                Form1.output.Text += s;
                Form1.output.Select(Form1.output.Text.Length, 0);
                Form1.output.ScrollToCaret();
            }));
        }
    }
}
