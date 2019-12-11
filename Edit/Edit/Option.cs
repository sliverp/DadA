﻿using System;
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
        public SignTable signTable;//符号表
        abstract public void doSomethings();
        public MateData result;
    }

    //举个例子,这个操作其实没有写完
    class AssignOperation : Operation
    {
        private MateData assigned;//需要被赋值的对象;
        
        private List<MateData> dataList;
        public AssignOperation(SignTable signTable)
        {
            this.signTable = signTable;
            this.mean = "赋值";
        }
        public override void doSomethings()
        {
            foreach(Sign sign in signTable)
            {
                if (sign.content is Function)
                {
                    Function f = (Function)sign.content;
                    MateData fResult = f.run();
                    sign.content = fResult;
                   
                }
            }
            String s = "";
            foreach(Sign sign in signTable)
            {
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
            DadaInt data = new DadaInt("ddd");
            data.setData(x.ToString());
            Sign ss = signTable.Find((e) => e.type == "结果");
            if (ss != null) {
                ss.content = data;
            }
            else
            {
                result = data;
                this.mean = "返回";
            }
               
        }
    }
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
        public FunctionDefinationOpration(SignTable signs,List<Operation> operations)
        {
            String funcId = "";
            if (signs[0].type == "保留字")
            {
                foreach (Sign sign in signs)
                {
                    if (sign.type == "保留字") continue;
                    if(sign.content is Function)
                    {
                        funcId = sign.id;
                        Function f = new Function(sign.id);
                        Lex.TotalSignList.Add(new Sign(f));//建立函数至符号表
                        f.setOptionList(operations);
                        continue;
                    }
                    if (sign.type == "args")
                    {
                        Sign func = Lex.TotalSignList.getSignById(funcId);
                        ((Function)func.content).addArgs(sign);
                    }
                }
            }           
        }

        public override void doSomethings()
        {
            //nothing to do
        }
    }
    class ReturnOpthon : Operation
    {
        MateData returnDate;
        public override void doSomethings()
        {

            throw new NotImplementedException();
        }
    }
}
