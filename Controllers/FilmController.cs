using System.Diagnostics;
using _18._10_Best_Films_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace _18._10_Best_Films_MVC.Controllers
{
    public class FilmController : Controller
    {
        FilmContext db;
        public FilmController(FilmContext context) => db = context;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FilmCard()
        {
            IEnumerable<Film> films = await Task.Run(() => db.Films);
            ViewBag.Films = films.ToList();
            return View();
        }
    }
}
