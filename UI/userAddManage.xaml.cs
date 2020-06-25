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
using Model;
using BLL;

namespace UI
{
    /// <summary>
    /// userAddManage.xaml 的交互逻辑
    /// </summary>
    public partial class userAddManage : Page
    {

        List<user> users;
        user uu;
        public userAddManage()
        {
            InitializeComponent();
            users = new userBLL().GetUsers();
            InitView();
        }

        public void InitView()
        {
            userListView.Items.Clear();
            foreach (user u in users)
            {
                userListView.Items.Add(new
                {
                    c1 = u.uid,
                    c2 = u.name
                });
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Uid.Text != "")
            {
                try
                {
                    int uid = int.Parse(Uid.Text);
                    uu = new user
                    {
                        uid = uid
                    };
                    uu = new userBLL().getUser(uu);
                    if (uu.uid == 0)
                    {
                        Tip.Content = "此学生不存在！";
                        return;
                    }
                    users.Clear();
                    users.Add(uu);
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
            Tip.Content = "";
            users = new userBLL().GetUsers();
            InitView();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int index = userListView.SelectedIndex;
            if (index == -1)
            {

                return;
            }
            MessageBoxResult dr = MessageBox.Show("确认删除" + users[index].name + "？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                bool result = new userBLL().deleteUser(users[index]);
                if (result == true)
                {
                    MessageBox.Show("删除成功!");
                    users = new userBLL().GetUsers();
                    InitView();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }
    }
}
