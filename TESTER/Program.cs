using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace TESTER
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Будто создаем дом с названием "Any", и в нем квартира под 908 номером, нас интересует, то что происходит в этой квартире */
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 908); //создаем сокет
            serverSocket.Start();

            /* Массивы байтов - то как сервер и клиент "общаются между собой", там будут строки, числа, картинки и т.д. - всё в байтах */
            byte[] data = new byte[256];

            /* Ждем подключения от какого-либо клиента */
            Console.Write("Waiting for a connection... ");
            TcpClient client = serverSocket.AcceptTcpClient();
            Console.WriteLine("Connected!");

            /* Если поймали клиента - хватаем данные которые он передал */
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            string message = reader.ReadLine();
            Console.WriteLine("Получено: " + message);

            /*while (true) //цикловая отправка
             {
              string message = reader.ReadLine();
              Console.WriteLine("Получено: " + message);
             }*/

            /* Отправляем сообщение на клиент */
            StreamWriter writer = new StreamWriter(stream);
            Console.WriteLine("Отправлено: " + "Go");
            writer.WriteLine("Go");

            /* Закрываем подключение клиента при нажатии какой-нибудь клавиши (при цикловой отправке)*/
            writer.Close();
            reader.Close();
            stream.Close();
            client.Close();

            Console.ReadLine();









            /* Альтернативная (как я понял устаревшая)  реализация */

            //static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Any, 908));
            //socket.Listen(5);
            //Socket client = socket.Accept();

            //Console.WriteLine("Connected new client");

            //byte[] buffer = new byte[1024];
            //client.Receive(buffer);

            //Console.WriteLine(Encoding.ASCII.GetString(buffer));
        }
    }
}
