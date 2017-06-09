using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Interfaces
{
    interface IReservationDAL
    {
        bool IsCampgroundOpen(Campground camp, DateTime start_date, DateTime end_date);
        List<int> GetTotalSites(int campground_id);
        List<Reservation> GetAllReservations();
        List<int> IsReservationOpen(DateTime startDate, DateTime endDate, List<Reservation> allReservations, List<int> numberOfSites);
        bool CreateReservation(Reservation newReservation);
    }
}
