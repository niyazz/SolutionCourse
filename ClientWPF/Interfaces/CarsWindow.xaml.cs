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
    public partial class CarsPage : Window
    {
        public User user;
        public List<Car> cars;
        public CarsPage(User user)
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
            Query query = new Query("TAKECARS", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            InitializeComponent();
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);

                this.cars = queryResult.Cars;

                for (int i = 0; i < cars.Count; i++)
                {
                    listView.Items.Add(cars[i].Car_numbers + ' ' + cars[i].Car_mark);
                }
            }
            //
            //  завершение общения с сервером:
            //
            reader.Close();
            writer.Close();
            stream.Close();   
        }

        public CarsPage(User user, List<Car> cars)
        {
            this.user = user;
            this.cars = cars;
            InitializeComponent();

        }
        private void BackClick_ToAccountPage(object sender, RoutedEventArgs e)
        {
            AccountPage accPage = new AccountPage(user);
            accPage.Show();
            this.Close();

        }
        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            AddCarPage addingcar = new AddCarPage(user, cars);
            addingcar.Show();
            this.Close();
        }
    }
}
