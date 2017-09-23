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
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace WpfApp1.WindowList
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
 //       public Connector connector;
        private bool logged = false;
        MainWindow main;

        public Login(Window caller)
        {
            InitializeComponent();
            main = (MainWindow)caller;
            usr.Focus();
        }
        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            if (pass.Password.Length<4)
            {
                return;
            }
            Window2 w = new Window2();
            w.Show();
            if (main.LogIn(usr.Text,pass.Password.ToString()))
            {
                logged = true;
                main.PopulateList();
                main.Show();
                w.Close();
                this.Close();
            }
            else
            {
                w.Close();
                MessageBox.Show("Invalid username or password.");
            }
        }
        private void Login_Closing(object sender, CancelEventArgs e)
        {
            if (!logged)
            {
                Environment.Exit(0);
            }
        }
        public bool Logged()
        {
            return logged;
        }
        public void LogOut()
        {
            logged = false;
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                login.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
