using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public int Open_From { get; set; }
        public int Open_To { get; set; }
        public decimal DailyFee { get; set; }

        public string GetMonthName(int monthNumber)
        {
            if (monthNumber == 1) return "January";
            if (monthNumber == 2) return "February";
            if (monthNumber == 3) return "March";
            if (monthNumber == 4) return "April";
            if (monthNumber == 5) return "May";
            if (monthNumber == 6) return "June";
            if (monthNumber == 7) return "July";
            if (monthNumber == 8) return "August";
            if (monthNumber == 9) return "September";
            if (monthNumber == 10) return "October";
            if (monthNumber == 11) return "November";
            if (monthNumber == 12) return "December";
            return "Unknown Month";
        }
    }
}
