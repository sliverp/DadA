using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Edit.Data;
using Edit.Error;
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
        public Sign(MateData content, String type)
        {
            this.id = content.id;
            this.content = content;
            this.type = type;
        }
        public Sign(String id)
        {
            this.id = id;
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

       
        public bool has(String id)
        {
            return this.Find((e) => e.id == id) != null;
        }

        public int size()
        {
            return this.ToArray().Length;
        }
    }

    class FunctionTable: List<FunctionBuilder>
    {
        public FunctionBuilder getFunctionBuilderByName(String name)
        {
            FunctionBuilder f = null;
            f = this.Find((e) => e.name == name);
            if (f == null)
            { 
                FunctionNotDefine fe=new FunctionNotDefine("函数名:"+name+"未定义");
                throw fe;
            }
            return f;
        }

        public Function buildFunctionByName(String name,List<Sign> args)
        {
            FunctionBuilder fb = this.getFunctionBuilderByName(name);
            Function f = fb.build(args);
            return f;
        }
    }

    class Lex
    {
        public static SignTable TotalSignList = new SignTable();//全局符号表
        public List<Operation> optionsList = new List<Operation>();
        public List<List<String>> sentencec = new List<List<string>>();
        public static FunctionTable TotalFunctionList = new FunctionTable();//存有哪些声明的函数
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



           



            //Func foo(a)=================
            SignTable signs = new SignTable();
            Sign s1 = new Sign("","保留字");
            Sign s2 = new Sign("foo","funcname");
            Sign args = new Sign("a", "args");

            signs.Add(s1);
            signs.Add(s2);
            signs.Add(args);

            FunctionDefinationOpration fdo = new FunctionDefinationOpration(signs);
            //=====================================








            // {=====================函数定义的前括号
            List<Operation> fooOperator = new List<Operation>();
            //==================================








            //    b=a+1;===========================
            SignTable op1Signtable = new SignTable();

            op1Signtable.Add(new Sign("b","结果"));
            op1Signtable.Add(new Sign("a"));
            op1Signtable.Add(new Sign("","+"));
            op1Signtable.Add(new Sign(new DadaInt("","1")));
  
            AssignOperation assign1 = new AssignOperation(op1Signtable);
            //=========================================










            //    foo ( b ) ;===============================
            SignTable op2Signtable = new SignTable();
    
            FunctionBuilder foobuilder = Lex.TotalFunctionList.getFunctionBuilderByName("foo");     
            op2Signtable.Add(new Sign(foobuilder));
            op2Signtable.Add(new Sign("b", "args"));


            AssignOperation assign2 = new AssignOperation(op2Signtable);
            //===============================================







            // }  =====================函数声明结尾时的花括号
            List<Operation> operations = new List<Operation>();
            operations.Add(assign1);
            operations.Add(assign2);
            fdo.addOperatorList(operations);
            //=======================================================








            //foo(1);==============================
            SignTable useSigntable = new SignTable();

            FunctionBuilder f1 = Lex.TotalFunctionList.getFunctionBuilderByName("foo");
            useSigntable.Add(new Sign(f1));
            useSigntable.Add(new Sign(new DadaInt("","1"),"args"));
            AssignOperation use = new AssignOperation(useSigntable);
            //==================================





            use.doSomethings(Lex.TotalSignList);


        }
    }
}
