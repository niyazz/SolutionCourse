using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;


namespace TESTER
{
    
    class Database
    {
        public string connectString { get; set; }
        public Database()
        {
            this.connectString = @"Data Source=DESKTOP-9CPJ5HD;Initial Catalog=CourseWork;Integrated Security=True";
            // поменяйте на свой путь
        }

        public string GetUser(string login, string password)
        {
            string username = null;
            string error = null;            
            string query = $"SELECT name FROM Users WHERE login = '{login}' AND password = '{password}'";
            SqlConnection connection = new SqlConnection(connectString);

            try
            {
                connection.Open();               
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

               if (reader.Read())
               {
                    Console.WriteLine("Пользователь найден");
                    username = reader[0].ToString();
               }
               else 
                    error = "Ошибка! Такого пользователя не существует";

                reader.Close();
                connection.Close();
            }
            catch
            {
                error = "Ошибка! Сервер не смог получить данные из БД";
            }

            return username != null ? username : error; // если юзер сущетсвует возвращаем его имя(потом будем возвращать набор данных), иначе - ошибку
        }
      
    }


}
