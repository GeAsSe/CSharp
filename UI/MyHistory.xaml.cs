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
namespace UI
{
    /// <summary>
    /// MyHistory.xaml 的交互逻辑
    /// </summary>
    public partial class MyHistory : Page
    {
        List<book> books;
        int uid;
        public MyHistory(string x)
        {
            uid = int.Parse(x);
            InitializeComponent();
            initView();
        }
        public void initView()
        {
            var listx = (System.Collections.Generic.List<string>)Tag;

            bookListView.Items.Clear();
            books = new userBook_BLL().getHistoryBooks(uid);
            foreach (book b in books)
            {
                
                bookListView.Items.Add(new
                {
                    c4 = b.isbn,
                    c1 = b.bname,
                    c2 = b.author,
                    c3 = b.publisher,
                });
            }
        }
    }
}
