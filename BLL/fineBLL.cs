using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class fineBLL
    {
        public List<fine> GetFines()
        {
            return new fineDAL().GetFines();
        }

        public List<fine> GetFines(int uid, string sDate, string eDate)
        {
            return new fineDAL().GetFines(uid, sDate, eDate);
        }

    }
}
