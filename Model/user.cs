using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class user
    {
        public int uid          { get; set; }  //用户id
        public string name      { get; set; }  //用户名
        public string password  { get; set; }  //用户密码
        public int admin        { get; set; }  //是否为管理员的标识 1：是; 0：不是
    }
}
