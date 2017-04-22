using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeamLab.ProjectsTracker.Web.Repository;
using System.Security.Claims;

namespace BeamLab.ProjectsTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Home")]
        [Route("", Name = "Home")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}