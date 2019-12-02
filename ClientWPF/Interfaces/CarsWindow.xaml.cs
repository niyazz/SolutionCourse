using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientWPF
{
    public partial class CarsPage : Window
    {
        public User user;
        public List<Car> cars;
        public CarsPage(User user, List<Car> cars)
        {
            InitializeComponent();
            this.user = user;
            this.cars = cars;

            for (int i = 0; i < cars.Count; i++)
            {
                listView.Items.Add(cars[i].Car_numbers + ' ' + cars[i].Car_mark);
            }
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
