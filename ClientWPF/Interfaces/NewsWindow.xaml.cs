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
using ClientWPF.Models;

namespace ClientWPF.Interfaces
{
    /// <summary>
    /// Логика взаимодействия для News.xaml
    /// </summary>
    public partial class News : Window
    {
        public User user;
        public List<classNews> news;
        public News(User user)
        {
            this.user = user;
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
            Query query = new Query("TAKENEWS", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            InitializeComponent();

            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                this.news = queryResult.News;
                newsTextBox.Text = "";
                for (int i = 0; i < news.Count; i++)
                {
                    newsTextBox.Text += $"ОТ {news[i].senderName} - { news[i].Text} \n";
                }
            }
            else
            {
                newsTextBox.Text = "Новостей нет";
            }
            //
            //  завершение общения с сервером:
            //
            reader.Close();
            writer.Close();
            stream.Close();
        }

        private void BackClick_ToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountPage accPage = new AccountPage(user);
            accPage.Show();
            this.Close();
        }
        void Send()
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
                classNews newnews = new classNews(0, user.User_name, DateTime.Now, inputNews.Text);
                Console.WriteLine("calssnew = " + newnews.Text);
                Query query = new Query("SENDNEWS", newnews);
                string json = JsonConvert.SerializeObject(query);
                Console.WriteLine(json);
                writer.WriteLine(json);
                writer.Flush();
                inputNews.Text = "";
                //
                //  получение ответа от сервера:
                //
                StreamReader reader = new StreamReader(stream);
                Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                switch (result.Type)
                {
                    case "ADDED":
                        newsTextBox.Text = "";
                        for (int i = 0; i < news.Count; i++)
                        {
                            newsTextBox.Text += $"ОТ {news[i].senderName} - { news[i].Text} \n";
                        }
                        break;
                    case "UNADDED":
                        newsTextBox.Text = " ";
                        throw new Exception();

                }
                ////
                ////  завершение общения с сервером:
                ////
                ///                
                newsTextBox.Text += $"ОТ {newnews.senderName} - { newnews.Text} \n";


                reader.Close();
                writer.Close();
                stream.Close();
                //newsTextBox.Text += $"ОТ {news[i].senderName} - { news[i].Text} \n";
            }
            catch
            {
                MessageBox.Show("Не удалось отправить новость!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            bool send1 = false;
            if (!string.IsNullOrEmpty(inputNews.Text))
                send1 = true;
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите текст новости!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (send1)
                Send();

        }
    }
}
