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

namespace UI
{
    /// <summary>
    /// userManage.xaml 的交互逻辑
    /// </summary>
    public partial class userManage : Page
    {
        public userManage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            string s = btn.Name.ToString();
            switch (s)
            {
                case "borrowManage":
                    #region 按钮换图
                    BitmapImage imagetemp1 = new BitmapImage(new Uri("\\Images\\jieyue2.png", UriKind.Relative));
                    BitmapImage imagetemp2 = new BitmapImage(new Uri("\\Images\\card1.png", UriKind.Relative));
                    img1.Source = imagetemp1;
                    img2.Source = imagetemp2;
                    #endregion
                    userFrame.Source = new Uri("userBorrowManage.xaml", UriKind.Relative);
                    break;

                case "userAddManage":
                    #region 按钮换图
                    BitmapImage imagetemp11 = new BitmapImage(new Uri("\\Images\\jieyue1.png", UriKind.Relative));
                    BitmapImage imagetemp21 = new BitmapImage(new Uri("\\Images\\card2.png", UriKind.Relative));
                    img1.Source = imagetemp11;
                    img2.Source = imagetemp21;
                    #endregion
                    userFrame.Source = new Uri("userAddManage.xaml", UriKind.Relative);
                    break;
            }
        }
    }
}
