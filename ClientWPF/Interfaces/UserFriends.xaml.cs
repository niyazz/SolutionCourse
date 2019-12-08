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
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            Query query = new Query("TAKEFRIENDS", user);
            string json = JsonConvert.SerializeObject(query);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(json);
            writer.Flush();

            InitializeComponent();

            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();
            InitializeComponent();
            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);

                this.user.Friends = queryResult.User.Friends;
                for (int i = 0; i < user.Friends.Count; i++)
                {
                    MyFriends.Items.Add(user.Friends[i].friendName);
                }
            }
           
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

        private void AddFriend_Click(object sender, RoutedEventArgs e)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            Friends newFriends = new Friends(user.User_id, Convert.ToInt32(frinedIdInput.Text));
            Query query = new Query("ADDFRIEND", newFriends, user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();

            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            if (responce.Contains("Ошибка!") == false)
            {
                MessageBox.Show("Охуенна");
            }
            else
            {
                MessageBox.Show(responce);
            }
        }
    }
}
