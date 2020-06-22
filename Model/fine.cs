using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class fine
    {
        public int fid { get; set; }            //罚单id
        public int uid { get; set; }            //罚单对象的user id
        public string name { get; set; }        //罚单对象的user name
        public int bid { get; set; }            //罚单的book id
        public string bname { get; set; }       //罚单的book name
        public Decimal panalty { get; set; }    //罚单的罚金
        public string ftime { get; set; }       //罚单的时间

    }
}
