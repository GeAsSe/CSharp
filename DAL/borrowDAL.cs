using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class borrowDAL
    {
        public int Borrow(user u, book b)
        {
            try
            {
                string cText1 = "select * from book where bid=" + b.bid;
                MySqlDataReader reader1 = MysqlHelper.getReader(cText1);
                reader1.Read();
                if (int.Parse(reader1["status"].ToString()) == 2 && int.Parse(reader1["reserve"].ToString()) != u.uid)
                {
                    reader1.Close();
                    MysqlHelper.conn.Close();
                    return 0;
                }
                reader1.Close();
                MysqlHelper.conn.Close();
                string cText2 = "select * from history where borrowtimes!=0 and borrowtimes!=-1 and uid=" + u.uid;
                MySqlDataReader reader2= MysqlHelper.getReader(cText2);
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
                while (reader2.Read())
                {
                    if(DateTime.Parse(nowDate) > DateTime.Parse(reader2["time"].ToString()))
                    {
                        reader2.Close();
                        MysqlHelper.conn.Close();
                        return 1;
                    }
                }
                reader2.Close();
                MysqlHelper.conn.Close();
                string cText3 = "insert into history(uid, name, bid, bname, time, borrowtimes) values (@uid, @name, @bid, @bname, @time, 1)";
                string time = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                MySqlParameter Uid = new MySqlParameter("@uid", MySqlDbType.Int32);
                MySqlParameter Name = new MySqlParameter("@name", MySqlDbType.VarChar);
                MySqlParameter Bid = new MySqlParameter("@bid", MySqlDbType.Int32);
                MySqlParameter Bname = new MySqlParameter("@bname", MySqlDbType.VarChar);
                MySqlParameter Time = new MySqlParameter("@time", MySqlDbType.VarChar);

                Uid.Value = u.uid;
                Name.Value = u.name;
                Bid.Value = b.bid;
                Bname.Value = b.bname;
                Time.Value = time;

                MySqlParameter[] sp = { Uid, Name, Bid, Bname, Time };
                MysqlHelper.ExecuteNonQueryProc(cText3, sp);
                string cText4 = "update book set `status`=3 where bid=" + b.bid;
                MysqlHelper.ExecuteNonQueryProc(cText4);
                return 2;
            }
            catch
            {
                return -1;
            }
        }
    }
}
