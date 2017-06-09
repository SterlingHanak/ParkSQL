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
    class ReservationSqlDAL : IReservationDAL
    {
        private string connectionString;
        private const string SQL_IsCampgroundOpen = @"SELECT * FROM campground WHERE campground_id = @campground_id";
        private const string SQL_GetAvailableSites = @"SELECT * FROM site INNER JOIN reservation ON reservation.site_id = site.site_id WHERE campground_id = @campground_id";

        // Single Parameter Constructor
        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool IsCampgroundOpen(Campground camp, DateTime start_date, DateTime end_date)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_IsCampgroundOpen, conn);
                    cmd.Parameters.AddWithValue("@campground_id", camp.CampgroundId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (start_date.Month >= camp.Open_From && end_date.Month < camp.Open_To)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return false;
        }

        //public List<Site> GetAvailableSites(Site campsite, DateTime startDate, DateTime endDate)
        //{
        //    List<Site> availableSites = new List<Site>();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(SQL_GetAvailableSites, conn);
        //            cmd.Parameters.AddWithValue("@campground_id", campsite.CampgroundId);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                if (!(startDate >= from_date && startDate <= to_date) || !(endDate >= from_date && endDate <= to_date))
        //                {
        //                    availableSites.Add(PopulateSiteObject(reader));
        //                }
        //            }

        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw;
        //    }
        //}

        public Reservation PopulateReservationObject(SqlDataReader reader)
        {
            return new Reservation()
            {
                ReservationId = Convert.ToInt32(reader["reservation_id"]),
                SiteId = Convert.ToInt32(reader["site_id"]),
                Name = Convert.ToString(reader["name"]),
                From_Date = Convert.ToDateTime(reader["from_date"]),
                To_Date = Convert.ToDateTime(reader["from_date"]),
                Create_Date = Convert.ToDateTime(reader["create_date"])
            };
        }
    }
}
