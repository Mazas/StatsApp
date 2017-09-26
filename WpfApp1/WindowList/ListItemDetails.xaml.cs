using System;
using System.Windows;

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
                string[] col = {"@Date","@android","@java","@net","@php","@cpp","@script","@ruby","@ios"};
                string[] val = {Title, int.Parse(androidN.Text).ToString(), int.Parse(javaN.Text).ToString(), int.Parse(net.Text).ToString(),
                    int.Parse(php.Text).ToString(),int.Parse(cpp.Text).ToString(),int.Parse(script.Text).ToString(),int.Parse(ruby.Text).ToString(),
                    int.Parse(ios.Text).ToString()};

                String query = "update mydb."+main.GetCurrentTable()+" set Android=" + col[1] + ",Java=" + col[2] +
                    ",`C#.NET`=" + col[3] + ",PHP=" + col[4] + ",Cpp=" + col[5] +
                    ",JavaScript=" + col[6] + ",Ruby=" + col[7] + ",IOS=" + col[8] + " where Date=" + col[0] + ";";
                if (!main.connector.NonQuery(col,val,query))
                {
                    main.InfoBox.Content = "Unable to connect";
                }
                else
                {

                    main.PopulateList();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                main.InfoBox.Content = ex.Message;
            }
        }
    }
}
