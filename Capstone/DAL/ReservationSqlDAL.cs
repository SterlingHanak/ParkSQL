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
        private const string SQL_GetAvailableSites = @"SELECT * FROM reservation";
        private const string SQL_CreateReservation = @"INSERT INTO reservation (site_id, name, to_date, from_date, create_date) VALUES(@site_id, @name, @to_date, @from_date, @create_date)";
        List<Reservation> allReservations = new List<Reservation>();

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


        public List<Reservation> GetAvailableSites()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAvailableSites, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allReservations.Add(PopulateReservationObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return allReservations;
        }


        public bool IsReservationOpen(DateTime startDate, DateTime endDate)
        {
            
            foreach(Reservation party in allReservations)
            {
                if (startDate >= party.From_Date && startDate <= party.To_Date) return false;
                if (endDate >= party.From_Date && endDate <= party.To_Date) return false;
                if (startDate < party.From_Date && endDate > party.To_Date) return false;
            }
            return true;
        }


        public bool CreateReservation(Reservation newReservation)
        {
            bool isAvailable = IsReservationOpen(newReservation.From_Date, newReservation.To_Date);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (isAvailable)
                    {
                        SqlCommand cmd = new SqlCommand(SQL_CreateReservation, conn);
                        cmd.Parameters.AddWithValue("@site_id", newReservation.SiteId);
                        cmd.Parameters.AddWithValue("@name", newReservation.Name);
                        cmd.Parameters.AddWithValue("from_date", newReservation.From_Date);
                        cmd.Parameters.AddWithValue("to_date", newReservation.To_Date);
                        cmd.Parameters.AddWithValue("create_date", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0);
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
