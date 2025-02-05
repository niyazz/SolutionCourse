﻿using System;
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
    public partial class MessagesPage : Window
    {
        public int id;
        public User user;
        public List<Message> Messages;
        public MessagesPage(User user)
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
            Query query = new Query("TAKEMESSAGES", user);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();
            //
            //  получение ответа от сервера:
            //
            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();

            InitializeComponent();
            MyFriends();
            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);
                this.Messages = queryResult.Messages;
                messTextBox.Text = "";
                for (int i = 0; i < Messages.Count; i++)
                {
                    messTextBox.Text += $"ОТ {Messages[i].senderName} - { Messages[i].Text} \n";
                }

            }
            else
            {
                messTextBox.Text = "Сообщений нет";
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
                selected();
                StreamWriter writer = new StreamWriter(stream);
                Message newMess = new Message(0, id, user.User_name, friends.Text, DateTime.Now, inputMess.Text);
                Query query = new Query("SENDMES", newMess);
                string json = JsonConvert.SerializeObject(query);
                Console.WriteLine(json);
                writer.WriteLine(json);
                writer.Flush();
                inputMess.Text = "";
                //
                //  получение ответа от сервера:
                //
                StreamReader reader = new StreamReader(stream);
                Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());
                switch (result.Type)
                {
                    case "SENT":
                        messTextBox.Text = "";
                       
                        break;
                    case "UNSENT":
                        messTextBox.Text = " ";
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
                MessageBox.Show("Не удалось отправить сообщение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void selected()
        {
            for (int i = 0; i < user.Friends.Count; i++)
            {
                if (friends.SelectedValue.ToString() == user.Friends[i].friendName)
                {
                    id = user.Friends[i].FriendID;
                }
            } 
        }
        void MyFriends()
        {
           
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            Query query = new Query("TAKEFRIENDS", user);
            string json = JsonConvert.SerializeObject(query);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(json);
            writer.Flush();

            InitializeComponent();

            StreamReader reader = new StreamReader(stream);
            string responce = reader.ReadLine().ToString();
            InitializeComponent();
            if (responce.Contains("Ошибка!") == false)
            {
                Query queryResult = JsonConvert.DeserializeObject<Query>(responce);

                this.user.Friends = queryResult.User.Friends;
                for (int i = 0; i < user.Friends.Count; i++)
                {
                    friends.Items.Add(user.Friends[i].friendName);
                }
            }
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            bool send1 = false;
            if (!string.IsNullOrEmpty(inputMess.Text))
                send1 = true;
            else //Если нет, он увидит сообщение, что что-то пошло не так
            {
                MessageBox.Show("Введите сообщение!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (send1)
                Send();
        }
    }
}
