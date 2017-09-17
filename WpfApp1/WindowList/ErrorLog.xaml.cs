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
using System.IO;

namespace WpfApp1.WindowList
{
    /// <summary>
    /// Interaction logic for ErrorLog.xaml
    /// </summary>
    public partial class ErrorLog : Window
    {
        string path = @"Logs\ErrorLog.txt";
        public ErrorLog()
        {
            InitializeComponent();
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        LogText.Text += line + "\n";
                    }
                }
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                LogText.Text = "";
            }
        }
    }
}
