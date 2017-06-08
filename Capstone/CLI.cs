using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class CLI
    {
        const string Command_AllParks = "1";
        const string Command_Park_Campgrounds = "2";
        const string Command_Quit = "q";
        readonly string DatabaseConnection = System.Configuration.ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

        public void RunCLI()
        {
            PrintHeader();
            PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_AllParks:
                        GetAllParks();
                        break;

                    case Command_Park_Campgrounds:
                        ViewParkCampgrounds();
                        break;


                    case Command_Quit:
                        { 
}
                        Console.WriteLine("Thank you for using the project organizer");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;

                }

                PrintMenu();
            }
        }

        public void PrintHeader()
        {
            Console.WriteLine("");
            Console.WriteLine("************PARKS RESERVATION SYSTEM**************");
            Console.WriteLine("");
            Console.WriteLine("");

        }

        public void PrintMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("Main Menu Please type in a command");
            Console.WriteLine(" 1 - Show all parks");
            Console.WriteLine(" 2 - Choose a park and view all campgrounds");

        }

        private void GetAllParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.GetParks();

            if (parks.Count > 0)
            {
                parks.ForEach(park =>
                {
                    Console.WriteLine(park);
                });
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }
        }

        private void ViewParkCampgrounds()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
        }


    }
}


