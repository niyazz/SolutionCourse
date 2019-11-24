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
using Newtonsoft.Json;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для Messages.xaml
    /// </summary>
    public partial class Messages : Window
    {
        public User user;
        public Messages(User user, Query q)
        {
            InitializeComponent();
            this.user = user;
            mes_t_sb.Text = "";
            for (int i = 0; i < q.Messages.Count; i++)
            {
                mes_t_sb.Text += $"ОТ {q.Messages[i].senderName} - { q.Messages[i].Text} \n";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(user);
            account.Show();
            this.Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            //Query query = new Query("USERMESSAGES", user);
        }
    }
}
