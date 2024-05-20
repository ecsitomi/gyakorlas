using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //HOZZÁADNI

namespace WindowsFormsApp1
{
    internal class BookService //adatkapcsolat létrehozása
        //Project/Manage Nuget packages/MySql.Data install szükséges
    {
        //jellenzők
        private static string DB_HOST = "localhost";
        private static string DB_USER = "root";
        private static string DB_PASSWORD = "";
        private static string DB_DBNAME = "vizsga";

        private MySqlConnection connection;
        public BookService() //konstruktor
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = DB_HOST;
            builder.UserID = DB_USER;
            builder.Password = DB_PASSWORD;
            builder.Database = DB_DBNAME;

            this.connection = new MySqlConnection(builder.ConnectionString);
            this.connection.Open();
        }

        public List<Book> GetBooks() //lekérdezés
        {
            List<Book> list = new List<Book>(); //kell egy lista
            string sql = "SELECT * FROM books"; //sql parancs
            MySqlCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = sql;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read()) //adatbázis sorok objektummá tétele és mentése a listába
                {
                    int id = reader.GetInt32("id");
                    string title = reader.GetString("title");
                    string author = reader.GetString("author");
                    int publish_year = reader.GetInt32("publish_year");
                    int page_count = reader.GetInt32("page_count");

                    Book book = new Book(id, title, author, publish_year, page_count);
                    list.Add(book);
                }
            }
            return list;
        }
        public bool DeleteBook(int id) //Törlés 
        {
            string sql = "DELETE FROM books WHERE id = @id";
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() == 1; 
        }
    }
}
