using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using smartstall.Models;
using smartstall;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace smartstall.Services
{
    public class DbWriteService
    {
        private string connectionString;

        public DbWriteService()
        {
            // Die Verbindungszeichenfolge aus der Konfigurationsdatei lesen           
            connectionString = "Server=127.0.0.1; port=3306; Database=smartstall; user=root; password=password;";
            //connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }

        public void InsertData(Datenpaket data)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO sensordata (Timestamp, Temperatur, Luftdruck, Luftfeuchtigkeit, Gas, Ammoniak) " +
                                   "VALUES (@Timestamp, @Temperatur, @Luftdruck, @Luftfeuchtigkeit, @Gas, @Ammoniak)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.Parameters.AddWithValue("@Temperatur", data.Temperatur);
                    command.Parameters.AddWithValue("@Luftdruck", data.Luftdruck);
                    command.Parameters.AddWithValue("@Luftfeuchtigkeit", data.Luftfeuchtigkeit);
                    command.Parameters.AddWithValue("@Gas", data.GasResistenz);
                    command.Parameters.AddWithValue("@Ammoniak", data.Ammoniak);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine("Fehler beim Einfügen der Daten: " + ex.Message);
            }
        }
    }
}