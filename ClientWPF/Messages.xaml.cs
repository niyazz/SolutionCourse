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
    /// <summary>
    /// Логика взаимодействия для Messages.xaml
    /// </summary>
    public partial class Messages : Window
    {
        public User user;
        public Query q;
        public Message m;
        public Messages(User user, Query q)
        {
            InitializeComponent();
            this.user = user;
            this.q = q;

            mes_t_sb.Text = "";
            for (int i = 0; i < q.Messages.Count; i++)
            {
                mes_t_sb.Text += $"ОТ {q.Messages[i].senderName} - { q.Messages[i].Text} \n";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Account account = new Account(user);
            account.Show();
            this.Close();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            m = new Message(0, 1, user.User_name, user.User_name, DateTime.Now, mes_from.Text);



            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            Query query = new Query("SEND", m);
            string json = JsonConvert.SerializeObject(query);
            writer.WriteLine(json);
            writer.Flush();

            //    StreamReader reader = new StreamReader(stream);
            //    Query result = JsonConvert.DeserializeObject<Query>(reader.ReadLine());

            //    switch (result.Type)
            //    {
            //        case "SENT":
            //            mes_t_sb.Text = "";
            //            for (int i = 0; i < q.Messages.Count; i++)
            //            {
            //                mes_t_sb.Text += $"ОТ {q.Messages[i].senderName} - { q.Messages[i].Text} \n";
            //            }
            //            break;
            //        case "NOTSENT":
            //            mes_t_sb.Text =" ";

            //            break;

            //    }

            //    reader.Close();
            //    writer.Close();
            //    stream.Close();
            //}
        }
    }
}
