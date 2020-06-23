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
using System.Reflection;
using BLL;
using Model;
using System.ComponentModel;

namespace UI
{
    public delegate void borrowBookHandler();

    /// <summary>
    /// bookBorrow.xaml 的交互逻辑
    /// </summary>
    public partial class bookBorrow : Window
    {
        public event borrowBookHandler borrowBookEvent;

        private book bb;
        private user u;
        public bookBorrow(book b)
        {
            InitializeComponent();
            bb = b;
            BookView.Items.Add(new
            {
                c1 = b.bid,
                c2 = b.isbn,
                c3 = b.bname,
                c4 = b.author,
                c5 = b.publisher,
            });
            u = new user
            {
                uid = 0,
                name=""
            };
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnChoose_Click(object sender, RoutedEventArgs e)
        {
            if(Uid.Text == "")
            {
                Tip.Content = "请输入借阅者学号！";
                return;
            }
            try
            {
                int uid = int.Parse(Uid.Text);
                Console.WriteLine(Uid.Text);
                u.uid = uid;
                u = new userBLL().getUser(u);
                if (u.uid == 0)
                {
                    Tip.Content = "此学生不存在！";
                    return;
                }
                Uname.Content = u.name;
                Tip.Content = "";
            }
            catch
            {
                Tip.Content = "请输入正确的学号！";
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(u.name == "")
            {
                return;
            }
            int result = new borrowBLL().Borrow(u,bb);
            if(result == -1)
            {
                MessageBox.Show("借阅失败，系统错误。");
            }
            else if(result == 0)
            {
                MessageBox.Show("借阅失败,书已被预定。");
            }
            else if(result == 1)
            {
                MessageBox.Show("借阅失败,有书未归还。");
            }
            else if(result == 2)
            {
                borrowBookEvent();
                MessageBox.Show("借阅成功!");
                this.Close();
                bookManage.writelock.Set();
            }
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            bookManage.writelock.Set();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            bookManage.writelock.Set();
            base.OnClosing(e);
        }
    }
}
