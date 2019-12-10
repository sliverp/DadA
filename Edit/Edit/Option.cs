using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;

namespace Edit
{
    abstract class Option
    {
        public String mean;
        abstract public void doSomethings();
        public MateData result;
    }

    //举个例子,这个操作其实没有写完
    class AssignOpthon : Option
    {
        private MateData assigned;//需要被赋值的对象;
        private SignTable signTable;//符号表
        private List<MateData> dataList;
        public AssignOpthon(SignTable signTable)
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
                if (sign.mean == "结果") continue;
                if(sign.content is Hashable)
                {
                        s = s + sign.content.toString();
                }
                else if (sign.content == null)
                {
                    s = s + sign.mean;
                }

            }
            DataTable dataTable = new DataTable();
            double x = double.Parse(dataTable.Compute(s, null).ToString());
            DadaInt data = new DadaInt("ddd");
            data.setData(x.ToString());
            Sign ss = signTable.Find((e) => e.mean == "结果");
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

    class ReturnOpthon : Option
    {
        MateData returnDate;
        public override void doSomethings()
        {

            throw new NotImplementedException();
        }
    }
}
