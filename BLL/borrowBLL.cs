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
    }
}
