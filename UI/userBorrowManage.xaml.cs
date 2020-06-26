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
using BLL;
using Model;
using CLR1;
using System.Runtime.InteropServices;
using xxxxx.Interop;


namespace UI
{
    /// <summary>
    /// userBorrowManage.xaml 的交互逻辑
    /// </summary>
    public partial class userBorrowManage : Page
    {

        List<history> histories;
        user u;
        public userBorrowManage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = borrowListView.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            userFine a = new userFine(histories[index]);
            a.lossBookEvent += new lossBookHandler(InitView);
            a.ShowDialog();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            int index = borrowListView.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            bool result = new borrowBLL().returnBook(histories[index].uid, histories[index].bid, histories[index].borrowtime);
            InitView();
        }

        private unsafe void Button_Click3(object sender, RoutedEventArgs e)
        {
            int index = borrowListView.SelectedIndex;
            if (index == -1 || histories[index].borrowtime==2)
            {
                if(index != -1)
                {
                    OBJ o = new OBJ();
                    o.push("此书已经不能再续借！");
                }
                return;
            }

            IntPtr intPtrStr1 = (IntPtr)Marshal.StringToHGlobalAnsi(histories[index].time);
            sbyte* sbyteStr1 = (sbyte*)intPtrStr1;
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            IntPtr intPtrStr2 = (IntPtr)Marshal.StringToHGlobalAnsi(nowDate);
            sbyte* sbyteStr2 = (sbyte*)intPtrStr2;

            if(Class1.DateMinus(sbyteStr1, sbyteStr2) > 15)
            {
                OBJ o = new OBJ();
                o.push("只能在还期15天内续借！");
                return;
            }            

            bool result = new borrowBLL().renewBook(histories[index].uid, histories[index].bid, histories[index].borrowtime, histories[index].time);
            if(result == false)
            {
                OBJ o = new OBJ();
                o.push("有逾期行为，不能续借！");
            }
            else
            {
                OBJ o = new OBJ();
                o.push("续借成功！");
                InitView();
            }
            
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            if(Uid.Text != "")
            {
                try
                {
                    int uid = int.Parse(Uid.Text);
                    u = new user
                    {
                        uid = uid
                    };
                    u = new userBLL().getUser(u);
                    if (u.uid == 0)
                    {
                        Tip.Content = "此学生不存在！";
                        return;
                    }
                    
                    InitView();
                    Tip.Content = "";
                    return;
                }
                catch
                {
                    Tip.Content = "学号格式错误!";
                    return;
                }
            }
            Tip.Content = "请输入学号！";
        }

        private void InitView()
        {
            histories = new borrowBLL().getHistories(u);
            string nowDate = DateTime.Now.ToString("yyyy-MM-dd");
            borrowListView.Items.Clear();
            foreach (history h in histories)
            {
                string xujie = "";
                if (h.borrowtime == 1)
                {
                    xujie = "可续借";
                }
                else if (h.borrowtime == 2)
                {
                    xujie = "不可续借";
                }

                string yuqi = "";
                if (DateTime.Parse(nowDate) > DateTime.Parse(h.time.ToString()))
                {
                    yuqi = "已逾期";
                }
                else
                {
                    yuqi = "未逾期";
                }

                borrowListView.Items.Add(new
                {
                    c1 = h.bid,
                    c2 = h.bname,
                    c3 = xujie,
                    c4 = h.time,
                    c5 = yuqi,
                });
            }
        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Enter))
            {
                Button_Search(sender, e);
            }
        }
    }
}
