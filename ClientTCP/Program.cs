using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace ClientTCP // для тестов
{
    class Program
    {
       
        static void Main(string[] args)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect("localhost", 908);
            NetworkStream stream = clientSocket.GetStream();

            string message = Console.ReadLine();

            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(message);
            writer.WriteLine("Второе сообщение");
            writer.Flush();

            StreamReader reader = new StreamReader(stream);
            string message2 = reader.ReadLine();
            

            reader.Close();
            writer.Close();
            stream.Close();

        }
    
    }
}
