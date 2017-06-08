using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    class CampgroundSqlDAL
    {
        private string connectionString;
        private const string SQL_GetAllCampgrounds = @"SELECT * FROM campground WHERE park_id = @park_id";

        // Single Parameter Constructor
        public CampgroundSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campground> GetAllCampgrounds(int park_id)
        {
            List<Campground> AllCampgroundsInPark = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllCampgrounds, conn);
                    cmd.Parameters.AddWithValue("@park_id", park_id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AllCampgroundsInPark.Add(PopulateCampgroundObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return AllCampgroundsInPark;
        }

        public Campground PopulateCampgroundObject(SqlDataReader reader)
        {
            return new Campground()
            {
                CampgroundId = Convert.ToInt32(reader["campground_id"]),
                ParkId = Convert.ToInt32(reader["park_id"]),
                Name = Convert.ToString(reader["name"]),
                Open_From = Convert.ToInt32(reader["open_from_mm"]),
                Open_To = Convert.ToInt32(reader["open_to_mm"]),
                DailyFee = Convert.ToDecimal(reader["daily_fee"])
            };
        }
    }
}
