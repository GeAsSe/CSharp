using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;

namespace BLL
{
    public class bookBLL
    {
        public List<book> getBooks()
        {
            return new bookDAL().getBooks();
        }

        public List<book> getBooks(string bname, string isbn, string author, string publisher, int status)
        {
            return new bookDAL().getBooks(bname, isbn, author, publisher, status);
        }

        public bool insertBook(string bname, string isbn, string author, string publisher, string place, Decimal price)
        {
            return new bookDAL().insertBook(bname, isbn, author, publisher, place, price);
        }

        public bool updateBook(int bid, string bname, string isbn, string author, string publisher, string place, Decimal price)
        {
            return new bookDAL().updateBook(bid, bname, isbn, author, publisher, place, price);
        }

        public bool deleteBook(book b)
        {
            return new bookDAL().deleteBook(b);
        }

        public book GetBook(int bid)
        {
            return new bookDAL().GetBook(bid);
        }
    }
}
