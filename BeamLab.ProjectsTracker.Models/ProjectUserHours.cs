using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeamLab.ProjectsTracker.Models
{
    public class ProjectUserHours
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double Hours { get; set; }

        public string Description { get; set; }
    }
}
