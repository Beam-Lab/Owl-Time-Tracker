using BeamLab.ProjectsTracker.Models;
using BeamLab.ProjectsTracker.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BeamLab.ProjectsTracker.Web.Repository
{
    public class SQLRepository : IRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _dbContext;

        public SQLRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public List<Project> GetMyProjects(string userId)
        {
            var pu = _dbContext.ProjectsUsers
                    .Include(p => p.Project)
                    .Where(c => c.ApplicationUser.Id == userId)
                    .Select(p => p.Project).ToList();

            return pu;
        }

        public Project GetProjectById(int id)
        {
            var p = _dbContext.Projects.Where(c => c.ID == id).FirstOrDefault();

            return p;
        }

        public Project SaveProject(Project project, string userId)
        {
            if (project.ID == 0)
            {
                _dbContext.Projects.Add(project);
            }
            else
            {
                var p = _dbContext.Projects.Where(c => c.ID == project.ID).FirstOrDefault();
                p.ID = project.ID;
                p.Name = project.Name;
                _dbContext.Update(p);
            }

            _dbContext.SaveChanges();

            var pu = _dbContext.ProjectsUsers.Where(c => c.ProjectID == project.ID && c.ApplicationUser.Id == userId).ToList();

            if (pu.Count == 0)
            {
                var user =
                _dbContext.ProjectsUsers.Add(new ProjectUser() { Project = project, ApplicationUser = _userManager.FindByIdAsync(userId).Result });
                _dbContext.SaveChanges();
            }

            return project;
        }

        public async Task<string> InviteUser(int projectID, string userEmail)
        {
            var project = _dbContext.Projects.Where(c => c.ID == projectID).FirstOrDefault();

            var user = await _userManager.FindByNameAsync(userEmail);

            if (user != null)
            {
                var pu = _dbContext.ProjectsUsers.Where(c => c.ProjectID == projectID && c.ApplicationUser.Id == user.Id).ToList();

                if (pu.Count == 0)
                {
                    _dbContext.ProjectsUsers.Add(new ProjectUser() { Project = project, ApplicationUser = user });
                    _dbContext.SaveChanges();
                }
            }

            return string.Empty;
        }

        public async void AddHoursToProject(DateTime Date, int Hours, string Description, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);


        }
    }
}
