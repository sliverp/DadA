using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
                int a1 = -r1.Next(1, int.MaxValue);
                s = a1.ToString();
            } while (Lex.TotalSignList.has(s));
            return s;

        }
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
    }
    
}
