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
    /// Логика взаимодействия для AddCar.xaml
    /// </summary>
    public partial class AddCar : Window
    {
        public User user;
        public Cars car;
        public AddCar(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.car.User = user;
            cars car_form = new cars(car);
            car_form.Show();
            this.Close();
        }
        void Registration_of_car()
        {
            try
            {
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();
                Cars newcar = new Cars(
                    Convert.ToInt32(region.Text),
                    numbers.Text,
                    brand.Text,
                    mark.Text,
                    Convert.ToInt32(old.Text),
                    description.Text,
                    user.User_id
                    );

                Query query = new Query("ADDINGCAR", car);
                string json = JsonConvert.SerializeObject(query);

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(json);
                writer.Flush();
            }
            catch
            {

            }
        
       }
        private void signcar_Click(object sender, RoutedEventArgs e)
        {
            Registration_of_car();
        }
    }
}
