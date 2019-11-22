using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для Account.xaml
    /// </summary>
    public partial class Account : Window
    {
        public User user;
        public Account(User user)
        {
            InitializeComponent();
            name_1.Content = user.User_name+" "+ user.User_sername;
            this.user = user;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            Query query = new Query("TAKEMESSAGES", user);

            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();

            StreamReader reader = new StreamReader(stream);
            string answer = reader.ReadLine().ToString();

            if (answer.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(answer);

                Messages messages = new Messages(user, queryResult);
                messages.Show();
                this.Close();
            }



           
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Вы уверены, что хотите выйти", "Выход", MessageBoxButton.OK, MessageBoxImage.Error);
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();

        }
    }
}
