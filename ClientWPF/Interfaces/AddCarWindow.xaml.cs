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
            CarsPage carPage = new CarsPage(user);
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
                        MessageBox.Show("Вы успешно добавили автомобиль!");
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
            bool mashina = false;
            if (!string.IsNullOrEmpty(mark.Text))
            {
                if (mark.Text.Length <= 10)
                {
                    if (!string.IsNullOrEmpty(brand.Text))
                    {
                        if (brand.Text.Length <= 10)
                        {
                            if (!string.IsNullOrEmpty(description.Text))
                            {
                                if (description.Text.Length <= 5000)
                                {
                                    if (!string.IsNullOrEmpty(region.Text))
                                    {
                                        if (region.Text.Length <= 3)
                                        {
                                            if (!string.IsNullOrEmpty(old.Text))
                                            {
                                                if (!string.IsNullOrEmpty(numbers.Text))
                                                {
                                                    if (numbers.Text.Length == 6)
                                                    {
                                                        if ((numbers.Text[0] >= 'a') && (numbers.Text[0] <= 'z'))
                                                        {
                                                            if ((numbers.Text[1] >= '0') && (numbers.Text[1] <= '9'))
                                                            {
                                                                if ((numbers.Text[2] >= '0') && (numbers.Text[2] <= '9'))
                                                                {
                                                                    if ((numbers.Text[3] >= '0') && (numbers.Text[3] <= '9'))
                                                                    {
                                                                        if ((numbers.Text[4] >= 'a') && (numbers.Text[4] <= 'z'))
                                                                        {
                                                                            if ((numbers.Text[5] >= 'a') && (numbers.Text[5] <= 'z'))
                                                                            {

                                                                                mashina = true;
                                                                            }
                                                                            else //Если нет, он увидит сообщение, что что-то пошло не так
                                                                            {
                                                                                MessageBox.Show("Шестой символ номера должен быть буквой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                                            }

                                                                        }
                                                                        else //Если нет, он увидит сообщение, что что-то пошло не так
                                                                        {
                                                                            MessageBox.Show("Пятый символ номера должен быть буквой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                                        }

                                                                    }
                                                                    else //Если нет, он увидит сообщение, что что-то пошло не так
                                                                    {
                                                                        MessageBox.Show("Четвертый символ номера должен быть цифой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                                    }
                                                                }
                                                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                                                {
                                                                    MessageBox.Show("Третий символ номера должен быть цифой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                                }
                                                            }
                                                            else //Если нет, он увидит сообщение, что что-то пошло не так
                                                            {
                                                                MessageBox.Show("Второй символ номера должен быть цифой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                            }
                                                        }
                                                        else //Если нет, он увидит сообщение, что что-то пошло не так
                                                        {
                                                            MessageBox.Show("Первый символ номера должен быть буквой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                        }
                                                    }
                                                    else //Если нет, он увидит сообщение, что что-то пошло не так
                                                    {
                                                        MessageBox.Show("Номер должен состоять из 6 символов!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                    }
                                                }
                                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                                {
                                                    MessageBox.Show("Введите номер!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                                }
                                            }
                                            else //Если нет, он увидит сообщение, что что-то пошло не так
                                            {
                                                MessageBox.Show("Введите год производства машины!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            }
                                        }
                                        else //Если нет, он увидит сообщение, что что-то пошло не так
                                        {
                                            MessageBox.Show("Регион не может быть длинне 3 символов!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    }
                                    else //Если нет, он увидит сообщение, что что-то пошло не так
                                    {
                                        MessageBox.Show("Введите регион!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else //Если нет, он увидит сообщение, что что-то пошло не так
                                {
                                    MessageBox.Show("Размер отзыва сликом большой!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                            else //Если нет, он увидит сообщение, что что-то пошло не так
                            {
                                MessageBox.Show("Напишите отзыв о машине!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else //Если нет, он увидит сообщение, что что-то пошло не так
                        {
                            MessageBox.Show("Слишком длинное у вас название модели автомобиля!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else //Если нет, он увидит сообщение, что что-то пошло не так
                    {
                        MessageBox.Show("Введите модель автомобиля!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else //Если нет, он увидит сообщение, что что-то пошло не так
                {
                    MessageBox.Show("Слишком длинное у вас название марки автомобиля!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите марку автомобиля!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (mashina)
                Registration_of_car();
        }


    }
}

