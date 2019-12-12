using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;
using Edit.Error;

namespace Edit
{
    class FunctionBuilder:MateData
    {
        List<Operation> optionList;
        public String name;
        SignTable localVariables = new SignTable();
        public FunctionBuilder(String name):base(name)
        {
            this.name = name;
        }

        public void addArgs(Sign arg)//声明函数时用于添加形式参数
        {
            this.localVariables.Add(arg);
        }

        public Function build(List<Sign> args)
        {
            Function f = new Function(Utils.getRandomId(), this.name);
            f.setFormalArgsList(this.localVariables);
            f.setArgsList(args);
            f.setOptionList(this.optionList);
            Lex.TotalSignList.Add(new Sign(f));
            return f;
        }
        public void setOperationList(List<Operation> optionList)
        {
            this.optionList = optionList;
        }
    }


    class Function:MateData
    {
        List<Operation> optionList;
        List<MateData> optionResult;
        String name;
        int argsPushPointer = 0;
        SignTable localVariables = new SignTable();
        List<MateData> argsData = new List<MateData>();

        public Function(String id) : base(id) { }
        public Function(String id,String name) : base(id) { this.name = name; }


        public void setArgsList(List<Sign> args)//调用函数时添加实参
        {
            if (args.ToArray().Length > localVariables.ToArray().Length)
            {
                VariableVinconsistent vv=new  VariableVinconsistent("形式参数与实际参数数量不一致");
                throw vv;
            }
            for(int i = 0; i < args.ToArray().Length; i++)
            {
                this.localVariables[i].content = args[i].content;
            }
            
        }

        public void setFormalArgsList(SignTable localVariables)
        {
            foreach(Sign sign in localVariables)
            {
                this.localVariables.Add(new Sign(sign.id));
            }
        }
        public void setOptionList(List<Operation> optionList)
        {
            foreach(Operation operation in optionList)
            {   //将每个operation中的变量替换为局部变量
                foreach(Sign sign in operation.signTable)
                {
                    Sign s = this.localVariables.getSignById(sign.id);
                    if (s != null)
                    {
                        sign.content = s.content;
                    }
                }
            }
            
            this.optionList = optionList;
        }
        public MateData run()
        {
            foreach(Operation op in this.optionList)
            {
                op.doSomethings(this.localVariables);
                 //测试时输出用 
                String s= this.localVariables[0].content.toString() + "\r\n";
                Form1.output.Invoke(new Action(() =>
                {
                    Form1.output.Text += s;
                }));
                System.Threading.Thread.Sleep(1000);
                //==============
            }
            return optionList.Find((c) =>c.mean == "返回").result;
        }
    }
}
