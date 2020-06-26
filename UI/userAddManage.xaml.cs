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
using System.Runtime.InteropServices;
using xxxxx.Interop;

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

        [DllImport("PROJECT11.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr readbook(IntPtr a);

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = ".csv|*.csv";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string filename = dialog.FileName;
                    IntPtr ptrIn = Marshal.StringToHGlobalAnsi(filename);
                    IntPtr ptr = readbook(ptrIn);
                    string resultstr = Marshal.PtrToStringAnsi(ptr);
                    string[] userstrs = resultstr.Split(';');
                    foreach (string userstr in userstrs)
                    {
                        if (userstr == "")
                        {
                            break;
                        }
                        string[] ustr = userstr.Split(',');
                        int uid = int.Parse(ustr[0]);
                        string name = ustr[1];
                        string password = ustr[2];
                    
                        bool result = new userBLL().insertUser(uid, name, password);
                        // Console.WriteLine(bookstr);
                    }
                    OBJ o = new OBJ();
                    o.push("导入成功!");
                    users = new userBLL().GetUsers();
                    InitView();
                }
                catch
                {
                    OBJ o = new OBJ();
                    o.push("导入失败，请检查文件后重试!");
                }


            }
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
                    OBJ o = new OBJ();
                    o.push("删除成功!");
                    users = new userBLL().GetUsers();
                    InitView();
                }
                else
                {
                    OBJ o = new OBJ();
                    o.push("删除失败！");
                }
            }
        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyStates == Keyboard.GetKeyStates(Key.Enter))
            {
                Button_Click(sender, e);
            }
        }
    }
}
