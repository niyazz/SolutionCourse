﻿using System;
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

namespace ClientWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // second-commit
        }
        public void Open()
        {
            Account account = new Account();
            account.Show();
            this.Close();
        }
        private void sendToServ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /* Будто создаем дом с названием "Any", и в нем квартира под 908 номером, нас интересует, то что происходит в этой квартире */
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect("localhost", 908);
                NetworkStream stream = clientSocket.GetStream();

                /* Берем текст из поля и отправляем на сервер */
                //string message = textBox.Text; //закоммитила З.
                string login_message = Login.Text;                      //добавила З.
                string password_message = Password.Password;            //добавила З.
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(login_message);                        //добавила З.
                writer.WriteLine(password_message);                     //добавила З.
                writer.Flush(); // мгновенная отправка

                /* Получаем ответ с сервера и выводим в бокс */
                StreamReader reader = new StreamReader(stream);
                // taked.Text = "Получен ответ: " + reader.ReadLine(); //закоммитила З.
                Open();
                /* Закрываем все потоки */
                reader.Close();
                writer.Close();
                stream.Close();
            }
            catch
            {
                //taked.Text = "Отсутствует соединение с сервером";  // закоммитила З.
                MessageBox.Show("Неправильный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);//добавила З.
            }
           
        }
    }
}
