using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public byte Accessible { get; set; }
        public int RvLength { get; set; }
        public byte HasUtilities { get; set; }

        public string yesOrNo (byte bitValue)
        {
            if (bitValue == 0) return "NO";
            if (bitValue == 1) return "YES";
            return "UNKNOWN";
        }
    }
}
