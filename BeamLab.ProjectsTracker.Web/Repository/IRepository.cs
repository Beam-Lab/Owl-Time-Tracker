using BeamLab.ProjectsTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeamLab.ProjectsTracker.Web.Repository
{
    public interface IRepository
    {
        List<Project> GetMyProjects(string userId);

        Project GetProjectById(int id);

        Project SaveProject(Project project, string userId);

        Task<string> InviteUser(int projectID, string userEmail);

        void AddHoursToProject(DateTime Date, int Hours, string Description, string userId);
    }
}
