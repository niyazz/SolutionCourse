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
        public Sign_up()
        {
            InitializeComponent();
        }
        private void sign_up_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {    
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();

                User user = new User(
                    "REGISTRATION",
                    login_sp.Text,
                    name.Text, 
                    sername.Text,
                    birthday.Text,
                    0,
                    email.Text,
                    password_sp.Text
                    );

                string json = JsonConvert.SerializeObject(user);

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush(); 

                StreamReader reader = new StreamReader(stream);
                User result= JsonConvert.DeserializeObject<User>(reader.ReadLine());
// объект юзера, юзер может быть залогиненым (получены все его данные) или незалогиненым(получены ошибки) и 
// зарегистрированным (получено сообщение об успехе 'TYPE = REGISTERED') или незарегистрированным (получено сообщение об успехе 'TYPE = UNREGISTERED')

                switch (result.Type)
                {
                    case "REGISTERED":
                        MessageBox.Show(result.Type);
                        break;
                    case "UNREGISTERED":
                        throw new Exception();
                        break;
                }
                
                reader.Close();
                writer.Close();
                stream.Close();
            }
            catch
            {
                MessageBox.Show("Такой логин уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
