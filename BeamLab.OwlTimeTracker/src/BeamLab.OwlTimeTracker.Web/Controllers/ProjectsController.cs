using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeamLab.OwlTimeTracker.Web.Data;
using BeamLab.OwlTimeTracker.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BeamLab.OwlTimeTracker.Web.Controllers
{

    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var loggedUser = await _userManager.GetUserAsync(HttpContext.User);

            var projects = await _context.ProjectsUsers.Where(c => c.User == loggedUser).Select(c => c.Project).ToListAsync();

            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Closed,DueDate,Name,StartDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = await _userManager.GetUserAsync(HttpContext.User);

                _context.Add(project);

                _context.ProjectsUsers.Add(new ProjectUser() { User = loggedUser, Project = project, IsAdmin = true });

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Closed,DueDate,Name,StartDate")] Project project)
        {
            if (id != project.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(m => m.ID == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> InviteUser(string ProjectId, string UserName)
        {
            var inviteUser = await _userManager.FindByEmailAsync(UserName);
            var project = await _context.Projects.Where(c => c.ID == Convert.ToInt32(ProjectId)).FirstOrDefaultAsync();

            _context.ProjectsUsers.Add(new ProjectUser() { User = inviteUser, Project = project, IsAdmin = false });
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ID == id);
        }
    }
}
