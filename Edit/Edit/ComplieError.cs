using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit.Error
{

    /// <summary>
    /// 所有错误的基类
    /// </summary>
    public class ComplieException:Exception
    {
        protected String hint=null;//错误提示
        protected String line = null;//错误所处的行
        protected String pos = null;//错误所处的列
        protected String filepath = null;//错误文件
        protected String name= "ComplieError";

        public ComplieException(String hint)
        {
            this.hint = hint;
        }
        public void addExceptionArgs(String hint, String line,String pos)
        {
            if (hint != null) { this.hint = hint; }
            if (line != null) { this.line = line; }
            if (pos != null) { this.pos = pos; }
        }
        public String getErrorMessage()
        {
            if(this.line==null && this.pos == null)
            {
                return this.name + ":错误位置在文件:" + this.filepath + "中.提示:" + this.hint;
            }
            if ( this.pos == null)
            {
                return this.name + ":错误位置在文件" + this.filepath+"的第" +this.line +"行处.提示:" + this.hint;
            }
            return this.name + ":错误位置在文件" + this.filepath + "的第" + this.line + "行第"+this.pos+"个字符处.提示:" + this.hint;
        }
    }
    /// <summary>
    /// 词法分析错误
    /// </summary>
    public class SyntaxException : ComplieException
    {
        public SyntaxException(string hint) : base(hint)
        {
            this.name = "SyntaxError";
        }
    }

    /// <summary>
    /// 语义分析错误
    /// </summary>
    public class SemanticsException : ComplieException
    {
        public SemanticsException(string hint) : base(hint)
        {
            this.name = "SemanticsError";
        }
    }

    public class TypeException : ComplieException
    {
        public TypeException(string hint) : base(hint)
        {
            this.name = "TypeException";
        }

    }

    public class VariableVinconsistent : ComplieException
    {//形式参数与实际参数数量不一致
        public VariableVinconsistent(string hint) : base(hint)
        {
            this.name = "VariableVinconsistent ";
        }

    }


    public class FunctionNotDefine : ComplieException
    {//函数未定义
        public FunctionNotDefine(string hint) : base(hint)
        {
            this.name = "FunctionNotDefine";
        }

    }
}
