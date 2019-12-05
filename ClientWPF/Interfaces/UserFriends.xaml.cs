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

namespace ClientWPF.Interfaces
{
    /// <summary>
    /// Логика взаимодействия для UserFriends.xaml
    /// </summary>
    public partial class UserFriends : Window
    {
        public User user;
        public List<Friends> friends;
        public UserFriends(User user)
        {
            this.user = user;
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
            Query query = new Query("TAKEFRIENDS", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            InitializeComponent();

        }

        public UserFriends(User user, List<Friends> friends)
        {
            this.user = user;
            this.friends = friends;
            InitializeComponent();

        }

        private void BackClick_ToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountPage accPage = new AccountPage(user);
            accPage.Show();
            this.Close();
        }
    }
}
