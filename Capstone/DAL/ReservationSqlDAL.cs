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
        private const string SQL_GetAllReservations = @"SELECT * FROM reservation";
        private const string SQL_CreateReservation = @"INSERT INTO reservation (site_id, name, to_date, from_date, create_date) VALUES(@site_id, @name, @to_date, @from_date, @create_date)";
        private const string SQL_GetTotalSites = @"SELECT site_id FROM site WHERE campground_id = @campground_id";
        List<Reservation> allReservations = new List<Reservation>();
        List<int> numberOfSites = new List<int>();

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

        public List<int> GetTotalSites(int campground_id)
        {
            int sum = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetTotalSites, conn);
                    cmd.Parameters.AddWithValue("@campground_id", campground_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        sum += 1;
                        numberOfSites.Add(sum);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return numberOfSites;
        }


        public List<Reservation> GetAllReservations()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllReservations, conn);

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


        public List<int> IsReservationOpen(DateTime startDate, DateTime endDate, List<Reservation> allReservations, List<int> numberOfSites)
        {
            
            foreach(Reservation party in allReservations)
            {
                if (startDate >= party.From_Date && startDate <= party.To_Date) numberOfSites.Remove(party.SiteId);
                if (endDate >= party.From_Date && endDate <= party.To_Date) numberOfSites.Remove(party.SiteId);
                if (startDate < party.From_Date && endDate > party.To_Date) numberOfSites.Remove(party.SiteId);
            }
            return numberOfSites;
        }


        public bool CreateReservation(Reservation newReservation)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                        SqlCommand cmd = new SqlCommand(SQL_CreateReservation, conn);
                        cmd.Parameters.AddWithValue("@site_id", newReservation.SiteId);
                        cmd.Parameters.AddWithValue("@name", newReservation.Name);
                        cmd.Parameters.AddWithValue("@from_date", newReservation.From_Date);
                        cmd.Parameters.AddWithValue("@to_date", newReservation.To_Date);
                        cmd.Parameters.AddWithValue("@create_date", DateTime.Now);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
