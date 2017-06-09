using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Interfaces
{
    interface ICampgroundDAL
    {
        List<Campground> GetAllCampgrounds(int park_id);
    }
}
