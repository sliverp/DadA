using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;
using Edit.Error;

namespace Edit
{
    class Function:MateData
    {
        List<Operation> optionList;
        List<MateData> optionResult;
        List<String> args;
        int argsPushPointer = 0;
        SignTable localVariables = new SignTable();
        List<MateData> argsData = new List<MateData>();
        public Function(String id) : base(id) { }
        public void addArgs(Sign arg)//声明函数时用于添加形式参数
        {
            this.localVariables.Add(arg);
        }

        public void setArgs(Sign arg)//调用函数时添加实参
        {
            if (argsPushPointer >= localVariables.ToArray().Length)
            {
                VariableVinconsistent vv=new  VariableVinconsistent("形式参数与实际参数数量不一致");
                throw vv;
            }         
            this.localVariables[this.argsPushPointer++].content = arg.content;
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
                op.doSomethings();
            }
            return optionList.Find((c) =>c.mean == "返回").result;
        }
    }
}
