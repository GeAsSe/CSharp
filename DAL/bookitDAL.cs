using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class bookitDAL
    {
        public bool bitit(int userId, string Isbn)
        {
            string cText = "update book set reserve=@userid,status=2 where isbn=@isbn";
            MySqlParameter userid = new MySqlParameter("@userid", MySqlDbType.Int32);
            MySqlParameter isbn = new MySqlParameter("@isbn", MySqlDbType.VarChar);
            userid.Value = userId;
            isbn.Value = Isbn;
            MySqlParameter[] sp = { userid, isbn };
            return MysqlHelper.ExecuteNonQueryProc(cText, sp);
        }


        public bool order(string bname,int userId,DateTime time)
        {
            string cText = "update history set time=@time,borrowtimes=2 where bname=@bname and uid =@uid and borrowtimes=1";
            MySqlParameter Time = new MySqlParameter("@time", MySqlDbType.VarChar);
            MySqlParameter Bname = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter UID = new MySqlParameter("@uid", MySqlDbType.Int32);
            Time.Value = time.ToString("yyyy-MM-dd");
            Bname.Value = bname;
            UID.Value = userId;
            MySqlParameter[] sp = { Time, Bname,UID };
            return MysqlHelper.ExecuteNonQueryProc(cText, sp);
        }

        public bool lose(int uid, string bname,string time)
        {
            string s1 = "update history set borrowtimes=-1 where bname=@bname and uid =@uid and borrowtimes>0";
            MySqlParameter Bname = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter UID = new MySqlParameter("@uid", MySqlDbType.Int32);
            Bname.Value = bname;
            UID.Value = uid;
            MySqlParameter[] sp1 = { Bname, UID };
            MysqlHelper.ExecuteNonQueryProc(s1, sp1);

            string s2= "update book set status=0 where bid in (select DISTINCT bid from history where bname=@bname and uid =@uid and borrowtimes=-1 )";
            MySqlParameter Bname1 = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter UID1 = new MySqlParameter("@uid", MySqlDbType.Int32);
            Bname1.Value = bname;
            UID1.Value = uid;
            MySqlParameter[] sp2 = { Bname1, UID1 };
            MysqlHelper.ExecuteNonQueryProc(s2, sp2);

            string s3 = "insert into fine(uid,name,bid,bname,panalty,ftime) " +
                "values(@uid,(select DISTINCT name from history where bname=@bname and uid =@uid and borrowtimes=-1),(select DISTINCT bid from history where bname=@bname and uid =@uid and borrowtimes=-1),@bname," +
                "(select DISTINCT price from history natural join book where bname=@bname and uid =@uid and status=0 and borrowtimes=-1),@time)";
            MySqlParameter Bname2 = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter UID2 = new MySqlParameter("@uid", MySqlDbType.Int32);
            MySqlParameter Time = new MySqlParameter("@time", MySqlDbType.VarChar);
            Bname2.Value = bname;
            UID2.Value = uid;
            Time.Value = time;
            MySqlParameter[] sp3 = { Bname2, UID2,Time };
            MysqlHelper.ExecuteNonQueryProc(s3, sp3);

            return true;
        }
    }

    
}
