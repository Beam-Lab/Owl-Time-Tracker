using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeamLab.OwlTimeTracker.Web.Models
{
    public class ProjectUser
    {
        public int ID { get; set; }

        public Project Project { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsAdmin { get; set; }

    }
}
