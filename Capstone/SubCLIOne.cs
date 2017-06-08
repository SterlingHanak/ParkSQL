using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class SubCLIOne
    {
        readonly string databaseConnection = System.Configuration.ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;


        public void Display(Park park)
        {



            CampgroundSqlDAL campgroundsInPark = new CampgroundSqlDAL(databaseConnection);
            //campgroundsInPark.GetAllCampgrounds(park.ParkId);
            List<Campground> campgrounds = campgroundsInPark.GetAllCampgrounds(park.ParkId);

            Console.WriteLine("Camp ID".PadRight(10) + "Camp Name".PadRight(35) + "Camp Open Date".PadRight(20) + "Camp Closing Date".PadRight(20) + "Daily Fee".PadRight(20));
            foreach (Campground camp in campgrounds)
            {
                Console.WriteLine(camp.CampgroundId.ToString().PadRight(10) + camp.Name.PadRight(35) + camp.GetMonthName(camp.Open_From).PadRight(20) + camp.GetMonthName(camp.Open_To).PadRight(20) + camp.DailyFee.ToString("C2").PadRight(20));
            }

            Console.WriteLine();

        }



    }
}
