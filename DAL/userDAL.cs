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

        public user getUser(user u)
        {
            string cText = "select * from user where admin=0 and uid=" + u.uid.ToString();
            MySqlDataReader reader = MysqlHelper.getReader(cText);
            if(reader.Read() == false)
            {
                user uu = new user
                {
                    uid = 0,
                    name = ""
                };
                return uu;
            }
            user uuu = new user
            {
                uid = int.Parse(reader["uid"].ToString()),
                name = reader["name"].ToString()
            };
            MysqlHelper.conn.Close();
            reader.Close();
            return uuu;
        }

        public List<user> GetUsers()
        {
            string cText = "select * from user where admin=0";
            MySqlDataReader reader = MysqlHelper.getReader(cText);
            List<user> users = new List<user>();
            while (reader.Read())
            {
                user u = new user();
                u.uid = int.Parse(reader["uid"] + "");
                u.name = reader["name"] + "";
                users.Add(u);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return users;

        }

        public bool deleteUser(user u)
        {
            string cText = "delete from user where uid=" + u.uid;
            return MysqlHelper.ExecuteNonQueryProc(cText);
        }

        public bool insertUser(int uid, string name, string password)
        {
            string cText = "insert into user(uid, name, password, admin) values (@uid, @name, @password, 0)";
            MySqlParameter Uid = new MySqlParameter("@uid", MySqlDbType.Int32);
            MySqlParameter Name = new MySqlParameter("@name", MySqlDbType.VarChar);
            MySqlParameter Password = new MySqlParameter("@password", MySqlDbType.VarChar);

            Uid.Value = uid;
            Name.Value = name;
            Password.Value = password;

            MySqlParameter[] sp = { Uid, Name, Password};
            return MysqlHelper.ExecuteNonQueryProc(cText, sp);

        }
    }
}
