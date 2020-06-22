using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class book
    {
        public int bid { get; set; }            //书的id
        public string bname { get; set; }        //书名
        public string isbn { get; set; }        //书的isbn号
        public Decimal price { get; set; }      //书的价格
        public string publisher { get; set; }   //出版社
        public string author { get; set; }      //作者
        public string place { get; set; }       //位置
        public int status { get; set; }         //书的状态，0:遗失，1:在馆，2:被预定，3:被借
        public int reserve { get; set; }        //如果书被预定了，则记录预定者的uid
    }
}
