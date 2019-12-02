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
    public partial class Register_Page : Window
    {
        public Register_Page()
        {
            InitializeComponent();
        }
        public void BackClick_ToLoginPage(object sender, RoutedEventArgs e)
        {
            Login_Page logPage = new Login_Page();
            logPage.Show();
            this.Close();
        }
        protected void Register()
        {
            string a;

            a = data.Text[1] + "/" + data.Text[3] + data.Text[4] + "/" + data.Text[6] + data.Text[7] + data.Text[8] + data.Text[9];
            string birthday = data.Text[0] + a;
            try
            {
                //
                //  подключение к серверу:
                //
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                //
                //  отправка данных клиентом:
                //
                User user = new User(
                    login_sp.Text,
                    name.Text,
                    sername.Text,
                    birthday,
                    0,
                    email.Text,
                    password_sp.Text
                    );
                Query query = new Query("REGISTRATION", user);
                string json = JsonConvert.SerializeObject(query);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush();
                //
                //  получение ответа от сервера:
                //
                StreamReader reader = new StreamReader(stream);
                Query responce = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                switch (responce.Type)
                {
                    case "REGISTERED":
                        MessageBox.Show("Вы успешно зарегистрировались!");
                        break;
                    case "UNREGISTERED":
                        throw new Exception();
                }
                //
                //  завершение общения с сервером:
                //
                reader.Close();
                writer.Close();
                stream.Close();

                Login_Page logPage = new Login_Page();
                logPage.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Регистрация не удалась!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            bool dataValid = false;
            DateTime d1 = new DateTime(2012, 12, 31);

            if (!string.IsNullOrEmpty(login_sp.Text))//Проверка заполнения поля с логином
            {
                if (login_sp.Text.Length <= 10) //Проверка заполнения поля с логином
                {
                    if (!string.IsNullOrEmpty(name.Text))
                    {
                        if (name.Text.Length <= 10)
                        {
                            if (!string.IsNullOrEmpty(sername.Text))
                            {
                                if (sername.Text.Length <= 10)
                                {
                                    if (!string.IsNullOrEmpty(data.Text))
                                    {
                                        if (data.SelectedDate < d1)
                                        {
                                            if (!string.IsNullOrEmpty(email.Text))
                                            {
                                                if (email.Text.Length <= 10)
                                                {
                                                    if (!string.IsNullOrEmpty(password_sp.Text))
                                                    {
                                                        if (password_sp.Text.Length <= 10)
                                                        {
                                                            dataValid = true;
                                                        }
                                                        else //Если нет, он увидит сообщение, что что-то пошло не так
                                                        {
                                                            MessageBox.Show("Слишком длинный пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                        }
                                                    }
                                                    else //Если нет, он увидит сообщение, что что-то пошло не так
                                                    {
                                                        MessageBox.Show("Введите пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                    }
                                                }
                                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                                {
                                                    MessageBox.Show("Слишком длинный адресс электронной почты!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                }
                                            }
                                            else //Если нет, он увидит сообщение, что что-то пошло не так
                                            {
                                                MessageBox.Show("Введите адресс электронной почты!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            }
                                        }
                                        else //Если нет, он увидит сообщение, что что-то пошло не так
                                        {
                                            MessageBox.Show("Дата вашего рождения должна быть не позже 2012 года!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    }
                                    else //Если нет, он увидит сообщение, что что-то пошло не так
                                    {
                                        MessageBox.Show("Выберете дату вашего рождения!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                {
                                    MessageBox.Show("Слишком длинная у вас фамилия,пожалуйста,сократите!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else //Если нет, он увидит сообщение, что что-то пошло не так
                            {
                                MessageBox.Show("Введите вашу фамилию!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else //Если нет, он увидит сообщение, что что-то пошло не так
                        {
                            MessageBox.Show("Слишком длинное у вас имя,пожалуйста,сократите!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else //Если нет, он увидит сообщение, что что-то пошло не так
                    {
                        MessageBox.Show("Введите ваше имя!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else //Если нет, он увидит сообщение, что что-то пошло не так
                {
                    MessageBox.Show("Слишком длинный у вас логин, пожалуйста, сократите!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите ваш логин!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (dataValid)
                Register();
        }
    }
}
