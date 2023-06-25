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
    public class DbReadService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public  DbReadService(IConfiguration configuration)

        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public List<Datenpaket> GetDataByDate(DateTime date)
        {
            List<Datenpaket> data = new List<Datenpaket>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM sensordata WHERE DATE(Timestamp) = @Date";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Date", date.Date);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Datenpaket datapoint = new Datenpaket
                            {
                                Timestamp = reader.GetDateTime("Timestamp"),
                                Temperatur = reader.GetFloat("Temperatur"),
                                Luftdruck = reader.GetFloat("Luftdruck"),
                                Luftfeuchtigkeit = reader.GetFloat("Luftfeuchtigkeit"),
                                GasResistenz = reader.GetFloat("Gas"),
                                Ammoniak = reader.GetFloat("Ammoniak")
                            };

                            data.Add(datapoint);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine("Fehler beim Lesen der Daten: " + ex.Message);
            }

            return data;
        }
    }
}