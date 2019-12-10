using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;

namespace Edit
{
    class Sign {
        public String id;
        public String mean;
        public MateData content;
        public int instenceNum;
        public Sign(MateData content)
        {
            this.content = content;
        }
        public Sign(String mean)
        {
            this.mean = mean;
        }
    }
    class SignTable: List<Sign>
    {
        public bool isInSignTable(String id)
        {
            return false;
        }
       

        public MateData getData(String id)
        {
            Sign s=this.Find((c) => c.id == id);
            return s.content;
        }
    }
    class Lex
    {
        public List<Option> optionsList = new List<Option>();
        public List<List<String>> sentencec = new List<List<string>>();
        public Lex(String program)
        {
            List<String> sentences = program.Split(';').ToList();
            foreach(String sentence in sentences)
            {
                List<String> words = sentence.Split(' ').ToList();
                this.sentencec.Add(words);
            }
        }
        public static bool isKeyWord(String s)
        {

            return true;
        }
        public void test()//以下全是test!!!!!!!!!!
        {
            //f(){
            //    return 1 + 2;
            //}
            //a = f();
            SignTable signs = new SignTable();

            DadaInt a = new DadaInt("a");
            a.setData("456789");
            signs.Add(new Sign(a));

            signs.Add(new Sign("+"));

            DadaInt b = new DadaInt("b");
            b.setData("456789");
            signs.Add(new Sign(b));
           
           
            Function f = new Function("f");
            List<Option> options = new List<Option>();
            AssignOpthon assignOpthon = new AssignOpthon(signs);
            options.Add(assignOpthon);
            f.setOptionList(options);


            SignTable signs2 = new SignTable();

            Sign s = new Sign(new DadaInt("s"));
            s.mean = "结果";
            signs2.Add(s);


            signs2.Add(new Sign(f));

            AssignOpthon assignOpthon2 = new AssignOpthon(signs2);

            assignOpthon2.doSomethings();



            //assignOpthon.doSomethings();
            //MateData mateData = assignOpthon.result;

        }
    }
}
