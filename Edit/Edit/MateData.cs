using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Edit.Error;

namespace Edit.Data
{

    interface Hashable
    {
        String getHashcode();
    }
    //所有数据的基类
    class MateData
    {
        public String id;
        public MateData(String id)
        {
            this.id = id;
        }
        public virtual String toString()
        {
            return "";
        }
    }

    class DadaString : MateData,Hashable
    {
        private String storage { get; set; }//仓库
        public DadaString(String id) : base(id) { }
        public void setData(String content)
        {
            this.storage = content;
        }
        public String plus(String s)//加法远算
        {
            return this.storage + s;
        }

        public List<String> split(String s)//字符串切分
        {
            String[] ss = this.storage.Split(s.ToCharArray()[0]);
            return ss.ToList();
        }

        public bool equal(String s)//相等
        {
            return this.storage == s;
        }

        public string getHashcode()
        {
            return this.GetHashCode().ToString();
        }
        public override String toString()
        {
            return this.storage;
        }
    }

    class DadaInt : MateData,Hashable//数字
    {
        private BigInteger storage;//大整数
        public DadaInt(string id) : base(id) { }
        public void setData(String content)
        {
            this.storage = BigInteger.Parse(content);
        }
        public String plus(BigInteger num)//相加
        {
            BigInteger a = this.storage + num;
            return a.ToString();
        }
        public String sub(BigInteger num)//-
        {
            BigInteger a = this.storage - num;
            return a.ToString();
        }

        public String multi(BigInteger num)//*
        {
            BigInteger a = this.storage * num;
            return a.ToString();
        }
        public String div(BigInteger num)// /
        {
            BigInteger a = this.storage / num;
            return a.ToString();
        }

        public string getHashcode()
        {
            return this.GetHashCode().ToString();
          
        }
        public override String toString()
        {
            return this.storage.ToString();
        }
    }

    class DadaList : MateData
    {
        private List<MateData> storage = new List<MateData>();
        public DadaList(String id) : base(id) { }
        public MateData getItem(int id){ return this.storage[id];}
        public void append(MateData item) { this.storage.Add(item); }
        public void sort()
        {
            
        }
        public void delate(int id)
        {
            this.storage.RemoveAt(id);
        }
    }

    class DadaMap : MateData//map
    {
        private Dictionary<MateData, MateData> storage = new Dictionary<MateData, MateData>();
        public DadaMap(string id) : base(id) { }
        public void setValue(MateData k,MateData v)
        {
            Type type = k.GetType().GetInterface("Hashable");
            if (type==null) {
                TypeException t = new TypeException("map 的 k 不能hash化");
                throw t;
            }
            this.storage.Add(k, v);
        }
        public MateData getValue(MateData k)
        {
            return this.storage[k];
        }

    }
}
