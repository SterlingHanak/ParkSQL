using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    class Site
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int MaxOccupancy { get; set; }
        public bool Accessible { get; set; }
        public int RvLength { get; set; }
        public bool HasUtilities { get; set; }
    }
}
