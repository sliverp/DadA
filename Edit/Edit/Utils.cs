using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit
{
    class Utils
    {
        public static String getRandomId()
        {
            Random r1 = new Random();
            String s = "";
            do
            {
                int a1 = r1.Next(1, int.MaxValue);
                s = a1.ToString();
            } while (Lex.TotalSignList.isIn(s));
            return s;

        }
    }
}
