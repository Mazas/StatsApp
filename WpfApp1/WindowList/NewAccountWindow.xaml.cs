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

namespace WpfApp1.WindowList
{
    /// <summary>
    /// Interaction logic for NewAccountWindow.xaml
    /// </summary>
    public partial class NewAccountWindow : Window
    {
        private MainWindow main;
        public NewAccountWindow(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }

        
        public void Cancel(object sender, RoutedEventArgs e)
        {
            main.Show();
            this.Close();
        }

        //Checking if the passwords match
        private void TextChanged(object sender, RoutedEventArgs e)
        {
            if (pass1.Password.ToString() != pass2.Password.ToString())
            {
                label1.Content = "Does Not Match";
            }
            else
            {
                label1.Content = "";
            }
        }

        //Create new account
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (pass1.Password.ToString() == pass2.Password.ToString())
            {
                string hashedPass = HashnSalt.Hash(pass1.Password.ToString().Trim());
                string elev;

                if ((bool)AdminAcc.IsChecked)
                {
                    elev = "all";

                }
                else
                {
                    elev = "none";
                }
                string[] command = { username.Text.Trim(), hashedPass, elev };
                try
                {
                    if (main.connector.Register(command))
                    {
                        MessageBox.Show("Success.");
                        pass1.Clear();
                        pass2.Clear();
                        username.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
