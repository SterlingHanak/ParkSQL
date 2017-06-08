using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Campground
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string Name { get; set; }
        public DateTime Open_From { get; set; }
        public DateTime Open_To { get; set; }
        public decimal DailyFee { get; set; }
    }
}
