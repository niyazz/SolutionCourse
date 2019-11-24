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
            byte[] data = new byte[256];
            Console.WriteLine("Waiting for a connection... ");

            while (true)
            {
                TcpClient client = serverSocket.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                Database db = new Database();
                
                string json = reader.ReadLine();
                Console.WriteLine(json);
               // Console.ReadLine();
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
                    case "UPDATE":
                        operationResult = db.Change_litrs(query.User);
                        break;

                }

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(operationResult);
                Console.WriteLine("Отправлено: " + operationResult);
                
                writer.Close();
                reader.Close();
                stream.Close();
                client.Close();
              
            }
            









            /* Альтернативная (как я понял устаревшая)  реализация */

            //static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Any, 908));
            //socket.Listen(5);
            //Socket client = socket.Accept();

            //Console.WriteLine("Connected new client");

            //byte[] buffer = new byte[1024];
            //client.Receive(buffer);

            //Console.WriteLine(Encoding.ASCII.GetString(buffer));

            /*while (true) //цикловая отправка
             {
              string message = reader.ReadLine();
              Console.WriteLine("Получено: " + message);
             }*/

        }
    }
}
