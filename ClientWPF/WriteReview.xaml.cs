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
    /// Логика взаимодействия для WriteReview.xaml
    /// </summary>
    public partial class WriteReview : Window
    {
        public User user;
        public Query q;
        public WriteReview(User user, Query q)
        {
            InitializeComponent();
            this.user = user;
            this.q = q;
            for (int i = 0; i < q.friends.Count; i++)
            {
                friends.Items.Insert(i, $" {q.friends[i].FriendID} \n");
            }
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
