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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;

namespace Wpf15puzzle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SQLiteConnection connection;
       public static SQLiteCommand command;

        RecordsWindow rw;

        static public DateTime dateTime;
        TimeSpan gameTime;
        static public string player = "Incognito";

        public MainWindow()
        {
            Title = $"Player name: {player.ToUpper()}";
            InitializeComponent();
            

            if (!File.Exists("myLiteDb.db"))
            {
                SQLiteConnection.CreateFile("myLiteDB.db");
            }
            connection = new SQLiteConnection("Data Source = myLiteDB.db");
            connection.Open();

            command = new SQLiteCommand(connection);
            string sql = "CREATE TABLE IF NOT EXISTS Records ('Id' integer primary key autoincrement, 'Name' text, 'record time' text)";
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int holdBut = -1;
            Button[] buttons = { button01, button02, button03, button04, button05, button06, button07, button08, button09, button10, button11, button12, button13, button14, button15, button16 };
            for (int i = 0; i < 16; i++)
            {
                if (buttons[i] == sender)
                {
                    holdBut = i;
                    break;
                }
            }

            int free = -1;
            for (int i = 0; i < 16; i++)
            {
                if (!buttons[i].IsEnabled)
                {
                    free = i;
                }
            }

            int row1 = Grid.GetRow(buttons[holdBut]);
            int column1 = Grid.GetColumn(buttons[holdBut]);
            int row2 = Grid.GetRow(buttons[free]);
            int column2 = Grid.GetColumn(buttons[free]);

            if ((row1 == row2 - 1 || row1 == row2 + 1) && column1 == column2
                || (column1 == column2 - 1 || column1 == column2 + 1) && row1 == row2
                )
            {
                Grid.SetRow(buttons[holdBut], row2);
                Grid.SetColumn(buttons[holdBut], column2);
                Grid.SetRow(buttons[free], row1);
                Grid.SetColumn(buttons[free], column1);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rw = new RecordsWindow();
            rw.Owner = this;
            rw.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameTime = DateTime.Now - dateTime;
            command.CommandText = "INSERT INTO Records ('Name', 'record time') values ('" + player + "', '" + gameTime.ToString(@"dd\.hh\:mm\:ss") + "')";
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var srW = new ShowRecordsWindow();
            srW.Owner = this;
            srW.Show();

            string sqlExpression = "SELECT * FROM Records";
            //  command = new SQLiteCommand(sqlExpression, connection);
            command.CommandText = sqlExpression;
            SQLiteDataReader reader = command.ExecuteReader();

            if(reader.HasRows)
            {    
                srW.TextB.Text = String.Format(" {0}\t{1}\t\t{2} \n", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    object name = reader.GetValue(1);
                    object record = reader.GetValue(2);
                    
                    srW.TextB.Text += String.Format(" {0}\t{1,-20}\t{2}  \n", id, name, record);
                }
            }
            reader.Close();
        }
    }
}
