using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;
using Edit.Error;

namespace Edit
{
    [Serializable]
    class ClassBuilder:MateData
    {
        List<Operation> operationList;
        List<Function> functionList;

        public String name;
        SignTable localVariables = new SignTable();
        //private Function templateFunc;
        FunctionTable localFunctions = new FunctionTable();

        private Class template;
        public ClassBuilder(String name) : base(name)
        {
            this.name = name;
        }

        public void setOperationList(List<Operation> operationList)
        {
            this.operationList = operationList;
        }

        public void addArgs(Sign arg)//声明类时用于添加类
        {
            this.localVariables.Add(arg);
        }

        public Class build(List<Sign> args)
        {
            Class c = new Class(Utils.getRandomId());
            c.setFunctionList(this.functionList);
            c.setOperationList(this.operationList);
            Lex.TotalSignList.Add(new Sign(c));
            return c;
        }                    
    }

    [Serializable]
    class Class : MateData
    {
        List<Operation> operationList;
        List<MateData> operationResult;
        List<Function> functionList;
        List<Sign> signList;

        String name;
        protected SignTable localVariables = new SignTable();

        public Class(String id) : base(id) { }
        public Class(String id, String name) : base(id) { this.name = name; }

        public void setOperationList(List<Operation> operationList)
        {
            foreach (Operation operation in operationList)
            {   //将每个operation中的变量替换为局部变量
                foreach (Sign sign in operation.signTable)
                {
                    Sign s = this.localVariables.getSignById(sign.id);
                    if (s != null)
                    {
                        sign.content = s.content;
                    }
                }
            }
        }


        public void setFunctionList(List<Function> functionList)
        {
            this.functionList = functionList;
        }

        public virtual void setArgsList(List<Sign> args)//创建类时添加类型定义 
        {
            this.localVariables.Clear();
            for (int i = 0; i < args.ToArray().Length; i++)
            {
                this.localVariables[i].content = args[i].content;
                //this.signList.Add(args[i]);
                this.signList.Add(localVariables[i]);
            }
        }

        public void setFormalArgsList(SignTable localVariables)
        {
            foreach (Sign sign in localVariables)
            {
                this.localVariables.Add(new Sign(sign.id));
            }
        }
    }

    internal class Objects : Class
    {// 也没有用处
        private string name;
        string id;
        protected SignTable localVariables = new SignTable();
        public Objects(String id) : base(id) { }
        public Objects(String id,String name) :base(id){ this.name = name; }

       
    }
    /*没有实现进入Class的接口 预期在Function.cs run()中调用*/
}
