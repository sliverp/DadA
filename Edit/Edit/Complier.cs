using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;
namespace Edit
{
    class ComplierAndRunner
    {
       public Lex Lexer;

        public void init()
        {
            //初始化系统函数
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

        public void programStart()
        {
            //Function startFunction = Lex.TotalFunctionList.buildFunctionByName("main", null);
            //startFunction.run();

            //测试print函数
            FunctionBuilder foobuilder = Lex.TotalFunctionList.getFunctionBuilderByName("print");
            SignTable s = new SignTable();
            Sign a1 = new Sign(new DadaInt("", "12456"), "args");
            Sign a2 = new Sign(new DadaInt("", "12456"), "args");
            Sign a3 = new Sign(new DadaInt("", "12456"), "args");
            s.Add(a1);
            s.Add(a1);
            s.Add(a1);
            Function f = foobuilder.build(s);
            f.run();
        }
    }
}
