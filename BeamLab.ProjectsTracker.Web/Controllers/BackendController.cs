using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BeamLab.ProjectsTracker.Models;
using Microsoft.AspNetCore.Identity;
using BeamLab.ProjectsTracker.Web.Repository;

namespace BeamLab.ProjectsTracker.Web.Controllers
{
    [Authorize]
    public class BackendController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IRepository _repository;

        public BackendController(
            UserManager<ApplicationUser> userManager,
            IRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var projects = _repository.GetMyProjects(_userManager.GetUserId(HttpContext.User)).OrderByDescending(c => c.DueDate);

            return View(projects);
        }

        public IActionResult EditProject(int id)
        {
            var project = new Project();

            if (id == 0)
            {
                project.DueDate = DateTime.Today.Date;
            }
            else
            {
                project = _repository.GetProjectById(id);
            }

            return View(project);
        }

        [HttpPost]
        public IActionResult CreateProject(Project model)
        {
            _repository.SaveProject(model, _userManager.GetUserId(HttpContext.User));

            return RedirectToAction("Index");
        }

        public IActionResult InviteUser(int id)
        {
            var model = new InviteUser();
            model.Project = id;
            model.Email = string.Empty;

            return View(model);
        }

        [HttpPost]
        public IActionResult InviteUser(InviteUser inviteUser)
        {
            try
            {
                _repository.InviteUser(inviteUser.Project, inviteUser.Email);
            }
            catch (Exception ex)
            {
                var exception = ex;
            }

            return RedirectToAction("Index", new { id = Convert.ToInt32(inviteUser.Project) });
        }

        [HttpPost]
        public IActionResult AddHours(string Project,string Hours)
        {

            return RedirectToAction("Index");
        }
    }
}