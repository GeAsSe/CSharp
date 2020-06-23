using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class UserBookDAL
    {
        public List<book> getHistoryBooks(int uid)
        {
            List<book> books = new List<book>();
            string sql = "select * from book natural join history where uid =" + uid + " and borrowtimes=0";
            MySqlDataReader reader = MysqlHelper.getReader(sql);

            while (reader.Read())
            {
                book b = new book();
                b.bname = reader["bname"] + "";
                b.isbn = reader["isbn"] + "";
                b.publisher = reader["publisher"] + "";
                b.author = reader["author"] + "";
                books.Add(b);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return books;
        }

        public List<MyBorrow> getNowBooks(int uid)
        {
            List<MyBorrow> books = new List<MyBorrow>();
            string sql = "select * from book natural join history where uid =" + uid + " and borrowtimes!=0";
            MySqlDataReader reader = MysqlHelper.getReader(sql);

            while (reader.Read())
            {
                MyBorrow b = new MyBorrow();
                b.bname = reader["bname"] + "";
                b.publisher = reader["publisher"] + "";
                b.author = reader["author"] + "";
                b.time = reader["time"] + "";
                b.borrowtime = int.Parse(reader["borrowtimes"].ToString());
                books.Add(b);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return books;
        }
    }
}
