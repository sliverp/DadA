using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Edit.Data;
using Edit.Error;

namespace Edit
{
    [Serializable]
    class Sign {
        public String id;
        public String type;
        public MateData content;
        public int instenceNum;
        public Sign(MateData content)
        {
            this.id = content.id;
            this.content = content;
        }
        public Sign(MateData content, String type)
        {
            this.id = content.id;
            this.content = content;
            this.type = type;
        }
        public Sign(String id)
        {
            this.id = id;
        }

        public Sign(String id,String type)
        {
            this.id = id;
            this.type = type;
        }
        
    }
   
    [Serializable]
    class SignTable: List<Sign>
    {
        public bool isInSignTable(String id)
        {
            return false;
        }
       

        public MateData getData(String id)
        {
            Sign s=this.Find((c) => c.id == id);
            return s.content;
        }
        public Sign getSignById(String id)
        {
            return this.Find((e) => e.id == id);
        }

       
        public bool has(String id)
        {
            return this.Find((e) => e.id == id) != null;
        }

        public int size()
        {
            return this.ToArray().Length;
        }

        public override String ToString()
        {
            String result="";
            foreach(Sign temp in this)
            {
                result += temp.id + " " + temp.type + "\n";
            }
            return result;
        }
    }

    class FunctionTable: List<FunctionBuilder>
    {
        public FunctionBuilder getFunctionBuilderByName(String name)
        {
            FunctionBuilder f = null;
            f = this.Find((e) => e.name == name);
            if (f == null)
            { 
                FunctionNotDefine fe=new FunctionNotDefine("函数名:"+name+"未定义");
                throw fe;
            }
            return f;
        }

        public Function buildFunctionByName(String name,List<Sign> args)
        {
            FunctionBuilder fb = this.getFunctionBuilderByName(name);
            Function f = fb.build(args);
            return f;
        }
    }

    class ClassTable : List<ClassBuilder>
    {
        public ClassBuilder getClassBuilderByname(String name)
        {
            ClassBuilder c = null;
            c = this.Find((e) => e.name == name);
            if (c == null)
            {
                ClassNotDefine ce = new ClassNotDefine("类名" + name + "未定义");
                throw ce;
            }
            
            return c;
        }

        public Class buildClassByname(String name,List<Sign> args)
        {//创建类
            ClassBuilder cb = this.getClassBuilderByname(name);

            Class c = cb.build(args);
            return c;
        }
        public Class buildObjectByname(String name,List<Sign> args)
        {//创建对象
            ClassBuilder cb = this.getClassBuilderByname(name);
            Class c = this.Find((e) => e.name == name).build(args);
            if (c == null)
            {
                ClassNotDefine ce = new ClassNotDefine("类名" + name + "未定义");
                throw ce;
            }
            return c;
        }
    }

    class Lex
    {
        public static SignTable TotalSignList = new SignTable();//全局符号表
        public List<Operation> operationsList = new List<Operation>();
        public List<List<String>> sentences = new List<List<string>>();
        public static FunctionTable TotalFunctionList = new FunctionTable();//存有哪些声明的函数
<<<<<<< HEAD
    
=======
        public static ClassTable TotalClassList = new ClassTable();//存有哪些声明的类
>>>>>>> f2561211b97fd205cd3bba5bc7be630a5d6ac0c2
        public Lex(String program)
        {

            List<String> rawSentences = program.Split('\n').ToList();
            List<SignTable> rawOperations = new List<SignTable>();
            for(int i = 0; i<rawSentences.Count; i++)
            {
                
                SignTable tempT = new SignTable();
                int operationMode = 0;
                List<String> words = rawSentences[i].Replace("\t", "").Replace(";","").Split(' ').ToList();
                
                foreach (String word in words)
                {
                    if (word == "func")
                    {
                        operationMode = 2;
                        tempT.Add(new Sign(word, "保留字"));
                    } 
                    else if(word == "while")
                    {
                        operationMode = 3;
                        tempT.Add(new Sign(word, "保留字"));
                    }
                    else 
                    {
                        if(word == "return")
                        {
                            tempT.Add(new Sign(word, "保留字"));
                        }
                        else if (IsNumeric(word))
                        {
                            tempT.Add(new Sign(new DadaInt("",word)));
                        }
                        else if (IsMark(word))
                        {
                            tempT.Add(new Sign(word, "标点"));
                        }
                        else if (IsOperator(word))
                        {
                            tempT.Add(new Sign(word, "op"));
                        }
                        else
                        {
                            tempT.Add(new Sign(word, "id"));
                        }
                    }
                    
                }
                if (operationMode == 0)
                    operationMode = 1;
                tempT.Add(new Sign(operationMode + "", "操作"));
                rawOperations.Add(tempT);
                }
            
            foreach(SignTable operation in rawOperations)
            {
                Console.WriteLine(operation);
            }
            
            
        }
        
        //该函数对每个符号串进行语法分析，生成最终的操作表
        public static List<Operation> Grammar(List<SignTable> rawOperations)
        {
            List<Operation> result = new List<Operation>;

            foreach(SignTable raw in rawOperations)
            {
                Operation oper;
                //遇到赋值语句，则要分析出：左值是？右值是？
                if (raw[raw.size() - 1].id == "1")
                {
                    int i = 0;
                    for (; i < raw.size(); i++)
                    {
                        if (raw[i].id == "=") break;
                    }
                    if (i < raw.size())
                    {
                        //等号之前的变量标记为左值
                        for(int j = i - 1; j >= 0; j--)
                        {
                            if (raw[j].type == "id")
                                raw[j].type = "结果";
                        }
                        //等号之后的变量标记位右值
                        for(int j = i + 1; j < raw.size(); j++)
                        {
                            if (raw[j].type == "id")
                                raw[j].type = "args";
                        }
                    }
                    oper = new AssignOperation(raw);
                    result.Add(oper);
                }
                //遇到函数定义语句，要分析出，函数名是？，变量是？
                else if(raw[raw.size() - 1].id == "2")
                {
                    int i = 0;
                    for ( ; i < raw.size(); i++)
                    {
                        if (raw[i].type == "保留字")
                        {//标记上该id为函数名
                            raw[i + 1].type = "funcname";
                        }
                        else if (raw[i].id == "(") break;
                    }
                    //把前括号到后括号的内容都标记为args
                    for (int j = i + 1; raw[j].id != ")"; j++)
                    {
                        raw[j].type = "args";
                    }
                }
                //遇到循环的语句，要分析出，循环判断条件是？判断左值是？右值是？
                else
                {
                    //分析括号之内的条件
                    bool isleft = true; //标记应当为左值，假如遇到不等号，切换为右值
                    for(int i = 2; raw[i].id != ")"; i++)
                    {
                        if (raw[i].type == "id")
                        {
                            if (isleft) raw[i].type = "ConditionLeft";
                            else raw[i].type = "ConditionRight";
                        }
                        else if(raw[i].type == "op")
                        {
                            isleft = !isleft;
                            raw[i].type = raw[i].id;
                        }
                    }
                }
            }
        }
        

        public static bool IsNumeric(String value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        public static bool IsMark(String value)
        {
            return (value == "," || value == "." || value == "(" || value == ")" || value == "{" || value == "}");
        }

        public static bool IsOperator(String value)
        {
            return (value == "=" || value == "==" || value == "++" || value == "--" || value == "**" || value == "+=" || value == "+" || value == "-" || value == "*" || value == "/" || value == "and" || value == "not" || value == "or" || value == ">" || value == "<" || value == ">=" || value == "<=");
        }
<<<<<<< HEAD




=======
                     
        //public void test()//以下全是test!!!!!!!!!!
        //{
        //    //f(){
        //    //    return 1 + 2;
        //    //}
        //    //a = f();
        //    //SignTable signs = new SignTable();
>>>>>>> f2561211b97fd205cd3bba5bc7be630a5d6ac0c2


    //public void test()//以下全是test!!!!!!!!!!
    //{
    //    //f(){
    //    //    return 1 + 2;
    //    //}
    //    //a = f();
    //    //SignTable signs = new SignTable();

    //    //DadaInt a = new DadaInt("a");
    //    //a.setData("456789");
    //    //signs.Add(new Sign(a));

    //    //signs.Add(new Sign("+"));

    //    //DadaInt b = new DadaInt("b");
    //    //b.setData("456789");
    //    //signs.Add(new Sign(b));


    //    //Function f = new Function("f");
    //    //List<Operation> options = new List<Operation>();
    //    //AssignOperation assignOpthon = new AssignOperation(signs);
    //    //options.Add(assignOpthon);
    //    //f.setOptionList(options);


    //    //SignTable signs2 = new SignTable();

    //    //Sign s = new Sign(new DadaInt("s"));
    //    //s.type = "结果";
    //    //signs2.Add(s);


    //    //signs2.Add(new Sign(f));

    //    //AssignOperation assignOpthon2 = new AssignOperation(signs2);

    //    //assignOpthon2.doSomethings();



    //    //assignOpthon.doSomethings();
    //    //MateData mateData = assignOpthon.result;


    //    //以下测试
    //    //不要跑起来,这个函数没有递归出口,可以断点调试

    //    //
    //    //Func foo(a){
    //    //    b=a+1;    
    //    //    foo(b);
    //    //}
    //    //foo(1)







    //    //Func foo(a)=================
    //    SignTable signs = new SignTable();
    //    Sign s1 = new Sign("","保留字");
    //    Sign s2 = new Sign("foo","funcname");
    //    Sign args = new Sign("a", "args");

    //    signs.Add(s1);
    //    signs.Add(s2);
    //    signs.Add(args);

    //    FunctionDefinationOpration fdo = new FunctionDefinationOpration(signs);
    //    //=====================================








    //    // {=====================函数定义的前括号
    //    List<Operation> fooOperator = new List<Operation>();
    //    //==================================








    //    //    b=a+1;===========================
    //    SignTable op1Signtable = new SignTable();

    //    op1Signtable.Add(new Sign("b","结果"));
    //    op1Signtable.Add(new Sign("a"));
    //    op1Signtable.Add(new Sign("","+"));
    //    op1Signtable.Add(new Sign(new DadaInt("","1")));

    //    AssignOperation assign1 = new AssignOperation(op1Signtable);
    //    //=========================================










    //    //    foo ( b ) ;===============================
    //    SignTable op2Signtable = new SignTable();

    //    FunctionBuilder foobuilder = Lex.TotalFunctionList.getFunctionBuilderByName("foo");     
    //    op2Signtable.Add(new Sign(foobuilder));
    //    op2Signtable.Add(new Sign("b", "args"));


    //    AssignOperation assign2 = new AssignOperation(op2Signtable);
    //    //===============================================







    //    // }  =====================函数声明结尾时的花括号
    //    List<Operation> operations = new List<Operation>();
    //    operations.Add(assign1);
    //    operations.Add(assign2);
    //    fdo.addOperatorList(operations);
    //    //=======================================================








    //    //foo(1);==============================
    //    SignTable useSigntable = new SignTable();

    //    FunctionBuilder f1 = Lex.TotalFunctionList.getFunctionBuilderByName("foo");
    //    useSigntable.Add(new Sign(f1));
    //    useSigntable.Add(new Sign(new DadaInt("","1"),"args"));
    //    AssignOperation use = new AssignOperation(useSigntable);
    //    //==================================





    //    use.doSomethings(Lex.TotalSignList);


    //}
}
}
