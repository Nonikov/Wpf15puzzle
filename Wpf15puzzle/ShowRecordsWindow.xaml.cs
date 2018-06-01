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

namespace Wpf15puzzle
{
    /// <summary>
    /// Interaction logic for ShowRecordsWindow.xaml
    /// </summary>
    public partial class ShowRecordsWindow : Window
    {
        public ShowRecordsWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.SizeToContent = SizeToContent.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.command.CommandText = "delete from records; delete from sqlite_sequence where name= 'Records'";
            MainWindow.command.ExecuteNonQuery();

            TextB.Text = String.Format(" Id\tName\t\trecord time ");
        }
    }
}
