using Capstone.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    class ParkSqlDAL : IParkDAL
    {
        private string connectionString;

        private const string SQL_SeeParks = @"SELECT * FROM park";

        // Single Parameter Constructor
        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> SeeParks()
        {
            List<Park> allParks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SeeParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allParks.Add(PopulateParkObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw; 
            }
            return allParks;
        }

        public Park PopulateParkObject(SqlDataReader reader)
        {
            return new Park()
            {
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["name"]),
                Description = Convert.ToString(reader["description"]),
                Location = Convert.ToString(reader["location"]),
                Area = Convert.ToInt32(reader["area"]),
                EstablishDate = Convert.ToDateTime(reader["establish_date"]),
                Visitors = Convert.ToInt32(reader["visitors"])
            };
        }
    }
}
