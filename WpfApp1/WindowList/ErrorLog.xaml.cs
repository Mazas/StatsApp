using System.Windows;
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
