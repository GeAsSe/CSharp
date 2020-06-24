using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using xxxxx.Interop;
using CLR1;
using System.Runtime.InteropServices;

namespace UI
{
    /// <summary>
    /// bookManage.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBook : Page
    {
        List<book> books;
        int uid;
        public SearchBook(string x)
        {
            uid = int.Parse(x);
            InitializeComponent();
            initView();
        }

        public void initView()
        {
            bookListView.Items.Clear();
            books = new bookBLL().getBooks();
            foreach (book b in books)
            {
                string status = "";
                switch (b.status)
                {
                    case 0:
                        status = "丢失";
                        break;
                    case 1:
                        status = "在馆";
                        
                        break;
                    case 2:
                        status = "预定";
                        break;
                    case 3:
                        status = "借出";
                        break;
                }
                bookListView.Items.Add(new
                {
                    c1 = b.isbn,
                    c2 = b.bname,
                    c3 = b.author,
                    c4 = b.publisher,
                    c5 = b.place,
                    c6 = b.price,
                    c7 = status,
                    c8=(status == "在馆"? Visibility.Visible: Visibility.Hidden)
                });
            }
        }

        private void searchbook()
        {
            string bname = txtName.Text;
            string isbn = txtIsbn.Text;
            string author = txtAuthor.Text;
            string publisher = txtPublisher.Text;
            int status = -1;
            Console.WriteLine(bname);
            bookListView.Items.Clear();
            books = new bookBLL().getBooks(bname, isbn, author, publisher, status);
            foreach (book b in books)
            {
                string statu = "";
                switch (b.status)
                {
                    case 0:
                        statu = "丢失";
                        break;
                    case 1:
                        statu = "在馆";
                        break;
                    case 2:
                        statu = "预定";
                        break;
                    case 3:
                        statu = "借出";
                        break;
                }
                bookListView.Items.Add(new
                {
                    c1 = b.isbn,
                    c2 = b.bname,
                    c3 = b.author,
                    c4 = b.publisher,
                    c5 = b.place,
                    c6 = b.price,
                    c7 = statu,
                    c8 = (statu == "在馆" ? Visibility.Visible : Visibility.Hidden)
                });
            }
        }
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            searchbook();
        }

        private void book_Click(object sender, RoutedEventArgs e)
        {
            var a = this.bookListView.SelectedItem;
            string b = (a.GetType().GetProperty("c1").GetValue(a, null)).ToString();
            new bookitBLL().bitit(uid, b);
            searchbook();
            Console.WriteLine(b);

            //使用C++/CLI
            int y=Class1.x(2);

            
            int r = MyAdd((int)9, (int)22);
            string p = Environment.CurrentDirectory;

            //使用COM
            OBJ o = new OBJ();
            o.push("预定成功！"+y.ToString()+" "+p+" "+r.ToString());
        }

        //使用win32dll
        [DllImport("PROJECT11.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MyAdd(int x, int y);

        private void Cboroom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtIsbn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BookListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}