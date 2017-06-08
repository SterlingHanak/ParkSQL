using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    class ReservationSqlDAL
    {
        private string connectionString;

        // Single Parameter Constructor
        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public bool MakeReservation()
        {
            throw new NotImplementedException();
        }
    }
}
