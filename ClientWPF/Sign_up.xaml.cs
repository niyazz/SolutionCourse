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
using Newtonsoft.Json;
namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для Sign_up.xaml
    /// </summary>
    public partial class Sign_up : Window
    {
        public static string str;
        public Sign_up()
        {
            InitializeComponent();
        }

        private void sign_up_button_Click(object sender, RoutedEventArgs e)
        {

            User user = new User();
            try
            {
                /* Будто создаем дом с названием "Any", и в нем квартира под 908 номером, нас интересует, то что происходит в этой квартире */
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();

                /* Берем текст из поля и отправляем на сервер */
                //string message = textBox.Text; //закоммитила З.
                //var ob1 = new User();
                user.User_login = login_sp.Text;
                user.User_password = password_sp.Text;
                user.User_name = name.Text;
                user.User_sername = sername.Text;
                user.User_mail = email.Text;
                user.User_birthday = birthday.Text;

               
                var serialized1 = JsonConvert.SerializeObject(user, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                });
                Console.WriteLine(serialized1);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(serialized1);

                writer.Flush(); // мгновенная отправка
                str = serialized1;
                /* Получаем ответ с сервера и выводим в бокс */
                StreamReader reader = new StreamReader(stream);
                // taked.Text = "Получен ответ: " + reader.ReadLine(); //закоммитила З.
                MessageBox.Show("Вы зарегистрировались!");
                /* Закрываем все потоки */
                reader.Close();
                writer.Close();
                stream.Close();
            }
            catch
            {
                //taked.Text = "Отсутствует соединение с сервером";  // закоммитила З.
                MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
