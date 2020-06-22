using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Model;

namespace DAL
{
    public class userDAL
    {
        public int Check(user u)
        {
            int uid = u.uid;
            string pwd = u.password;
            return new MysqlHelper().LoginCheck(uid, pwd);
        }

        public string getName(user u)
        {
            string cText = "select * from user where uid = " + u.uid.ToString();
            Console.WriteLine(cText);
            MySqlDataReader reader = MysqlHelper.getReader(cText);
            reader.Read();
            string name = reader["name"].ToString();
            MysqlHelper.conn.Close();
            reader.Close();
            return name;
        }
    }
}
