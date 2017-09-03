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
    /// Interaction logic for AccountInfo.xaml
    /// </summary>
    public partial class AccountInfo : Window
    {
        private Connector connector;
        private AccountList acc;
        public AccountInfo(AccountList acc)
        {
            this.acc = acc;
            connector = acc.main.connector;
            InitializeComponent();
            Message.Content = "";
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // Build query

            string elev = (bool)AdminBox.IsChecked ? "all" : "none";
            string query = query = "update mydb.users set elev='" + elev + "'";
            if (Pass1.Text.Length==0 && Email.Text.Contains('@'))
            {
                query+=",email='"+Email.Text+"'";
            }

            if (Pass1.Text.Length >= 6)
            {
                if (Pass1.Text == Pass2.Text)
                {
                    string hashedPass = HashnSalt.Hash(Pass1.Text.Trim());
                    query += ", pass='" + hashedPass + "'";
                }
                else
                {
                    Message.Content = "Passwords don`t match.";
                }
            }
            else if (Pass1.Text.Length>0 && Pass1.Text.Length<6)
            {
                Message.Content = "Password is too short.";
            }
            query+= "where username = '"+username.Content+"';";

            // Execute query

            if (connector.NonQuery(query))
            {
                acc.windowOpen = false;
                acc.PopulateList();
                Close();
            }
            else
            {
                Message.Content = "Failed";
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            acc.windowOpen = false;
            acc.PopulateList();
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            acc.windowOpen = false;
            acc.PopulateList();
        }
    }
}
