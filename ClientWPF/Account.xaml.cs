using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
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
            Messages messages = new Messages(user);
            messages.Show();
            this.Close();
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
