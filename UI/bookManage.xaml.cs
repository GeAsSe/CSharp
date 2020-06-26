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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using xxxxx.Interop;

namespace UI
{
    /// <summary>
    /// bookManage.xaml 的交互逻辑
    /// </summary>
    public partial class bookManage : Page
    {
        public static AutoResetEvent writelock = new AutoResetEvent(true);
        List<book> books;
        public bookManage()
        {
            InitializeComponent();
            initView();
        }

        public void initView()
        {
            bookListView.Items.Clear();
            books = new bookBLL().getBooks();
            foreach (book b in books)
            {
                string status = "";
                switch (b.status)
                {
                    case 0:
                        status = "丢失";
                        break;
                    case 1:
                        status = "在馆";
                        break;
                    case 2:
                        status = "预定";
                        break;
                    case 3:
                        status = "借出";
                        break;
                }
                bookListView.Items.Add(new
                {
                    c1 = b.isbn,
                    c2 = b.bname,
                    c3 = b.author,
                    c4 = b.publisher,
                    c5 = b.place,
                    c6 = b.price,
                    c7 = status
                });
            }
        }

        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string bname = txtName.Text;
            string isbn = txtIsbn.Text;
            string author = txtAuthor.Text;
            string publisher = txtPublisher.Text;
            int status = 0;
            if(comboStatus.SelectedItem == null)
            {
                status = -1;
            }
            else
            {
                status = comboStatus.SelectedIndex;
            }
            Console.WriteLine(bname);
            bookListView.Items.Clear();
            books = new bookBLL().getBooks(bname, isbn, author, publisher, status);
            foreach (book b in books)
            {
                string statu = "";
                switch (b.status)
                {
                    case 0:
                        statu = "丢失";
                        break;
                    case 1:
                        statu = "在馆";
                        break;
                    case 2:
                        statu = "预定";
                        break;
                    case 3:
                        statu = "借出";
                        break;
                }
                bookListView.Items.Add(new
                {
                    c1 = b.isbn,
                    c2 = b.bname,
                    c3 = b.author,
                    c4 = b.publisher,
                    c5 = b.place,
                    c6 = b.price,
                    c7 = statu
                });
            }

        }

        private void Cboroom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bookAddition a = new bookAddition();
            a.addBookEvent += new AddBookHandler(initView);
            a.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int index = bookListView.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            bookChange a = new bookChange(books[index]);
            a.updateBookEvent += new updateBookHandler(initView);
            a.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Task t = new Task(() => OpenBorrow());
            t.Start();
        }

        public void OpenBorrow()
        {
            writelock.WaitOne();
            Dispatcher.Invoke((Action)(() =>
            {
                int index = bookListView.SelectedIndex;
                if (index == -1 || books[index].status == 0 || books[index].status == 3)
                {
                    writelock.Set();
                    return;
                }
                bookBorrow a = new bookBorrow(books[index]);
                a.borrowBookEvent += new borrowBookHandler(initView);
                a.Show();
            }));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int index = bookListView.SelectedIndex;
            if (index == -1 || books[index].status == 3)
            {
                if (index != -1)
                {
                    MessageBox.Show("此书不可删除。");
                }
                return;
            }
            MessageBoxResult dr = MessageBox.Show("确认删除"+books[index].bname+"？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                bool result = new bookBLL().deleteBook(books[index]);
                if(result == true)
                {
                    MessageBox.Show("删除成功!");
                    initView();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if(e.KeyStates == Keyboard.GetKeyStates(Key.Enter))
            {
                btnSearch_Click(sender, e);
            }
        }

        [DllImport("PROJECT11.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr readbook(IntPtr a);

        private void Button_Click_4(object sender, RoutedEventArgs e)
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
                    string[] bookstrs = resultstr.Split(';');
                    foreach (string bookstr in bookstrs)
                    {
                        if( bookstr == "" ){
                            break;
                        }
                        string[] bstr = bookstr.Split(',');
                        string bname = bstr[0];
                        string isbn = bstr[1];
                        string author = bstr[2];
                        string publisher = bstr[3];
                        Decimal bprice = Decimal.Parse(bstr[4]);
                        string place = bstr[5];
                        bool result = new bookBLL().insertBook(bname, isbn, author, publisher, place, bprice);
                        // Console.WriteLine(bookstr);
                    }
                    MessageBox.Show("导入成功!");
                    initView();
                }
                catch
                {
                    MessageBox.Show("导入失败!");
                }
                

            }
        }
    }
}
