using Capstone.DAL;
using Capstone.Interfaces;
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
        Reservation userReservation = new Reservation();


        public void Display(Park park)
        {

            ICampgroundDAL campgroundsInPark = new CampgroundSqlDAL(databaseConnection);
            //campgroundsInPark.GetAllCampgrounds(park.ParkId);
            List<Campground> campgrounds = campgroundsInPark.GetAllCampgrounds(park.ParkId);

            Console.WriteLine("Camp ID".PadRight(10) + "Camp Name".PadRight(35) + "Camp Open Date".PadRight(20) + "Camp Closing Date".PadRight(20) + "Daily Fee".PadRight(20));
            foreach (Campground camp in campgrounds)
            {
                Console.WriteLine(camp.CampgroundId.ToString().PadRight(10) + camp.Name.PadRight(35) + camp.GetMonthName(camp.Open_From).PadRight(20) + camp.GetMonthName(camp.Open_To).PadRight(20) + camp.DailyFee.ToString("C2").PadRight(20));
            }

            Console.WriteLine();
            CampGroundChoices(campgrounds);

        }

        public void CampGroundChoices(List<Campground> campgrounds)
        {

            Console.WriteLine("Select a campground to check for availability. Please select a number");

            for (int i = 0; i < campgrounds.Count; i++)
            {
                Console.WriteLine((i + 1).ToString().PadRight(10) + campgrounds[i].Name);
            }

            int parsedUserInput = 0;

            while (parsedUserInput <= 0 || parsedUserInput > campgrounds.Count)
            {
                string userInput = Console.ReadLine();
                Int32.TryParse(userInput, out parsedUserInput);
                if (parsedUserInput <= 0 || parsedUserInput > campgrounds.Count)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }

            Campground userCampground = campgrounds[parsedUserInput - 1];
            Console.WriteLine("You Selected campground " + userCampground.Name);
            Console.WriteLine();

            DateTime startDate = DateTime.MinValue;

            while (startDate == DateTime.MinValue)
            {
                Console.WriteLine("What is your start date? (MM/DD/YYYY)");
                Console.WriteLine();
                string userStartDate = Console.ReadLine();
                DateTime.TryParse(userStartDate, out startDate);
            }

            Console.WriteLine("Your start date is " + startDate.ToShortDateString());
            Console.WriteLine();

            DateTime endDate = DateTime.MinValue;

            while (endDate == DateTime.MinValue)
            {
                Console.WriteLine("What is your end date? (MM/DD/YYYY)");
                Console.WriteLine();
                string userEndDate = Console.ReadLine();
                DateTime.TryParse(userEndDate, out endDate);
            }

            Console.WriteLine("Your end date is " + endDate.ToShortDateString());

            IReservationDAL dal = new ReservationSqlDAL(databaseConnection);
            List<int> numberOfSites = dal.GetTotalSites(userCampground.CampgroundId);
            List<Reservation> allReservations = dal.GetAllReservations();
            List<int> openSites = dal.IsReservationOpen(startDate, endDate, allReservations, numberOfSites);

            bool isOpen = dal.IsCampgroundOpen(userCampground, startDate, endDate);

            if (isOpen)
            {
                IReservationDAL reservation = new ReservationSqlDAL(databaseConnection);

                ISiteDAL siteDAL = new SiteSqlDAL(databaseConnection);
                List<Site> availableSites = siteDAL.GetAvailableSites(userCampground.CampgroundId);

                foreach (Site camp in availableSites)
                {
                    if (numberOfSites.Contains(camp.SiteId))
                    {
                        Console.WriteLine("Site #" + camp.SiteNumber);
                        Console.WriteLine(" Max Occupancy: " + camp.MaxOccupancy);
                        Console.WriteLine(" Handicap Accessible: " + camp.yesOrNo(camp.Accessible));
                        Console.WriteLine(" Max RV Length: " + camp.RvLength.ToString());
                        Console.WriteLine(" Utilities Available: " + camp.yesOrNo(camp.HasUtilities));
                        Console.WriteLine(" Total Fee: " + userCampground.DailyFee * Convert.ToInt32((endDate.Subtract(startDate)).TotalDays));
                        Console.WriteLine();
                    }
                }
                if (availableSites.Count == 0)
                {
                    Console.WriteLine("No available sites for that time period");

                }
            }
            else
            {
                Console.WriteLine("The campground is not open during that period");
            }


            Console.WriteLine("What site number do you choose?");
            int userSite = Convert.ToInt32(Console.ReadLine());

            if (numberOfSites.Contains(userSite))
            {
                userReservation.SiteId = userSite;
            }
            else
            {
                Console.WriteLine("That was not a valid site number");
            }

            Console.WriteLine("What name would you like to book this reservation under? ");
            string userName = Console.ReadLine();

            userReservation.Name = userName;
            userReservation.From_Date = startDate;
            userReservation.To_Date = endDate;
            userReservation.Create_Date = DateTime.Now;

            bool reservationSuccess = dal.CreateReservation(userReservation);
            if (reservationSuccess)
            {
                List<Reservation> updatedReservations = dal.GetAllReservations();
                
                Console.WriteLine("Your reservation has been successfully booked!");
                Console.WriteLine("Your confirmation id is " + (updatedReservations.Count));

            }

        }

    }

}






