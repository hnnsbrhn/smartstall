using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using smartstall.Models;
using smartstall;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Microsoft.VisualBasic;

namespace smartstall.Services
{
    public class DbReadService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbReadService(IConfiguration configuration)

        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public bool IsAttributeValid(string attribute)
        {
            bool isValid = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM information_schema.columns WHERE table_name = 'sensordata' AND column_name = @Attribute";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Attribute", attribute);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    isValid = count > 0;
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung
                Console.WriteLine("Fehler beim Überprüfen des Spaltennamens: " + ex.Message);
            }

            return isValid;
        }

        public Dictionary<DateTime, string> GetDataForDay(DateTime date, string attribute)
        {
            Dictionary<DateTime, string> readValues = new Dictionary<DateTime, string>();
            if (IsAttributeValid(attribute))
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        string query = "SELECT timestamp, " + attribute + " FROM sensordata WHERE DATE(timestamp) = @Date";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Date", date.Date);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime time = reader.GetDateTime("timestamp");
                                float wert = reader.GetFloat(attribute);
                                //KeyValuePair<DateTime, string> readValue = new KeyValuePair<DateTime, string>(reader.GetDateTime("timestamp"), reader.GetFloat(attribute).ToString());
                                readValues.Add(time, wert.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    Console.WriteLine("Fehler beim Lesen der Daten: " + ex.Message);
                }
            }
            return readValues;
        }

        public Dictionary<DateTime, string> GetDataForWeek(DateTime inputDate, string attribute)
        {
            Dictionary<DateTime, string> readValues = new Dictionary<DateTime, string>();
            if (IsAttributeValid(attribute))
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(_connectionString))
                    {
                        connection.Open();

                        DateTime endDate = inputDate;  // Enddatum ist heute
                        DateTime startDate = endDate.AddDays(-6);  // Startdatum ist vor 6 Tagen

                        // Iteriere über die Tage im Zeitraum rückwärts
                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            string query = "SELECT timestamp, " + attribute + " FROM sensordata WHERE DATE(timestamp) = @Date";

                            MySqlCommand command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Date", date.Date);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    DateTime time = reader.GetDateTime("timestamp");
                                    float value = reader.GetFloat(attribute);
                                    readValues.Add(time, value.ToString());
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung
                    Console.WriteLine("Fehler beim Lesen der Daten: " + ex.Message);
                }
            }
            return readValues;
        }
    }
}