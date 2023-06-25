using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using smartstall.Models;
using smartstall;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.Extensions.Configuration;

namespace smartstall.Services
{
    public class DbWriteService
    {
        private string connectionString;
        private readonly IConfiguration _configuration;
        public DbWriteService(IConfiguration configuration)

        {
            _configuration = configuration;
            // Die Verbindungszeichenfolge aus der Konfigurationsdatei lesen                       
            connectionString = _configuration.GetConnectionString("DefaultConnection");

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
                    command.Parameters.AddWithValue("@Timestamp", data.Timestamp);
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