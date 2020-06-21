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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UI
{
    /// <summary>
    /// bookManage.xaml 的交互逻辑
    /// </summary>
    public partial class bookManage : Page
    {
       
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cboroom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window a = new bookAddition();
            a.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window a = new bookChange();
            a.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window a = new bookBorrow();
            a.ShowDialog();
        }
    }
}
