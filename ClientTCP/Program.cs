using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace ClientTCP // для тестов
{
    class Program
    {
       // static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main(string[] args)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();
            //while (true)
            //{
            string message = Console.ReadLine();
            //byte[] data = Encoding.UTF8.GetBytes(message);
            //stream.Write(data, 0, data.Length);
            //}   
            //first commit
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(message);
            writer.Flush();

            StreamReader reader = new StreamReader(stream);
            string message2 = reader.ReadLine();
            Console.WriteLine("Получен ответ: " + message2);

            reader.Close();
            writer.Close();
            stream.Close();

        }
    
    }
}
