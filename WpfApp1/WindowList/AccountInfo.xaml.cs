using System;
using System.Collections;
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
            string query = query = "update mydb.users set elev= @elev";
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            list.Add("@elev");
            list2.Add(elev);
            if (Email.Text.Contains('@'))
            {
                query+=",email= @email";
                list.Add("@email");
                list2.Add(Email.Text);
            }

            if (Pass1.Text.Length >= 6)
            {
                if (Pass1.Text == Pass2.Text)
                {
                    string hashedPass = HashnSalt.Hash(Pass1.Text.Trim());
                    query += ", pass= @pass";
                    list.Add("@pass");
                    list2.Add(hashedPass);
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
            query+= " where username = @username;";
            list.Add("@username");
            list2.Add(username.Content);

            string[] col = new string[list.Count];
            string[] val = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                col[i] = (string)list[i];
                val[i] = (string)list2[i];
            }
           
            // Execute query

            if (connector.NonQuery(col,val, query))
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
