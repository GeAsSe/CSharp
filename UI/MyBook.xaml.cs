using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BLL;
using Model;
using xxxxx.Interop;
namespace UI
{
    /// <summary>
    /// MyBook.xaml 的交互逻辑
    /// </summary>
    public partial class MyBook : Page
    {
        int uid;
        List<MyBorrow> books = new List<MyBorrow>();
        public MyBook(string x)
        {
            uid = int.Parse(x);
            InitializeComponent();
            initView();
            
        }
        public void initView()
        {
            bookListView.Items.Clear();
            books = new userBook_BLL().GetMyBorrows(uid);
            foreach (MyBorrow b in books)
            {
                string status = "";
                if (b.borrowtime == -1)
                {
                    status = "丢失";
                }
                else if (DateTime.Compare(DateTime.Now, Convert.ToDateTime(b.time)) > 0)
                {
                    status = "已超出还书期限";
                }
                else
                {
                    switch (b.borrowtime)
                    {
                        case 1:
                            status = "初次借阅";
                            break;
                        case 2:
                            status = "已经续借";
                            break;
                    }
                }
                bookListView.Items.Add(new
                {
                    c1 = b.bname,
                    c2 = b.author,
                    c3 = b.publisher,
                    c4 = b.time,
                    c5=status,
                    c6= (status == "初次借阅" ? Visibility.Visible : Visibility.Collapsed),
                    c7 = (status == "丢失" ? Visibility.Collapsed : Visibility.Visible)
                });
            }
        }

        private void order(object sender, RoutedEventArgs e)
        {
            var a = this.bookListView.SelectedItem;
            string b = (a.GetType().GetProperty("c1").GetValue(a, null)).ToString();
            string c = (a.GetType().GetProperty("c4").GetValue(a, null)).ToString();
            new bookitBLL().order(b, uid, c);
            initView();
            OBJ o = new OBJ();
            o.push("续借成功！");
        }

        public void lose(object sender, RoutedEventArgs e)
        {
            var a = this.bookListView.SelectedItem;
            string b = (a.GetType().GetProperty("c1").GetValue(a, null)).ToString();
            new bookitBLL().lose(uid, b);
            initView();
            OBJ o = new OBJ();
            o.push("挂失成功！");
        }
        private void BookListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
