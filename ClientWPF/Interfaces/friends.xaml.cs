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

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для friends.xaml
    /// </summary>
    public partial class friends : Window
    {
        public User user;
        public Query friend;
        public friends(User user, Query friends)
        {
            InitializeComponent();
            this.friend = friends;
            this.user = user;
            
            for (int i = 0; i < friends.friends.Count; i++)
            {
                ListOfFriends.Items.Insert(i,$" {friends.friends[i].FriendID} \n");
            }
        }

        private void writeReview_Click(object sender, RoutedEventArgs e)
        {

        }
       
        
        
        //назад
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Account account = new Account(user);
            account.Show();
            this.Close();

        }
    }
}
