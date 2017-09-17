using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections;
using System.Globalization;
using System.ComponentModel;
using System.Threading;

namespace WpfApp1.WindowList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread thread;

        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        Login login;
        public bool trigger = false;
        public Connector connector;
        //private bool logged = false;


        //INITIALIZE
        public MainWindow()
        {
            login = new Login(this);
            InitializeComponent();
            if (!login.Logged())
            {
                login.Show();
                this.Hide();
            }
            else
            {
                login.Hide();
            }
        }


        
        //Update button 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(thread.ThreadState.Equals(ThreadState.Running)))
            {
                thread = new Thread(new ThreadStart(RetrieveXML));
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            PopulateList();    
            /*
            try
            {
                ListItem item = new ListItem {
                    Date = DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Value1 = androidN.Text,
                    Value2 = javaN.Text,
                    Value3 = net.Text,
                    Value4 = php.Text,
                    Value5 = cpp.Text,
                    Value6 = script.Text,
                    Value7 = ruby.Text,
                    Value8 = ios.Text,
                };
                if (!connector.DeleteItem("insert into mydb.`table`(Date,Android,Java,`C#.NET`,PHP,Cpp,JavaScript,Ruby,IOS) values('" +
                    item.Date+"','" +
                    item.Value1+"','"+
                    item.Value2+"','"+
                    item.Value3+"','"+
                    item.Value4+"','"+
                    item.Value5+"','"+
                    item.Value6+"','"+
                    item.Value7+"','"+
                    item.Value8+"');"))
                {
                    MessageBox.Show("Unable to connect");
                }
                else
                {
                    PopulateList();
                    androidN.Text = "";
                    javaN.Text = "";
                    net.Text = "";
                    php.Text = "";
                    cpp.Text = "";
                    script.Text = "";
                    ruby.Text = "";
                    ios.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }


        //LOG IN
        public bool LogIn(string usr, string pass)
        {
            try
            {
                connector = new Connector();
                bool returnThis;
                returnThis= connector.Login(usr, pass);
                if (!connector.Admin)
                {
                    NewAccButton.Visibility=Visibility.Hidden;
                    DeleteButton.Visibility = Visibility.Hidden;
                }
                else
                {
                    NewAccButton.Visibility = Visibility.Visible;
                    DeleteButton.Visibility = Visibility.Visible;
                }
                thread = new Thread(new ThreadStart(RetrieveXML));
                thread.IsBackground = true;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                return returnThis;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //CLOSE BUTTON 
        //returns to login screen
        private void CloseBtn(object sender, RoutedEventArgs e)
        {
            login = new Login(this);
            login.Show();
            login.LogOut();
            this.Hide();
        }

        // Populates list from the database
        public void PopulateList()
        {
            var gridView = new GridView();
            ListView.Items.Clear();
            ListView.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Date",
                DisplayMemberBinding = new Binding("Date")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Android",
                DisplayMemberBinding = new Binding("Value1")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Java",
                DisplayMemberBinding = new Binding("Value2")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "C#.NET",
                DisplayMemberBinding = new Binding("Value3")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "PHP",
                DisplayMemberBinding = new Binding("Value4")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "CPP",
                DisplayMemberBinding = new Binding("Value5")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "JavaScript",
                DisplayMemberBinding = new Binding("Value6")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Ruby",
                DisplayMemberBinding = new Binding("Value7")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "IOS",
                DisplayMemberBinding = new Binding("Value8")
            });

            try
            {

                gridView.Columns[0].DisplayMemberBinding.BindingGroupName = "Date";
                gridView.Columns[1].DisplayMemberBinding.BindingGroupName = "Value1";
                gridView.Columns[2].DisplayMemberBinding.BindingGroupName = "Value2";
                gridView.Columns[3].DisplayMemberBinding.BindingGroupName = "Value3";
                gridView.Columns[4].DisplayMemberBinding.BindingGroupName = "Value4";
                gridView.Columns[5].DisplayMemberBinding.BindingGroupName = "Value5";
                gridView.Columns[6].DisplayMemberBinding.BindingGroupName = "Value6";
                gridView.Columns[7].DisplayMemberBinding.BindingGroupName = "Value7";
                gridView.Columns[8].DisplayMemberBinding.BindingGroupName = "Value8";

                foreach (var col in gridView.Columns)
                {
                    col.Width = 75;
                }
                string[] colums = { "Date","Android", "Java", "C#.NET", "PHP", "Cpp", "JavaScript", "Ruby", "IOS"};

                ArrayList[] arr = connector.ReadAll("SELECT * FROM mydb.`table`;",colums);
                ArrayList date = arr[0];
                ArrayList Android = arr[1];
                ArrayList Java = arr[2];
                ArrayList net = arr[3];
                ArrayList php = arr[4];
                ArrayList cpp = arr[5];
                ArrayList script = arr[6];
                ArrayList ruby = arr[7];
                ArrayList ios = arr[8];
                for (int i = 0; i < date.Count; i++)
                {
                    ListView.Items.Add(new ListItem
                    {
                        Date = (string)date[i],
                        Value1 = (string)Android[i],
                        Value2 = (string)Java[i],
                        Value3 = (string)net[i],
                        Value4 = (string)php[i],
                        Value5 = (string)cpp[i],
                        Value6 = (string)script[i],
                        Value7 = (string)ruby[i],
                        Value8 = (string)ios[i]
                    });
                }
                ListView.MouseDoubleClick += ListViewItem_MouseDoubleClick;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        /*
         * Delete button method
         * Deletes selected item
         */
        public void DeleteItem(object sender, RoutedEventArgs e)
        {
            ListItem selectedItem = (ListItem)ListView.SelectedItem;
            if (selectedItem != null)
            {
                string query = "DELETE FROM mydb.`table` WHERE Date=@Date;";
                string[] col = {"@Date"};
                string[] val = {selectedItem.Date };
                if (connector.NonQuery(col,val,query))
                {
                    // LabelText.Content = "Deleted";
                    PopulateList();
                }
            }
        }

        // Completely close the app
        private void CloseExit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /*
private void DbClick(object sender, RoutedEventArgs e)
{
   ListItem item = (ListItem)ListView.SelectedItem;
   TextBox box = new TextBox();
   box.Text = item.Name;
   ListView.SelectedItem = box;

}
*/
        //Open a window to create new account
        public void NewAcc(object sender, RoutedEventArgs e)
        {
            NewAccountWindow newAcc = new NewAccountWindow(this);
            newAcc.Show();
        }

        //Event when list item is double clicked
        void ListViewItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!trigger)
            {
                ListItem item = (ListItem)ListView.SelectedItem;
                Window1 details = new Window1(this);
                details.androidN.Text = item.Value1;
                details.javaN.Text = item.Value2;
                details.net.Text = item.Value3;
                details.php.Text = item.Value4;
                details.cpp.Text = item.Value5;
                details.script.Text = item.Value6;
                details.ruby.Text = item.Value7;
                details.ios.Text = item.Value8;
                details.Title = item.Date;
                try
                {
                    int total = 0;
                    total += int.Parse(item.Value1);
                    total += int.Parse(item.Value2);
                    total += int.Parse(item.Value3);
                    total += int.Parse(item.Value4);
                    total += int.Parse(item.Value5);
                    total += int.Parse(item.Value6);
                    total += int.Parse(item.Value7);
                    total += int.Parse(item.Value8);
                    details.Total.Content = total;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                details.Show();
            }
        }

        // Open Plot window
        void Plot(object sender, RoutedEventArgs e)
        {
            Sort("Date", ListSortDirection.Ascending);
            try
            {
                ListItem[] list = new ListItem[ListView.Items.Count];
                for (int i = 0; i < list.Length; i++)
                {
                    list[i] = (ListItem)ListView.Items.GetItemAt(i);
                }
                Graph graph = new Graph(list);
                graph.Initialize();
                graph.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void Main_Window_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }


        // Sorting
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
        // Sorting
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(ListView.Items);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        // Retrieve xml from API and save it to DB
        public void RetrieveXML()
        {
 //           Window2 load = new Window2();
            try
            {

//                load.LoadingText.Content = "Connecting...";
//                load.Show();
                string[] lib = { "Android", "Java", "C#.NET", "PHP", "C++", "JavaScript", "Ruby", "IOS" };
                ArrayList results = new ArrayList();
                foreach (string s in lib)
                {
                    var baseUrl = @"http://api.arbetsformedlingen.se/";
                    // method platsannons/soklista/komunner with parameter landid set to some value
                    var method = string.Format("platsannons/matchning?lanid=12&yrkesomradeid=3&nyckelord=" + s);
                    //var method = string.Format("platsannons/soklista/yrkesomraden");
                    var client = new System.Net.WebClient();
                    // important - the service requires this two parameters!
                    client.Headers.Add(System.Net.HttpRequestHeader.Accept, "application/xml");
                    client.Headers.Add(System.Net.HttpRequestHeader.AcceptLanguage, "en-US");
                    // retrieve content
                    var responseContent = client.DownloadString(string.Format("{0}{1}", baseUrl, method));
                    // "create" the xml object
                    var xml = System.Xml.Linq.XDocument.Parse(responseContent);
                    // do something with the xml
                    /*
                    xml.Root.Descendants("matchningslista").ToList().ForEach(li =>
                     {
                         Console.WriteLine(string.Format("{0} - {1}", li.Element("antal_platserTotal").Value, li.Element("antal_platsannonser").Value));
                     });
                     */
                    results.Add(xml.Root.Element("antal_platserTotal").Value);
                }

                //                load.LoadingText.Content = "Saving...";

                //add to the database
                string[] col = { "@Android", "@Java", "@.NET", "@PHP", "@Cpp", "@JavaScript", "@Ruby", "@IOS" };
                string[] val = { results[0].ToString(), results[1].ToString(),
                    results[2].ToString(), results[3].ToString(), results[4].ToString(), results[5].ToString(), results[6].ToString(), results[7].ToString()};
                connector.NonQuery(col,val,"insert into mydb.`table`(Date,Android,Java,`C#.NET`,PHP,Cpp,JavaScript,Ruby,IOS) values(" +"'"+
                    DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "',"+
                    col[0] + "," +
                    col[1] + "," +
                    col[2] + "," +
                    col[3] + "," +
                    col[4] + "," +
                    col[5] + "," +
                    col[6] + "," +
                    col[7] + ");");
                Application.Current.Dispatcher.Invoke(() => {
                    PopulateList();
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
//            load.Close();
        }

        private void EditAccounts(object sender, RoutedEventArgs e)
        {
            AccountList accountList = new AccountList(this);
            accountList.Show();
        }

        private void ShowErrLog(object sender, RoutedEventArgs e)
        {
            new ErrorLog().Show();
        }
    }
}
