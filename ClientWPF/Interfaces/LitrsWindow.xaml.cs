using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Sockets;
using System.IO;
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
    public partial class LitrsPage : Window
    {
        public User user;
        private int maxLitrs = 200, minLitrs = 0;
        public LitrsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            count_of_litrs.Content = user.User_litrs;
        }

        private void BackClick_ToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountPage account = new AccountPage(user);
            account.Show();
            this.Close();
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            if (user.User_litrs <= maxLitrs)
            {
                user.User_litrs = user.User_litrs + 1;
                ChangeLitrs();
            }
            else MessageBox.Show("Значение достигло своего максимума", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Dicrease_Click(object sender, RoutedEventArgs e)
        {
            if (user.User_litrs > minLitrs)
            {
                user.User_litrs = user.User_litrs - 1;
                ChangeLitrs();
            }
            else MessageBox.Show("Значение достигло своего минимума", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        void litrs()
        {
            if (user.User_litrs <= maxLitrs)
            {
                user.User_litrs = user.User_litrs + Convert.ToInt32(count.Text);
                ChangeLitrs();
            }
            else MessageBox.Show("Значение достигло своего максимума", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void addlitr_Click(object sender, RoutedEventArgs e)
        {
            bool send1 = false;
            if (!string.IsNullOrEmpty(cnumbercard.Text))
            {
                if (cnumbercard.Text.Length == 16)
                {
                    if (!string.IsNullOrEmpty(nameowner.Text))
                    {
                        if (!string.IsNullOrEmpty(cvc.Text))
                        {
                            if (cvc.Text.Length == 3)
                            {
                                if (!string.IsNullOrEmpty(count.Text))
                                {
                                    send1 = true;
                                }
                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                {
                                    MessageBox.Show("Введите количество литров!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else //Если нет, он увидит сообщение, что что-то пошло не так
                            {
                                MessageBox.Show("CVV состоит из 3 цифр!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else //Если нет, он увидит сообщение, что что-то пошло не так
                        {
                            MessageBox.Show("Введите CVV!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else //Если нет, он увидит сообщение, что что-то пошло не так
                    {
                        MessageBox.Show("Введите свое имя!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else //Если нет, он увидит сообщение, что что-то пошло не так
                {
                    MessageBox.Show("Номер карты состоит из 16 цифр!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите номер карты!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (send1)
                litrs();
        }

        private void ChangeLitrs()
        {
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
                StreamWriter writer = new StreamWriter(stream);
                Query query = new Query("UPDATELITRS", user);
                string json = JsonConvert.SerializeObject(query);
                writer.WriteLine(json);
                writer.Flush();
                //
                //  получение ответа от сервера:
                //
                StreamReader reader = new StreamReader(stream);
                Query responce = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                switch (responce.Type)
                {
                    case "CHANGED":
                        count_of_litrs.Content = user.User_litrs;
                        break;
                    case "UNCHANGED":
                        throw new Exception();
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
                MessageBox.Show("Не удалось изменить литры!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    Account account = new Account(user);
        //    account.Show();
        //    this.Close();
        //}
    }
}
