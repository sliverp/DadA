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
        public String type;
        public MateData content;
        public int instenceNum;
        public Sign(MateData content)
        {
            this.id = content.id;
            this.content = content;
        }
        public Sign(String type)
        {
            this.type = type;
        }

        public Sign(String id,String type)
        {
            this.id = id;
            this.type = type;
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
        public Sign getSignById(String id)
        {
            return this.Find((e) => e.id == id);
        }
    }
    class Lex
    {
        public static SignTable TotalSignList = new SignTable();//全局符号表
        public List<Operation> optionsList = new List<Operation>();
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
            //SignTable signs = new SignTable();

            //DadaInt a = new DadaInt("a");
            //a.setData("456789");
            //signs.Add(new Sign(a));

            //signs.Add(new Sign("+"));

            //DadaInt b = new DadaInt("b");
            //b.setData("456789");
            //signs.Add(new Sign(b));


            //Function f = new Function("f");
            //List<Operation> options = new List<Operation>();
            //AssignOperation assignOpthon = new AssignOperation(signs);
            //options.Add(assignOpthon);
            //f.setOptionList(options);


            //SignTable signs2 = new SignTable();

            //Sign s = new Sign(new DadaInt("s"));
            //s.type = "结果";
            //signs2.Add(s);


            //signs2.Add(new Sign(f));

            //AssignOperation assignOpthon2 = new AssignOperation(signs2);

            //assignOpthon2.doSomethings();



            //assignOpthon.doSomethings();
            //MateData mateData = assignOpthon.result;


            //以下测试
            //不要跑起来,这个函数没有递归出口,可以断点调试

            //
            //Func foo(a){
            //    b=a+1;    
            //    foo(b);
            //}
            //foo(1)


            //有bug,先别测试

            SignTable signs = new SignTable();

            Sign s1 = new Sign("保留字");
            Sign s2 = new Sign("foo");
            Sign args = new Sign("a", "args");


            List<Operation> fooOperator = new List<Operation>();

            SignTable op1Signtable = new SignTable();

            
            Sign b = new Sign(new DadaInt("b"));
            b.type = "结果";

            Sign a = new Sign(new DadaInt("a"));

            DadaInt dadaInt = new DadaInt("c");
            dadaInt.setData("1");
            Sign c = new Sign(dadaInt);

            op1Signtable.Add(b);
            op1Signtable.Add(a);
            op1Signtable.Add(c);

            

            SignTable op2Signtable = new SignTable();

            Function foo = new Function("foo");
            foo.setArgs(new Sign("b"));
            op2Signtable.Add(new Sign(foo));


            AssignOperation assign1 = new AssignOperation(op1Signtable);
            AssignOperation assign2 = new AssignOperation(op2Signtable);


            List<Operation> operations = new List<Operation>();
            operations.Add(assign1);
            operations.Add(assign2);

            FunctionDefinationOpration fdo = new FunctionDefinationOpration(signs, operations);




            SignTable useSigntable = new SignTable();
            Function function = new Function("foo");
            DadaInt shican = new DadaInt("");
            shican.setData("1");
            function.setArgs(new Sign(shican));
            useSigntable.Add(new Sign(function));
            AssignOperation use = new AssignOperation(useSigntable);


            use.doSomethings();


        }
    }
}
