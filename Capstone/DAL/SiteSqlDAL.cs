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
    class SiteSqlDAL : ISiteDAL
    {
        private string connectionString;
        private string SQL_GetAllSites = @"SELECT TOP 5 * FROM site WHERE campground_id = @campground_id";

        // Single Parameter Constructor
        public SiteSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Site> GetAvailableSites(int campground_id)
        {
            List<Site> AvailableSites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllSites, conn);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AvailableSites.Add(PopulateSiteObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return AvailableSites;
        }

        public Site PopulateSiteObject(SqlDataReader reader)
        {
            return new Site()
            {
                SiteId = Convert.ToInt32(reader["site_id"]),
                CampgroundId = Convert.ToInt32(reader["campground_id"]),
                SiteNumber = Convert.ToInt32(reader["site_number"]),
                MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]),
                Accessible = Convert.ToByte(reader["accessible"]),
                RvLength = Convert.ToInt32(reader["max_rv_length"]),
                HasUtilities = Convert.ToByte(reader["utilities"])
            };
        }
    }
}
