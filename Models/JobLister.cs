using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace JobHosting.Models
{
    [PrimaryKey(nameof(ListerId))]
    public class JobLister
    {
        public int ListerId { get; set; } = 0;
        public String ListerName { get; set; } = String.Empty;
        public String ListerWebSite { get; set; } = String.Empty;
        public List<int> Listings { get; set; } = new List<int>();

        override
        public String ToString()
        {
            return ListerId + " " + ListerName + " " + ListerWebSite + " " + Listings.Count();
        }
    }
}
