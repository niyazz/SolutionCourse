using Newtonsoft.Json;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // second-commit
        }
        public void Open() // должна будет принимать объект залогиненного пользователя
        {
            Account account = new Account();
            account.Show();
            this.Close();
        }
        public void Open_sp()
        {
            Sign_up sign = new Sign_up();
            sign.Show();
            this.Close();
        }
        private void sendToServ_Click(object sender, RoutedEventArgs e)
        {
            try
            {             
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();

                User user = new User("SIGNIN", Login.Text, Password.Password);
                string json = JsonConvert.SerializeObject(user);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush(); 

                StreamReader reader = new StreamReader(stream);
                string message =  reader.ReadLine().ToString(); 

                if(message.Contains("Ошибка!") == false)
                {
                    Open(); // тут отправка залогиненого пользователя                  
                }
                else
                {
                    Taken.Visibility = Visibility.Visible;
                    Taken.Text = message;
                }
                        
                reader.Close();
                writer.Close();
                stream.Close();

            }
            catch
            {
                Taken.Text = "Отсутствует соединение с сервером"; 
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Open_sp();
        }
    }
}
