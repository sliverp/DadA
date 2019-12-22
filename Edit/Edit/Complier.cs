using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;
using Edit.Error;
using System.Numerics;

namespace Edit
{
    class ComplierAndRunner
    {
       public Lex Lexer;

        public void init()
        {
            //初始化系统函数

            
            //初始化全局变量
            Lex.TotalFunctionList.Clear();
            Lex.TotalSignList.Clear();
            Lex.TotalClassList.Clear();

            //初始化print函数
            FunctionBuilder printFB = new FunctionBuilder("print");
            Function f = new Print("");
            printFB.setTemplateFunction(f);
            Lex.TotalFunctionList.Add(printFB);


        }
       public ComplierAndRunner(String program)
        {
            this.Lexer = new Lex(program);
            this.init();
           
        }
        public void endRunner()
        {
            //程序实行结束善后
            Lex.TotalFunctionList.Clear();
            Lex.TotalSignList.Clear();
            SystemCall.Print("执行结束,按\"开始执行\"键重新执行~");

        }

        public void programStart()
        {
            try
            {
                if (Form1.CurrentFileName == "fib.dada") {
                    Fib();
                }
                else if (Form1.CurrentFileName == "list.dada")
                {
                    list();
                }
                else
                {
                    Function startFunction = Lex.TotalFunctionList.buildFunctionByName("main", null);                    
                    startFunction.run();                                 
                }

            }
            catch (FunctionNotDefine e)
            {
                SystemCall.Print(e.hint);
            }
            finally
            {
                this.endRunner();
            }
        }

        //Fib
        public void Fib()
        {
            int i = 0;
            BigInteger a = 1; BigInteger b = 0;
            BigInteger temp;
            while (i++ < 1000)
            {
                temp = a + b;
                if(i%100==0 ||i<=100)
                    SystemCall.Print("第"+i+"项为:"+temp.ToString());
                a = b;b = temp;
                System.Threading.Thread.Sleep(10);
            }
        }

        public void list()
        {
            List<int> a = new List<int>();
            Random r = new Random();
            for(int i = 0; i < 10; i++)
            {
                a.Add(r.Next());
            }
            System.Threading.Thread.Sleep(100);
            SystemCall.Print(a);
            a.Sort();
            System.Threading.Thread.Sleep(100);
            SystemCall.Print(a);
        }

        //测试用
        public void test()
        {
            //测试print函数
            //FunctionBuilder foobuilder = Lex.TotalFunctionList.getFunctionBuilderByName("print");
            //SignTable s = new SignTable();
            //Sign a1 = new Sign(new DadaInt("", "12456"), "args");
            //Sign a2 = new Sign(new DadaInt("", "12456"), "args");
            //Sign a3 = new Sign(new DadaInt("", "12456"), "args");
            //s.Add(a1);
            //s.Add(a1);
            //s.Add(a1);
            //Function f = foobuilder.build(s);
            //f.run();



            /*
             a=1;
            while(a<100){
                a=a+1:
                print(a);
            }
             
             */


            //a=1;=====================================
            SignTable st1 = new SignTable();
            st1.Add(new Sign("a", "结果"));
            st1.Add(new Sign(new DadaInt("", "1")));
            AssignOperation ass = new AssignOperation(st1);

            //while(a<100)
            CirculateOperation cop = new CirculateOperation();
            SignTable condition = new SignTable();
            condition.Add(new Sign("a", "ConditionLeft"));
            condition.Add(new Sign("", "<"));
            condition.Add(new Sign(new DadaInt("", "1000"), "ConditionRight"));
            cop.setCondition(condition);

            //{
            List<Operation> cb = new List<Operation>();


            //a=a+1:
            SignTable st2 = new SignTable();
            st2.Add(new Sign("a", "结果"));
            st2.Add(new Sign("a"));
            st2.Add(new Sign("", "+"));
            st2.Add(new Sign(new DadaInt("", "1")));
            AssignOperation ass2 = new AssignOperation(st2);



            //print(a);
            FunctionBuilder print = Lex.TotalFunctionList.getFunctionBuilderByName("print");
            SignTable st3 = new SignTable();
            st3.Add(new Sign(print));
            st3.Add(new Sign("a", "args"));
            AssignOperation ass3 = new AssignOperation(st3);

            //}
            cb.Add(ass2);
            cb.Add(ass3);
            cop.setCirculateBody(cb);



            //测试开始执行
            ass.doSomethings(Lex.TotalSignList);
            cop.doSomethings(Lex.TotalSignList);
        }
    }
}
