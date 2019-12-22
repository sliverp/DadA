using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
<<<<<<< HEAD
            Lex test = new Lex("func f ( ) {\n\treturn 1 + 2;\n}\na = f ( );");
            Console.ReadLine();
            /*
=======
            //Lex test = new Lex("f(){\n\treturn 1 + 2;\n}\na = f();");

            
>>>>>>> f2561211b97fd205cd3bba5bc7be630a5d6ac0c2
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
}
