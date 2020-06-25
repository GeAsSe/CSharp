using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class fineDAL
    {
        public List<fine> GetFines()
        {
            List<fine> fines = new List<fine>();
            string cText = "select * from fine";
            MySqlDataReader reader = MysqlHelper.getReader(cText);
            while (reader.Read())
            {
                fine f = new fine();
                f.fid = int.Parse(reader["fid"] + "");
                f.uid = int.Parse(reader["uid"] + "");
                f.name = reader["name"] + "";
                f.bid = int.Parse(reader["bid"] + "");
                f.bname = reader["bname"] + "";
                f.panalty = Decimal.Parse(reader["panalty"] + "");
                f.ftime = reader["ftime"] + "";

                fines.Add(f);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return fines;
        }

        public List<fine> GetFines(int uid, string sDate, string eDate)
        {
            StringBuilder cText = new StringBuilder("select * from fine");
            List<string> wheres = new List<string>();
            if (uid != 0)
            {
                wheres.Add(" uid=" + uid);
            }
            if(sDate != "")
            {
                wheres.Add(" ftime>='" + sDate + "'");
            }
            if (eDate != "")
            {
                wheres.Add(" ftime<='" + eDate + "'");
            }
            if (wheres.Count > 0)
            {
                string wh = string.Join(" and ", wheres.ToArray());
                cText.Append(" where " + wh);
            }
            List<fine> fines = new List<fine>();
            MySqlDataReader reader = MysqlHelper.getReader(cText.ToString());
            while (reader.Read())
            {
                fine f = new fine();
                f.fid = int.Parse(reader["fid"] + "");
                f.uid = int.Parse(reader["uid"] + "");
                f.name = reader["name"] + "";
                f.bid = int.Parse(reader["bid"] + "");
                f.bname = reader["bname"] + "";
                f.panalty = Decimal.Parse(reader["panalty"] + "");
                f.ftime = reader["ftime"] + "";

                fines.Add(f);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return fines;
        }
    }
}
