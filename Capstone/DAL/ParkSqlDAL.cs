using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    class ParkSqlDAL
    {
        private string connectionString;

        // Single Parameter Constructor
        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> SeeParks()
        {
            throw new NotImplementedException();
        }
    }
}
