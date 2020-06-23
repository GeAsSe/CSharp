using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    public delegate void updateBookHandler();
    
    /// <summary>
    /// bookChange.xaml 的交互逻辑
    /// </summary>
    public partial class bookChange : Window
    {
        private book changebook;
        public event updateBookHandler updateBookEvent;

        public bookChange(book b)
        {
            InitializeComponent();
            changebook = b;
            txtBname.Text = b.bname;
            txtIsbn.Text = b.isbn;
            txtAuthor.Text = b.author;
            txtPublisher.Text = b.publisher;
            txtPlace.Text = b.place;
            txtPrice.Text = b.price.ToString();
            comboStatus.SelectedIndex = b.status;
            comboStatus.IsEnabled = false;

        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            int bid = changebook.bid;
            string bname = txtBname.Text;
            string author = txtAuthor.Text;
            string isbn = txtIsbn.Text;
            string publisher = txtPublisher.Text;
            string place = txtPlace.Text;
            string price = txtPrice.Text;

            if (bname != "" && author != "" && isbn != "" && publisher != "" && place != "" && price != "")
            {
                try
                {
                    Decimal bprice = Decimal.Parse(price);
                    bool result = new bookBLL().updateBook(bid, bname, isbn, author, publisher, place, bprice);
                    if (result == false)
                    {
                        MessageBox.Show("修改信息失败。");
                    }
                    else
                    {
                        //writelock.Set();
                        this.Close();
                        MessageBox.Show("修改信息成功！");
                        updateBookEvent();
                    }
                }
                catch
                {
                    Tip.Content = "价格输入错误！";
                }
            }
            else
            {
                Tip.Content = "输入信息不能为空！";
            }
        }
        

    }
}
