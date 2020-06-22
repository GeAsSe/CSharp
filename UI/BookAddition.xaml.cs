using BLL;
using Model;
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

namespace UI
{
    /// <summary>
    /// bookAddition.xaml 的交互逻辑
    /// </summary>
    /// 


    public delegate void AddBookHandler();
    public partial class bookAddition : Window
    {
        public bookAddition()
        {
            InitializeComponent();
        }


        public event AddBookHandler addBookEvent;

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string bname = txtBname.Text;
            string author = txtAuthor.Text;
            string isbn = txtIsbn.Text;
            string publisher = txtPublisher.Text;
            string place = txtPlace.Text;
            string price = txtPrice.Text;

            if(bname!="" && author!="" && isbn!="" && publisher!="" && place!="" && price != "")
            {
                try
                {
                    Decimal bprice = Decimal.Parse(price);
                    bool result = new bookBLL().insertBook(bname, isbn, author, publisher, place, bprice);
                    if(result == false)
                    {
                        MessageBox.Show("插入数据失败。");
                    }
                    else
                    {
                        this.Close();
                        MessageBox.Show("添加书籍成功！");
                        addBookEvent();
                    }
                }
                catch
                {
                    Tip.Content = "请输入正确的价格！";
                }
            }
            else
            {
                Tip.Content = "请填写所有的信息！";
            }

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtBname.Text = "";
            txtAuthor.Text = "";
            txtIsbn.Text = "";
            txtPublisher.Text = "";
            txtPlace.Text = "";
            txtPrice.Text = "";
        }
    }
}
