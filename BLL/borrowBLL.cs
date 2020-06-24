using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class borrowBLL
    {
        public int Borrow(user u, book b)
        {
            return new borrowDAL().Borrow(u, b);
        }

        public List<history> getHistories(user u)
        {
            return new borrowDAL().GetHistories(u);
        }

        public bool returnBook(int uid, int bid, int borrowtimes)
        {
            return new borrowDAL().returnBook(uid, bid, borrowtimes);
        }

        public bool renewBook(int uid, int bid, int borrowtimes, string time)
        {
            return new borrowDAL().renewBook(uid, bid, borrowtimes, time);
        }

        public bool lossBook(history h, book b)
        {
            return new borrowDAL().lossBook(h, b);
        }
    }
}
