using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для litrs.xaml
    /// </summary>
    public partial class litrs : Window
    {
        public User user;
        public litrs(User user)
        {
            InitializeComponent();
            this.user = user;
            count_of_litrs.Content = user.User_litrs;
        }

        private void close_Click_1(object sender, RoutedEventArgs e)
        {
            Account account = new Account(user);
            account.Show();
            this.Close();
        }

        private void increase_Click(object sender, RoutedEventArgs e)
        {
            if (user.User_litrs <= 200)
            {
                user.User_litrs = user.User_litrs+1;
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                Query query = new Query("UPDATE", user);
                string json = JsonConvert.SerializeObject(query);
                writer.WriteLine(json);
                writer.Flush();

                StreamReader reader = new StreamReader(stream);
                Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                
                switch (result.Type)
                {
                    case "update":
                        count_of_litrs.Content = user.User_litrs;
                        break;
                    case "error":
                        throw new Exception();
                        break;
                }
                
                reader.Close();
                writer.Close();
                stream.Close();

                
            }
            else MessageBox.Show("Значение достигло своего максимума", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void dicrease_Click(object sender, RoutedEventArgs e)
        {
            if (user.User_litrs > 0)
            {
                user.User_litrs = user.User_litrs - 1;
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                Query query = new Query("UPDATE", user);
                string json = JsonConvert.SerializeObject(query);
                writer.WriteLine(json);
                writer.Flush();

                StreamReader reader = new StreamReader(stream);
                Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                // объект юзера, юзер может быть залогиненым (получены все его данные) или незалогиненым(получены ошибки) и 
                // зарегистрированным (получено сообщение об успехе 'TYPE = REGISTERED') или незарегистрированным (получено сообщение об успехе 'TYPE = UNREGISTERED')
                switch (result.Type)
                {
                    case "update":
                        count_of_litrs.Content = user.User_litrs;
                        break;
                    case "error":
                        throw new Exception();
                        break;
                }

                reader.Close();
                writer.Close();
                stream.Close();


            }
            else MessageBox.Show("Значение достигло своего минимума", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Account account = new Account(user);
            account.Show();
            this.Close();
        }
    }
}
