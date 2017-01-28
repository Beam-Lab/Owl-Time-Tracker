using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeamLab.OwlTimeTracker.Web.Models
{
    public class Activity
    {

        public int ID { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Project Project { get; set; }

        public ApplicationUser User { get; set; }

    }
}
