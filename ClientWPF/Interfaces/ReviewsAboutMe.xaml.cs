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

namespace ClientWPF.Interfaces
{
    /// <summary>
    /// Логика взаимодействия для ReviewsAboutMe.xaml
    /// </summary>
    public partial class ReviewsAboutMe : Window
    {
        public User user;
        public List<Review> reviews;
        public ReviewsAboutMe(User user,List<Review> reviews)
        {
            this.reviews = reviews;
            this.user = user;
            InitializeComponent();
        }
           
        public ReviewsAboutMe(User user)
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
            Query query = new Query("TAKEREVIEWS", reviews);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();
            InitializeComponent();
            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                this.reviews = queryResult.Reviews;
                reviewsmeTextBox.Text = "";
                for (int i = 0; i < reviews.Count; i++)
                {
                    reviewsmeTextBox.Text += $"ОТ {reviews[i].carNumber} -  {reviews[i].Text} \n";
                }
            }
            else
            {
                
                    reviewsmeTextBox.Text = "Новостей нет";
                
            }
            //
            //  завершение общения с сервером:
            //
            reader.Close();
            writer.Close();
            stream.Close();


        }

        private void WriteReview(object sender, RoutedEventArgs e)
        {
            WriteReview wreview = new WriteReview(user);
            wreview.Show();
            this.Close();


        }

        private void BackClick_ToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountPage accPage = new AccountPage(user);
            accPage.Show();
            this.Close();
        }
    }
}
