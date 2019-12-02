using Newtonsoft.Json;
using System.IO;
using System.Net.Sockets;
using System.Windows;

namespace ClientWPF
{
    public partial class Login_Page : Window
    {
        public Login_Page()
        {
            InitializeComponent(); // second-commit
        }
        public void Open_Account(User user) // должна будет принимать объект залогиненного пользователя
        {
            AccountPage accPage = new AccountPage(user);
            accPage.Show();
            this.Close();
        }
        public void Open_RegisterPage()
        {
            Register_Page regPage = new Register_Page();
            regPage.Show();
            this.Close();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {   //
                //  подключение к серверу:
                //
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                //
                //  отправка данных клиентом:
                //
                User user = new User(Login.Text, Password.Password);
                Query query = new Query("SIGNIN", user);
                string json = JsonConvert.SerializeObject(query);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush();
                //
                //  получение ответа от сервера:
                //
                StreamReader reader = new StreamReader(stream);
                string responce = reader.ReadLine().ToString();
                if (responce.Contains("Ошибка!") == false)
                {
                    Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                    Open_Account(queryResult.User);
                }
                else
                {
                    Error.Visibility = Visibility.Visible;
                    Error.Text = responce;
                }
                //
                //  завершение общения с сервером:
                //
                reader.Close();
                writer.Close();
                stream.Close();

            }
            catch
            {
                Error.Text = "Отсутствует соединение с сервером";
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ToRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            Open_RegisterPage();
        }
    }
}
