using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace TESTER
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 908); //создаем сокет
            serverSocket.Start();
            Console.WriteLine("Waiting for a connection... ");

            while (true)
            {
                TcpClient client = serverSocket.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                Database db = new Database();

                string json = reader.ReadLine(); // полученные с клиента данные
                Query query = JsonConvert.DeserializeObject<Query>(json);

                string operationResult = "";
                switch (query.Type)
                {
                    case "REGISTRATION":
                        operationResult = db.RegisterUser(query.User);
                        break;
                    case "SIGNIN":
                        operationResult = db.GetUser(query.User);
                        break;
                    case "TAKEMESSAGES":
                        operationResult = db.TakeMessages(query.User);
                        break;
                    case "SENDMES":
                        Console.WriteLine("***SENDING**********");
                        operationResult = db.SendMessage(query.Message);
                        break;
                    case "UPDATELITRS":
                        operationResult = db.ChangeLitrsAmount(query.User);
                        break;
                    case "TAKECARS":
                        operationResult = db.TakeCars(query.User);
                        break;
                    case "ADDCAR":
                        operationResult = db.AddCar(query.Car);
                        break;
                    case "SENDREVIEW":
                        operationResult = db.AddReview(query.Review);
                        break;
                    case "SENDNEWS":
                        operationResult = db.AddNews(query.New);
                        break;
                    case "TAKENEWS":
                        operationResult = db.TakeNews();
                        break;
                    case "TAKEREVIEWS":
                        operationResult = db.TakeReviews();
                        break;

                }

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(operationResult); // запись ответа от сервера
                Console.WriteLine("Отправлено: " + operationResult);

                writer.Close();
                reader.Close();
                stream.Close();
                client.Close();

            }
        }
    }
}
