using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class userBook_BLL
    {
        public List<book> getHistoryBooks(int uid)
        {
            return new UserBookDAL().getHistoryBooks(uid);
        }

        public List<MyBorrow> GetMyBorrows(int uid)
        {
            return new UserBookDAL().getNowBooks(uid);
        }
    }
}
