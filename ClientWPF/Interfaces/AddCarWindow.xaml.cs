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
    public partial class AddCarPage : Window
    {
        public User user;
        public Car car;
        public List<Car> cars;
        public AddCarPage(User user, List<Car> cars)
        {
            InitializeComponent();
            this.user = user;
            this.cars = cars;
        }

        private void BackClick_ToCarsPage(object sender, RoutedEventArgs e)
        {
            CarsPage carPage = new CarsPage(user, cars);
            carPage.Show();
            this.Close();
        }
        private void Registration_of_car()
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
                Car newCar = new Car(
                    region.Text,
                    description.Text,
                    Convert.ToInt32(old.Text),
                    numbers.Text,
                    mark.Text,
                    brand.Text,
                    user.User_id
                    );

                Query query = new Query("ADDCAR", newCar);
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
                    case "ADDED":
                        break;
                    case "UNADDED":
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
                MessageBox.Show("Не удалось добавить машину!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void RegisterCar_Click(object sender, RoutedEventArgs e)
        {
            Registration_of_car();
        }
    }
}
