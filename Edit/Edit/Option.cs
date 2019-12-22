using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;

namespace Edit
{
    abstract class Operation
    {
        public String mean;
        public SignTable signTable;//这一句话里有什么符号
        abstract public void doSomethings(SignTable contextSignTable);//拿到局部变量的符号表,上下文符号
        public MateData result;
        public void initSignTable(SignTable sTable)
        {
            //从上下文符号符号标准中查找值赋值
            foreach (Sign sign in sTable)
            {
                if (this.signTable.has(sign.id))
                {
                    this.signTable.getSignById(sign.id).content = sign.content;
                    //sign.content = this.localSignTable.getSignById(sign.id).content;
                }
            }
        }
    }

    //举个例子,这个操作其实没有写完

    //返回,赋值,函数调用
    class AssignOperation : Operation
    {
        
        public AssignOperation(SignTable signTable)
        {
            this.signTable = signTable;
            this.mean = "赋值";
        }
        public override void doSomethings(SignTable contextTable)
        {
            this.initSignTable(contextTable);
            SignTable runtimeTable = Utils.DeepCopy<SignTable>(this.signTable);//深拷贝运行时符号表

            //执行函数
            for (int i=0;i< runtimeTable.size(); i++)
            {
                if (runtimeTable[i].content is FunctionBuilder)
                {
                List<Sign> trueArgs = new List<Sign>();
                for (int j = i+1; j < signTable.size(); j++)
                {//向后检索实参列表
                    if (runtimeTable[j].type == "args") trueArgs.Add(runtimeTable[j]);
                    else
                    {
                        break;
                    }
                }
                Function f = ((FunctionBuilder)runtimeTable[i].content).build(trueArgs);
                MateData fResult=f.run();

                    runtimeTable[i].content = fResult;
                }
            }
            String s = "";


            //从上下中给变量赋值
            foreach(Sign sign in runtimeTable)
            {
                if (contextTable.has(sign.id)){
                    sign.content = contextTable.getSignById(sign.id).content;
                }
                else if(sign.content is Hashable)
                {
                    if(sign.id!=""&&sign.id[0]!='-')
                        contextTable.Add(sign);
                }

                if (sign.type == "结果") continue;
                if(sign.content is Hashable)
                {
                        s = s + sign.content.toString();
                }
                else if (sign.content == null)
                {
                    s = s + sign.type;
                }

            }
            DataTable dataTable = new DataTable();
            double x = double.Parse(dataTable.Compute(s, null).ToString());
            DadaInt data = new DadaInt("");
            data.setData(x.ToString());
            Sign ss = runtimeTable.Find((e) => e.type == "结果");
            //以下写的很乱,但功能是对的
            if (ss != null)
            {
                if (contextTable.has(ss.id))
                {
                    contextTable.getSignById(ss.id).content = ss.content;
                }
                else
                {
                    contextTable.Add(ss);
                }
            }
            if (ss != null) {
                contextTable.getSignById(ss.id).content = data;
            }
            else
            {
                result = data;
                this.mean = "返回";
            }
            
        }
    }


    //函数定义语句
    class FunctionDefinationOpration : Operation
    {
        /*
         functionId:函数名称
         operations:里面有的操作

        e.g.
            函数定义为:
            Func foo(a,b){
                a=123;
                b=456;
                return a+b;
            }
     SignTable格式::
         
     signs[0].id=null
     signs[0].content=null
     signs[0].type=保留字

     signs[1].id=foo
     signs[1].content=Function

     signs[2].id=a
     signs[2].content=Matedata
     signs[2].type="args"

     signs[3].id=a
     signs[3].content=Matedata
     signs[3].type="args"
        

     List<Operation>格式为:
     operations[0]=AssignmentOperation()
     operations[1]=AssignmentOperation()
     operations[2]=AssignmentOperation()

             */

        Function f;
        FunctionBuilder fb;
        String funcname = "";
        public FunctionDefinationOpration(SignTable signs)
        {            
            if (signs[0].type == "保留字")
            {
                foreach (Sign sign in signs)
                {
                    if (sign.type == "保留字") continue;//讲道理不需要验证,但是验证一下显得专业
                    if(sign.type== "funcname")//讲道理不需要验证,但是验证一下显得专业
                    {
                        funcname = sign.id;
                        f = new Function(sign.id);
                        fb = new FunctionBuilder(funcname);
                        continue;
                    }
                    if (sign.type == "args")
                    {
                        fb.addArgs(sign);
                        
                    }
                }
                Lex.TotalFunctionList.Add(fb);
            }           
        }

        public void addOperatorList( List<Operation> operations)
        {
            Lex.TotalFunctionList.Find((e) => e.name == funcname).setOperationList(operations);
        }

        public override void doSomethings(SignTable signTable)
        {
            //nothing to do
        }
    }


    //循环
    class CirculateOperation : Operation
    {
        Sign ConditionLeft;
        Sign ConditionRight;
        Sign type;
        List<Operation> circulateBody;
        private bool isBreak()
        {
            if (type.type == "<")
            {
                return ((DadaInt)ConditionLeft.content) < ((DadaInt)ConditionRight.content);
            }
            if (type.type == ">")
            {
                return ((DadaInt)ConditionLeft.content) > ((DadaInt)ConditionRight.content);
            }
            if (type.type == "==")
            {
                return ((DadaInt)ConditionLeft.content) == ((DadaInt)ConditionRight.content);
            }
            if (type.type == "!=")
            {
                return ((DadaInt)ConditionLeft.content) != ((DadaInt)ConditionRight.content);
            }
            return false;
        }
        public void setCondition(List<Sign> conditionSignList)
        {
            foreach(Sign s in conditionSignList)
            {
                if(s.type== "ConditionLeft")
                {
                    ConditionLeft = s;
                    this.signTable = new SignTable();
                    this.signTable.Add(s);
                }
                if (s.type == "ConditionRight")
                {
                    ConditionRight = s;
                }
                if (s.type == ">"|| s.type == "<" || s.type == "==" || s.type == "!=")
                {
                    this.type = s;
                }
            }
        }
        public void setCirculateBody(List<Operation> circulateBody)
        {
            this.circulateBody = circulateBody;
        }
        public override void doSomethings(SignTable signTable)
        {
            this.initSignTable(signTable);
            while (this.isBreak())
            {
                foreach(Operation op in this.circulateBody)
                {
                    op.doSomethings(signTable);
                    ConditionLeft.content = signTable.getSignById(ConditionLeft.id).content;
                }
            }
        }
    }
}
