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
using ClientWPF.Interfaces;

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
            MessagesPage messPage = new MessagesPage(user);
            messPage.Show();
            this.Close();
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
            CarsPage carsPage = new CarsPage(user);
            carsPage.Show();
            this.Close();
        }

        private void ToSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            UserFriends userFriends = new UserFriends(user);
            userFriends.Show();
            this.Close();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReviewsAboutMe reviewsAboutMe = new ReviewsAboutMe(user);
            reviewsAboutMe.Show();
            this.Close();
        }

        private void News_Click(object sender, RoutedEventArgs e)
        {
            News news = new News(user);
            news.Show();
            this.Close();
        }

        //private void friends_Click(object sender, RoutedEventArgs e)
        //{
        //    friends friends = new friends(user, q);
        //    friends.Show();
        //    this.Close();
        //}
    }
}
