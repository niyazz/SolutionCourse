using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using Newtonsoft.Json;
using TESTER.Models;

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

        public string GetUser(User user) // функция входа в аккаунт - успех "LOGINED"
        {
            string json = null, error = null;
            List<Friends> friends = new List<Friends>();
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
                       Convert.ToInt32(reader[0].ToString()),
                        reader[1].ToString(),
                        reader[2].ToString(),
                        reader[3].ToString(),
                        reader[4].ToString(),
                        Convert.ToInt32(reader[5].ToString()),
                        reader[6].ToString(),
                        reader[7].ToString()
                        );
                    connection.Close();
                    connection.Open();
                    
                    query = $"SELECT friend_id, friend_name FROM Friends WHERE user_id = {logined.User_id}";
                    command = new SqlCommand(query, connection);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            friends.Add(new Friends(Convert.ToInt32(reader[0].ToString()), reader[1].ToString()));
                        }
                        logined.Friends = friends;

                    }
                        Query result = new Query("LOGINED", logined);
                        json = JsonConvert.SerializeObject(result);
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
        public string RegisterUser(User user) // функция регистрации пользователя - успех "REGISTERED"
        {
            string json = "UNREGISTERED";
            string query = $"INSERT INTO Users VALUES (" +
                $"'{user.User_login}'," +
                $"'{user.User_name}'," +
                $"'{user.User_sername}'," +
                $"'{user.User_birthday}'," +
                $"{Convert.ToInt32(user.User_litrs)}," +
                $"'{user.User_mail}'," +
                $"'{user.User_password}')";

            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    Query result = new Query("REGISTERED");
                    json = JsonConvert.SerializeObject(result);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string ChangeLitrsAmount(User user) // функция изменения значения литров пользователя - успех "CHANGED"
        {
            string json = "UNCHANGED";
            string query = $"UPDATE Users SET liters = '{user.User_litrs}' WHERE login='{user.User_login}'";

            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                Query result = new Query("CHANGED");
                json = JsonConvert.SerializeObject(result);

                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string TakeMessages(User user) // функция скачки сообщения пользователя - успех "TAKENMESS"
        {
            string json = null, error = null;
            string query = $"SELECT sName, time, text FROM Messages WHERE aID = '{user.User_id}'";
            Console.WriteLine(query);
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
                    Query result = new Query("TAKENMESS", messages);
                    json = JsonConvert.SerializeObject(result);

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
        public string SendMessage(Message message) // функция отправки сообщения - успех "SENT"
        {
            string date = $"'{message.Time.Day }.{message.Time.Month}.{message.Time.Year}'";
            string json = "UNSENT";

            string query = $"INSERT INTO Messages VALUES (" +
                $"{Convert.ToInt32(message.senderID)}," +
                $"{Convert.ToInt32(message.adressID)}," +
                $"'{message.senderName}'," +
                $"'{message.adressName}'," +
                $" {date}," +
                $"'{message.Text}')";
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    Query result = new Query("SENT");
                    json = JsonConvert.SerializeObject(result);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string TakeCars(User user) // функция скачки машин пользователя - успех "TAKENCARS"
        {
            string json = null, error = null;

            string query = $"SELECT * FROM Cars WHERE user_id = '{user.User_id}'";
            List<Car> cars = new List<Car>();
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

                        cars.Add(new Car(
                           reader[0].ToString(),
                           reader[1].ToString(),
                           reader[2].ToString(),
                           Convert.ToInt32(reader[3].ToString()),
                           reader[4].ToString(),
                           reader[5].ToString(),
                           reader[6].ToString(),
                          Convert.ToInt32(reader[7].ToString())
                           ));
                    }
                    Query result = new Query("TAKENCARS", cars);
                    json = JsonConvert.SerializeObject(result);


                }
                else
                    error = "Ошибка! У вас нет машин!";
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
        public string AddCar(Car car) // функция добавления машины - успех "ADDED"
        {
            string json = "UNADDED";

            string query = $"INSERT INTO Cars VALUES (" +
                $"'{car.Car_region}'," +
                $"'{car.Car_describtion}'," +
                $" {car.Car_old}," +
                $"'{car.Car_numbers}'," +
                $"'{car.Car_mark}'," +
                $"'{car.Car_brand}'," +
                $"{car.id_of_owner})";

            Console.WriteLine(query);
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    Query result = new Query("ADDED");
                    json = JsonConvert.SerializeObject(result);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string AddReview(TESTER.Models.Review review) // функция добавления отзыва - успех "ADDED"
        {
            string json = "UNADDED";

            string query = $"INSERT INTO Reviews VALUES (" +
                $"'{review.carNumber.Trim()}'," +        
                $"'{review.Text}')";
            Console.WriteLine(query);
            SqlConnection connection = new SqlConnection(connectString);

            try
            {
                connection.Open();
                Console.WriteLine("200");
                SqlCommand command = new SqlCommand(query, connection);
                Console.WriteLine("201");
                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("203");
                    Query result = new Query("ADDED");
                    json = JsonConvert.SerializeObject(result);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string AddNews(TESTER.Models.classNews news) // функция добавления новости - успех "ADDED"
        {
            string date = $"'{news.Time.Day }.{news.Time.Month}.{news.Time.Year}'";
            string json = "UNADDED";

            string query = $"INSERT INTO News VALUES (" +
                $"'{news.senderID}'," +
                $" {date}," +
                $"'{news.Text}'," +
                $"'{news.senderName}'" +
                $")";
            Console.WriteLine(query);
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                Console.WriteLine("100");
                if (command.ExecuteNonQuery() == 1)
                {
                    Query result = new Query("ADDED");
                    json = JsonConvert.SerializeObject(result);
                }
                connection.Close();
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string TakeNews() // функция скачки новостей пользователя - успех "TAKENNEWS"
        {
            string json = null, error = null;

            string query = $"SELECT senderName, time, text FROM News ";
            List<TESTER.Models.classNews> news = new List<TESTER.Models.classNews>();
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

                        news.Add(new TESTER.Models.classNews(
                          reader[0].ToString(),
                          Convert.ToDateTime(reader[1].ToString()),
                          reader[2].ToString()
                           ));
                    }
                    Query result = new Query("TAKENNEWS", news);
                    json = JsonConvert.SerializeObject(result);
                }
                else
                    error = "Ошибка! Нет новостей!";
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
        public string TakeReviews() // функция скачки машин пользователя - успех "TAKENCARS"
        {
            string json = null, error = null;

            string query = $"SELECT carnumber, text FROM Reviews ";
            List<TESTER.Models.Review> reviews = new List<TESTER.Models.Review>();
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

                        reviews.Add(new TESTER.Models.Review(
                          reader[0].ToString(),
                          reader[1].ToString()
                           ));
                    }
                    Query result = new Query("TAKENREVIEWS", reviews);
                    json = JsonConvert.SerializeObject(result);
                }
                else
                    error = "Ошибка! Нет отзывов!";
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
        public string AddFriend(User user,Friends friends) // функция добавления друга - успех "ADDED"
        {
            string json = "UNADDED";
            string query = $"SELECT name, login FROM Users WHERE id = {friends.FriendID}";
            SqlConnection connection = new SqlConnection(connectString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    query = $"INSERT INTO Friends VALUES({friends.UserID}, {friends.FriendID}, '{reader[0].ToString()}')";
                    connection.Close();
                    connection.Open();
              
                    command = new SqlCommand(query, connection);
                    if (command.ExecuteNonQuery() == 1)
                    {

                        query = $"INSERT INTO Friends VALUES({friends.FriendID}, {friends.UserID}, '{user.User_name}')";
                        connection.Close();
                        connection.Open();

                        command = new SqlCommand(query, connection);
                        if (command.ExecuteNonQuery() == 1)
                        {
                            Query result = new Query("ADDED");
                            json = JsonConvert.SerializeObject(result);
                            connection.Close();
                        }

                           
                    }
                }
                else
                {
                    json = "Ошибка! Такого пользователя не существует"; 
                }
                
            }
            catch
            {
                connection.Close();
            }

            return json;
        }
        public string TakeFriends(User user) // функция скачки друзей пользователя - успех "TAKENFRIENDS"
        {
            string json = null, error = null;
            List<Friends> friends = new List<Friends>();
            string query = query = $"SELECT friend_id, friend_name FROM Friends WHERE user_id = {user.User_id}";

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
                        friends.Add(new Friends(Convert.ToInt32(reader[0].ToString()), reader[1].ToString()));
                    }
                    user.Friends = friends;
                    Query result = new Query("TAKENFRIENDS", user);
                    json = JsonConvert.SerializeObject(result);
                }
                else
                    error = "Ошибка! Нет Друзей!";
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