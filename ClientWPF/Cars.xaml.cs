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
    /// <summary>
    /// Логика взаимодействия для cars.xaml
    /// </summary>
    public partial class cars : Window
    {
        public Cars car;
        public cars(Cars car)
        {
            InitializeComponent();
            this.car = car;
            //List_of_cars.Text = car.Car_name + ' ' + car.Car_numbers+'\n';


        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Account account = new Account(car.User);
            account.Show();
            this.Close();

        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            AddCar addingcar = new AddCar(car.User);
            addingcar.Show();
            this.Close();
        }
    }
}
