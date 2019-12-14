using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit
{
    class ComplierAndRunner
    {
       public Lex Lexer;

        public void init()
        {


        }
       public ComplierAndRunner(String program)
        {
            this.Lexer = new Lex(program);
           
        }

        public void programStart()
        {
            Function startFunction = Lex.TotalFunctionList.buildFunctionByName("main", null);
            startFunction.run();

        }
    }
}
