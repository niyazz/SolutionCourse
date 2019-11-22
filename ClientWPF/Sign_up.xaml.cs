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
        public bool направление = false;
        public Sign_up()
        {
            InitializeComponent();

        }
        public void Open_MainWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
        void Activity1()
        {
            try
            {
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                User user = new User(  
                    login_sp.Text,
                    name.Text,
                    sername.Text,
                    birthday.Text,
                    0,
                    email.Text,
                    password_sp.Text
                    );

                Query query = new Query("REGISTRATION", user);
                string json = JsonConvert.SerializeObject(query);

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush();

                StreamReader reader = new StreamReader(stream);
                Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
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
                Open_MainWindow();
            }
            catch
            {
                MessageBox.Show("Введите корректные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void sign_up_button_Click(object sender, RoutedEventArgs e)
        {
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
                                                            //sign_up_button.IsEnabled = true;
                                                            направление = true;
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
                    MessageBox.Show("Слишком длинный у вас логин,пожалуйста,сократите!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите ваш логин!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (направление == true)
                Activity1();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
