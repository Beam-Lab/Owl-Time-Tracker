using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeamLab.ProjectsTracker.Models;

namespace BeamLab.ProjectsTracker.Web.Repository
{
    public class MockRepository : IRepository
    {
        public void AddHoursToProject(DateTime Date, int Hours, string Description, string userId)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetMyProjects(string userId)
        {
            var projects = new List<Project>();

            for (int i = 0; i < 15; i++)
            {
                projects.Add(new Project() { ID = i, Name = $"Project {i}", Description = $"Description {i}", DueDate = DateTime.Today.AddDays(i), BudgetHours = i * 5 });
            }

            return projects;
        }

        public Project GetProjectById(int id)
        {
            return new Project() { ID = id, Name = $"Project {id}", Description = $"Description {id}", DueDate = DateTime.Today.AddDays(id), BudgetHours = id * 5 };
        }

        public Task<string> InviteUser(int projectID, string userEmail)
        {
            throw new NotImplementedException();
        }

        public Project SaveProject(Project project, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
