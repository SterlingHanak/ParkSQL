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
            Console.WriteLine(" Q - Quit program");
            

        }

        private void GetAllParks()
        {
            IParkDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.SeeParks();

            if (parks.Count > 0)
            {
                foreach(Park park in parks)
                {
                    Console.WriteLine("Park ID".PadRight(10) + "Name".PadRight(20) + "Location".PadRight(15) + "Establish Date".PadRight(20) + "Area".PadRight(10) + "Visitors");
                    Console.WriteLine("");
                    Console.WriteLine(park.ParkId.ToString().PadRight(10) + park.Name.PadRight(20) + park.Location.PadRight(15) + park.EstablishDate.ToString("MMMM dd, yyyy").PadRight(20) + park.Area.ToString("N0").PadRight(10) + park.Visitors.ToString("N0"));
                    Console.WriteLine();
                    Console.WriteLine("Description: " + park.Description);
                    Console.WriteLine("");

                }
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }
        }

        private void ViewParkCampgrounds()
        {
            ParkSqlDAL dal = new ParkSqlDAL(DatabaseConnection);
            List<Park> parks = dal.SeeParks();
            Console.WriteLine("Which park would you like to choose?");
            if (parks.Count > 0)
            {
                for(int i = 0; i < parks.Count; i++)
                {
                    Console.WriteLine((i + 1).ToString().PadRight(10) + parks[i].Name);
                }
            }
            int parsedUserInput = 0;
            
            while(parsedUserInput <= 0 || parsedUserInput > parks.Count)
            {
                string userInput = Console.ReadLine();
                Int32.TryParse(userInput, out parsedUserInput);
                if(parsedUserInput <= 0 || parsedUserInput > parks.Count)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            }
            
            Park userPark = parks[parsedUserInput - 1];
            SubCLIOne submenu = new SubCLIOne();

            submenu.Display(userPark);
        }


    }
}


