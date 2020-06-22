using BLL;
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
using System.Windows.Threading;

namespace UI
{
    /// <summary>
    /// MainUnit.xaml 的交互逻辑
    /// </summary>
    public partial class MainUnit : Window
    {
        private DispatcherTimer ShowTimer;  //声明一个计时器

        public MainUnit()
        {
            InitializeComponent();
            FullScreenManager.RepairWpfWindowFullScreenBehavior(this); //最大化时，窗口不遮挡任务栏
            Loaded += delegate
            {
                //显示当前用户名
                lblUserName.Content = this.Tag + ""; 
                //设置计时器
                ShowTimer = new DispatcherTimer();
                ShowTimer.Tick += new EventHandler(ShowCurrentTimer);
                ShowTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                ShowTimer.Start();
            };
        }

        //实时显示日期时间
        private void ShowCurrentTimer(object sender, EventArgs e)
        {
            string date = DateTime.Now.DayOfWeek.ToString();  //当前日期
            if (date == "Monday")        { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期一"; }
            else if (date == "Tuesday")  { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期二"; }
            else if (date == "Wednesday"){ lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期三"; }
            else if (date == "Thursday") { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期四"; }
            else if (date == "Friday")   { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期五"; }
            else if (date == "Saturday") { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期六"; }
            else if (date == "Sunday")   { lblDate.Content = DateTime.Now.ToLongDateString().ToString() + " 星期日"; }
            lblTime.Content = DateTime.Now.ToString("HH:mm:ss");  //当前时间
        }

        //"退出系统"按钮事件
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //窗口移动事件
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
         
        //左箭头按钮点击事件
        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            var top = choicePanel.Margin.Top;
            var bot = choicePanel.Margin.Bottom;
            var left = choicePanel.Margin.Left;
            var right = choicePanel.Margin.Right;
            choicePanel.Margin = new Thickness(left, top, right+5, bot);
            gridLeft.Margin = new Thickness(gridLeft.Margin.Left, gridLeft.Margin.Top,
                                            gridLeft.Margin.Right-5, gridLeft.Margin.Bottom);
        }
        //右箭头按钮点击事件
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            var top = choicePanel.Margin.Top;
            var bot = choicePanel.Margin.Bottom;
            var left = choicePanel.Margin.Left;
            var right = choicePanel.Margin.Right;
            choicePanel.Margin = new Thickness(left + 5, top, right, bot);
            gridRight.Margin = new Thickness(gridRight.Margin.Left - 5, gridRight.Margin.Top,
                                             gridRight.Margin.Right, gridRight.Margin.Bottom);
        }
        //功能选项区按钮点击事件
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            
            string func = btn.Content.ToString();
            switch (func)
            {
                case "图书管理":
                    #region 按钮变色
                    btn.Foreground = Brushes.LightSkyBlue;
                    btnUser.Foreground = Brushes.MidnightBlue;
                    btnFine.Foreground = Brushes.MidnightBlue;
                    
                    #endregion
                    showFrame.Source = new Uri("bookManage.xaml", UriKind.Relative);
                    break;

                case "用户管理":
                    #region 按钮变色
                    btn.Foreground = Brushes.LightSkyBlue;
                    btnBook.Foreground = Brushes.MidnightBlue;
                    btnFine.Foreground = Brushes.MidnightBlue;

                    #endregion
                    showFrame.Source = new Uri("userManage.xaml", UriKind.Relative);
                    break;

                case "罚单管理":
                    #region 按钮变色
                    btn.Foreground = Brushes.LightSkyBlue;
                    btnBook.Foreground = Brushes.MidnightBlue;
                    btnUser.Foreground = Brushes.MidnightBlue;

                    //BitmapImage imagetemp12 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp22 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp32 = new BitmapImage(new Uri("\\Images\\Btn+.png", UriKind.Relative));
                    //BitmapImage imagetemp42 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp52 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp62 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp72 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //BitmapImage imagetemp82 = new BitmapImage(new Uri("\\Images\\Btn-.png", UriKind.Relative));
                    //img1.ImageSource = imagetemp12;
                    //img2.ImageSource = imagetemp22;
                    //img3.ImageSource = imagetemp32;
                    //img4.ImageSource = imagetemp42;
                    //img5.ImageSource = imagetemp52;
                    //img6.ImageSource = imagetemp62;
                    //img7.ImageSource = imagetemp72;
                    //img8.ImageSource = imagetemp82;
                    #endregion
                    showFrame.Source = new Uri("fineManage.xaml", UriKind.Relative);
                    break;
            }
        }
        //最大化
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                BitmapImage imagetemp1 = new BitmapImage(new Uri("\\Images\\RE.png", UriKind.Relative));
                imgRE.Source = imagetemp1;
                btnMax.ToolTip = "向下还原";
            }
            else  //还原
            {
                WindowState = WindowState.Normal;
                BitmapImage imagetemp2 = new BitmapImage(new Uri("\\Images\\MAX.png", UriKind.Relative));
                imgRE.Source = imagetemp2;
                btnMax.ToolTip = "最大化";
            }
        }
        //最小化
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
