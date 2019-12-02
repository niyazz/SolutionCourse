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
    public partial class AccountPage : Window
    {
        public User user;

        public AccountPage(User user)
        {
            InitializeComponent();
            nameLabel.Content = user.User_name + " " + user.User_sername;
            this.user = user;
        }

        private void ToMessagesPage_Click(object sender, RoutedEventArgs e)
        {
            //
            //  подключение к серверу:
            //
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            //
            //  отправка данных клиентом:
            //
            StreamWriter writer = new StreamWriter(stream);
            Query query = new Query("TAKEMESSAGES", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                MessagesPage mesPage = new MessagesPage(user, queryResult.Messages);
                mesPage.Show();
                this.Close();
            }
            //
            //  завершение общения с сервером:
            //
            reader.Close();
            writer.Close();
            stream.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Вы уверены, что хотите выйти", "Выход", MessageBoxButton.OK, MessageBoxImage.Error);
            Login_Page logPage = new Login_Page();
            logPage.Show();
            this.Close();

        }
        private void ToLitrsPage_Click(object sender, RoutedEventArgs e)
        {
            LitrsPage litrPage = new LitrsPage(user);
            litrPage.Show();
            this.Close();
        }

        private void ToCarsPage_Click(object sender, RoutedEventArgs e)
        {
            //
            //  подключение к серверу:
            //
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            //
            //  отправка данных клиентом:
            //
            StreamWriter writer = new StreamWriter(stream);
            Query query = new Query("TAKECARS", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                CarsPage carsPage = new CarsPage(user, queryResult.Cars);
                carsPage.Show();
                this.Close();
            }
            //
            //  завершение общения с сервером:
            //
            reader.Close();
            writer.Close();
            stream.Close();

        }

        private void ToSettingsPage_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void friends_Click(object sender, RoutedEventArgs e)
        //{
        //    friends friends = new friends(user, q);
        //    friends.Show();
        //    this.Close();
        //}
    }
}
