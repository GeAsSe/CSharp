using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class bookDAL
    {
        public List<book> getBooks()
        {
            List<book> books = new List<book>();
            MySqlDataReader reader = MysqlHelper.getReader("select * from book");
            while (reader.Read())
            {
                book b = new book();
                b.bid = int.Parse(reader["bid"] + "");
                b.bname = reader["bname"] + "";
                b.isbn = reader["isbn"] + "";
                b.price = (Decimal)reader["price"];
                b.publisher = reader["publisher"] + "";
                b.author = reader["author"] + "";
                b.place = reader["place"] + "";
                b.status = int.Parse(reader["status"] + "");
                if (reader["reserve"].ToString() == "")
                {
                    b.reserve = 0;
                }
                else
                {
                    b.reserve = int.Parse(reader["reserve"] + "");
                }
                books.Add(b);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return books;
        }

        public List<book> getBooks(string bname, string isbn, string author, string publisher, int status)
        {
            StringBuilder cText = new StringBuilder("select * from book");
            List<string> wheres = new List<string>();
            if (bname != "")
            {
                wheres.Add(" bname like '%" + bname+"%'");
            }
            if (isbn != "")
            {
                wheres.Add(" isbn like '%" + isbn+"%'");
            }
            if (author != "")
            {
                wheres.Add(" author like '%" + author+"%'");
            }
            if (publisher != "")
            {
                wheres.Add(" publisher like '%" + publisher+"%'");
            }
            if (status != -1)
            {
                wheres.Add(" `status`=" + status);
            }

            if (wheres.Count > 0)
            {
                string wh = string.Join(" and ", wheres.ToArray());
                cText.Append(" where " + wh);
            }
            List<book> books = new List<book>();
            MySqlDataReader reader = MysqlHelper.getReader(cText.ToString());

            while (reader.Read())
            {
                book b = new book();
                b.bid = int.Parse(reader["bid"] + "");
                b.bname = reader["bname"] + "";
                b.isbn = reader["isbn"] + "";
                b.price = (Decimal)reader["price"];
                b.publisher = reader["publisher"] + "";
                b.author = reader["author"] + "";
                b.place = reader["place"] + "";
                b.status = int.Parse(reader["status"] + "");
                if (reader["reserve"].ToString() == "")
                {
                    b.reserve = 0;
                }
                else
                {
                    b.reserve = int.Parse(reader["reserve"] + "");
                }
                books.Add(b);
            }
            MysqlHelper.conn.Close();
            reader.Close();
            return books;
        }

        public bool insertBook(string bname, string isbn, string author, string publisher, string place, Decimal price)
        {
            string cText = "insert into book(bname, isbn, author, publisher, place, price, `status`) values (@bname, @isbn, @author, @publisher, @place, @price, 1)";
            MySqlParameter Bname = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter Isbn = new MySqlParameter("@isbn", MySqlDbType.VarChar);
            MySqlParameter Author = new MySqlParameter("@author", MySqlDbType.VarChar);
            MySqlParameter Publisher = new MySqlParameter("@publisher", MySqlDbType.VarChar);
            MySqlParameter Place = new MySqlParameter("@place", MySqlDbType.VarChar);
            MySqlParameter Price = new MySqlParameter("@price", MySqlDbType.Decimal);

            Bname.Value = bname;
            Isbn.Value = isbn;
            Author.Value = author;
            Publisher.Value = publisher;
            Place.Value = place;
            Price.Value = price;

            MySqlParameter[] sp = { Bname, Isbn, Author, Publisher, Place, Price };

            return MysqlHelper.ExecuteNonQueryProc(cText, sp);
        }

        public bool updateBook(int bid, string bname, string isbn, string author, string publisher, string place, Decimal price)
        {
            string cText = "update table book set bname=@bname, isbn=@isbn, author=@auther, publisher=@publisher, place=@place, price=@price where bid=@bid";
            MySqlParameter Bname = new MySqlParameter("@bname", MySqlDbType.VarChar);
            MySqlParameter Isbn = new MySqlParameter("@isbn", MySqlDbType.VarChar);
            MySqlParameter Author = new MySqlParameter("@author", MySqlDbType.VarChar);
            MySqlParameter Publisher = new MySqlParameter("@publisher", MySqlDbType.VarChar);
            MySqlParameter Place = new MySqlParameter("@place", MySqlDbType.VarChar);
            MySqlParameter Price = new MySqlParameter("@price", MySqlDbType.Decimal);
            MySqlParameter Bid = new MySqlParameter("@bid", MySqlDbType.Int32);


            Bname.Value = bname;
            Isbn.Value = isbn;
            Author.Value = author;
            Publisher.Value = publisher;
            Place.Value = place;
            Price.Value = price;
            Bid.Value = bid;

            MySqlParameter[] sp = { Bname, Isbn, Author, Publisher, Place, Price, Bid };
            return MysqlHelper.ExecuteNonQueryProc(cText, sp);
        }

    }
}


