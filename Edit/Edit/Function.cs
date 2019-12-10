using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edit.Data;

namespace Edit
{
    class Function:MateData
    {
        List<Option> optionList;
        List<MateData> optionResult;
        List<String> args;
        List<MateData> argsData = new List<MateData>();
        public Function(String id) : base(id) { }
        void setArgs(List<String> args)
        {
            this.args = args;
            foreach(String arg in args)
            {
 
                this.argsData.Add(new MateData(arg));
            }
            
        }
        public void setOptionList(List<Option> optionList)
        {
            this.optionList = optionList;
        }
        public MateData run()
        {
            foreach(Option op in this.optionList)
            {
                op.doSomethings();
            }
            return optionList.Find((c) =>c.mean == "返回").result;
        }
    }
}
