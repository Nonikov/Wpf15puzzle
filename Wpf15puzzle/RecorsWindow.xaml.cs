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
    /// Interaction logic for RecorsWindow.xaml
    /// </summary>
    public partial class RecordsWindow : Window
    {
        public RecordsWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
            MainWindow.dateTime = DateTime.Now;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           MainWindow.player = EnterName.Text != "" ? EnterName.Text : "Incognito";
            Owner.Title = $"Player name: {MainWindow.player.ToUpper()}" ;
        }

        private void Window_TouchEnter(object sender, TouchEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Button button = sender as Button;
            if (e.Key == Key.Enter)
                Close();
        }
    }
}
