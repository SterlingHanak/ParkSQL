using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Interfaces
{
    interface ISiteDAL
    {
        List<Site> GetAvailableSites(int campground_id);
    }
}
