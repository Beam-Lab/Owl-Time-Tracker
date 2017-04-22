using System;

namespace BeamLab.ProjectsTracker.Models
{
    public class Project
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BudgetHours { get; set; }

        public DateTime DueDate { get; set; }

        public bool Closed { get; set; }
    }
}
