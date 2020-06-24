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

    public delegate void lossBookHandler();
    /// <summary>
    /// userFine.xaml 的交互逻辑
    /// </summary>
    public partial class userFine : Window
    {
        history hh;
        book b;
        public event lossBookHandler lossBookEvent;

        public userFine(history h)
        {
            b = new bookBLL().GetBook(h.bid);
            InitializeComponent();
            hh = h;
            txtBname.Text = h.bname;
            txtBname.IsEnabled = false;
            txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtTime.IsEnabled = false;
            txtUid.Text = h.uid.ToString();
            txtUid.IsEnabled = false;
            txtName.Text = h.name;
            txtName.IsEnabled = false;
            txtFine.Text = b.price.ToString();
            txtFine.IsEnabled = false;
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            bool result =  new borrowBLL().lossBook(hh, b);
            if(result == false)
            {
                MessageBox.Show("挂失失败!");
            }
            else
            {
                MessageBox.Show("挂失成功");
                lossBookEvent();
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
