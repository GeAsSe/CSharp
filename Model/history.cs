using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class history
    {
        public int uid { get; set; }        //借阅记录的user id
        public string name { get; set; }    //借阅记录的user name
        public int bid { get; set; }        //借阅记录的book id
        public string bname { get; set; }   //借阅记录的book name
        public string time { get; set; }    //借阅记录的时间，yyyy-mm-dd，为了方便用string记录
        public int borrowtime { get; set; } //用来标识借阅状态的属性，0:已归还; 1:未归还且第一次借; 2:未归还且已经续借
    }
}
