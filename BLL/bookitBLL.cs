using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class bookitBLL
    {
        public bool bitit(int userId, string Isbn)
        {
            return new bookitDAL().bitit(userId, Isbn);
        }

        public bool order(string bname, int userId, string time)
        {
            
            return new bookitDAL().order(bname, userId, Convert.ToDateTime(time).AddDays(30));
        }

        public bool lose(int uid,string bname)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            return new bookitDAL().lose(uid, bname, time);
        }
    }
    
}
