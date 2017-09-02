using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfApp1.Core;

namespace WpfApp1.WindowList
{
    /// <summary>
    /// Interaction logic for AccountList.xaml
    /// </summary>
    public partial class AccountList : Window
    {
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        private MainWindow main;
        bool trigger = false;

        public AccountList(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
            PopulateList();
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void PopulateList()
        {
            try
            {
                var gridView = new GridView();
                ListView.Items.Clear();
                ListView.View = gridView;
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Username",
                    DisplayMemberBinding = new Binding("username")
                });
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Elevation",
                    DisplayMemberBinding = new Binding("elev")
                });


                gridView.Columns[0].DisplayMemberBinding.BindingGroupName = "Username";
                gridView.Columns[1].DisplayMemberBinding.BindingGroupName = "elev";


                gridView.Columns[0].Width = 75;
                gridView.Columns[1].Width = 75;


                string[] colums = { "username", "elev" };

                ArrayList[] arr = main.connector.ReadAll("SELECT * FROM mydb.users;", colums);
                for (int i = 0; i < arr.Length; i++)
                {
                    ListView.Items.Add(new AccountListItem
                    {
                        username = (string)arr[0][i],
                        elev = (string)arr[1][i]
                    });
                }
                ListView.MouseDoubleClick += ListViewItem_MouseDoubleClick;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //Sorting
        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    //string header = headerClicked.Column.Header as string;
                    string header = headerClicked.Column.DisplayMemberBinding.BindingGroupName;
                    Sort(header, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    // Remove arrow from previously sorted header  
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(ListView.Items);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            main.NewAcc(sender,e);
        }

        void ListViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!trigger)
            {
                AccountListItem item = (AccountListItem)ListView.SelectedItem;
                AccountInfo details = new AccountInfo(main.connector);
                if (item.elev == "all")
                {
                    details.AdminBox.IsChecked = true;
                }
                details.Title = item.username;
                details.username.Content = item.username;
                details.Show();
            }
        }
    }
}
