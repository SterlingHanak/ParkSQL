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
    }
}
