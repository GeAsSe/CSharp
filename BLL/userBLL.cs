using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class userBLL
    {
        public int Check(user u)
        {
            return new userDAL().Check(u);
        }

        public string getName(user u)
        {
            return new userDAL().getName(u);
        }

        public user getUser(user u)
        {
            return new userDAL().getUser(u);
        }
    }
}
