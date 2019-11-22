using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using Newtonsoft.Json;

namespace TESTER
{
    class Database
    {
        public string connectString { get; set; }
        public Database()
        {
            this.connectString = @"Data Source=DESKTOP-3DSDFUN\NIZZOSQL;Initial Catalog=CourseWork;Integrated Security=True";
            // Data Source=DESKTOP-9CPJ5HD;Initial Catalog=CourseWork;Integrated Security=True - Z
            // Data Source=DESKTOP-3DSDFUN\NIZZOSQL;Initial Catalog=CourseWork;Integrated Security=True - N
        }

        public string GetUser(User user)
        {
            string json = null;
            string error = null;
            string query = $"SELECT * FROM Users WHERE login = '{user.User_login}' AND password = '{user.User_password}'";
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    User logined = new User(
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        Convert.ToInt32(reader[5].ToString()),
                        reader[6].ToString(),
                        reader[7].ToString()
                        );

                    Query responce = new Query("LOGINED", logined);
                    json = JsonConvert.SerializeObject(responce);
                }
                else
                    error = "Ошибка! Такого пользователя не существует";
                reader.Close();
                connection.Close();
            }
            catch
            {
                error = "Ошибка! Сервер не смог получить данные из БД";
                connection.Close();
            }
            return json != null ? json : error;
        }

        public string RegisterUser(User user)
        {
            string json = "UNREGISTERED";
            string query = $"INSERT INTO Users VALUES (" +
                $"'{user.User_login}'," +
                $"'{user.User_name}'," +
                $"'{user.User_sername}'," +
                $"{user.User_birthday}," +
                $"{Convert.ToInt32(user.User_litrs)}," +
                $"'{user.User_mail}'," +
                $"'{user.User_password}')";

            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if(command.ExecuteNonQuery() == 1)
                {
                    Query registered = new Query("REGISTERED");
                    json = JsonConvert.SerializeObject(registered);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;

        }
        public string TakeMessages(User user)
        {
            string json = null;
            string error = null;
            string query = $"SELECT sName, time, text FROM Messages WHERE aName = '{user.User_name}'";
            List<Message> messages = new List<Message>();

         
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        messages.Add(new Message(
                           reader[0].ToString(),
                           Convert.ToDateTime(reader[1].ToString()),
                           reader[2].ToString()
                           ));
                    }
                    Query responce = new Query("USERMESSAGES", messages);

                    json = JsonConvert.SerializeObject(responce);
           
                }            
                else
                    error = "Ошибка! У вас нет сообщений!";
                    reader.Close();
                    connection.Close();
            }
            catch
            {
                error = "Ошибка! Сервер не смог получить данные из БД";
                connection.Close();
            }
            return json != null ? json : error;
        }
    }

}