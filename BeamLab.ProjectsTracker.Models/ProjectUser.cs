using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BeamLab.ProjectsTracker.Models
{
    public class ProjectUser
    {
        public int ID { get; set; }

        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
