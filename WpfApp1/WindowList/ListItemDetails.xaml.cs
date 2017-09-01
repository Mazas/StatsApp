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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        MainWindow main;
        public Window1(MainWindow main)
        {
            Closing += Window1_Closing;
            this.main = main;
            InitializeComponent();
            main.trigger = true;
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            main.trigger = false;
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            main.trigger = false;
            this.Close();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                main.trigger = false;
                ListItem item = new ListItem
                {
                    Date = Title,
                    Value1 = androidN.Text,
                    Value2 = javaN.Text,
                    Value3 = net.Text,
                    Value4 = php.Text,
                    Value5 = cpp.Text,
                    Value6 = script.Text,
                    Value7 = ruby.Text,
                    Value8 = ios.Text,
                };
                if (!main.connector.NonQuery("update mydb.`table`set Android='"+ item.Value1 + "',Java='"+ item.Value2 + 
                    "',`C#.NET`='"+ item.Value3 + "',PHP='"+item.Value4 + "',Cpp='"+ item.Value5 + 
                    "',JavaScript='"+ item.Value6 + "',Ruby='" + item.Value7+"',IOS='"+ item.Value8 +"' where Date='"+item.Date+"';"))
                {
                    MessageBox.Show("Unable to connect");
                }
                else
                {

                    main.PopulateList();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
