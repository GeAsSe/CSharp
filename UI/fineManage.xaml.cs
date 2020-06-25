using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// fineManage.xaml 的交互逻辑
    /// </summary>
    public partial class fineManage : Page
    {

        List<fine> fines;
        public fineManage()
        {
            InitializeComponent();
            fines = new fineBLL().GetFines();
            initView();
        }

        public void initView()
        {
            fineListView.Items.Clear();
            foreach (fine f in fines)
            {
                fineListView.Items.Add(new
                {
                    c1 = f.uid,
                    c2 = f.name,
                    c3 = f.bname,
                    c4 = f.panalty,
                    c5 = f.ftime
                });
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Uid.Text !="" || startTime.SelectedDate != null || endTime.SelectedDate != null)
            {
                try
                {
                    if(startTime.SelectedDate != null && endTime != null && startTime.SelectedDate > endTime.SelectedDate)
                    {
                        Tip.Content = "起止时间的设定错误!";
                        return;
                    }

                    int uid = 0;
                    string sDate = "";
                    string eDate = "";

                    if(Uid.Text != "")
                    {
                        uid = int.Parse(Uid.Text);
                        user u = new user
                        {
                            uid = uid
                        };
                        u = new userBLL().getUser(u);
                        if (u.uid == 0)
                        {
                            Tip.Content = "学号不存在！";
                            return;
                        }
                    }
                    if (startTime.SelectedDate != null)
                    {
                        sDate = DateTime.Parse(startTime.SelectedDate.ToString()).ToString("yyyy-MM-dd");
                    }
                    if (endTime.SelectedDate != null)
                    {
                        eDate = DateTime.Parse(endTime.SelectedDate.ToString()).ToString("yyyy-MM-dd");
                    }

                    
                    fines = new fineBLL().GetFines(uid, sDate, eDate);
                    Tip.Content = "";
                    initView();
                    return;
                }
                catch
                {
                    Tip.Content = "学号格式错误!";
                    return;
                }
            }
            Tip.Content = "";
            fines = new fineBLL().GetFines();
            initView();
        }
    }
}
