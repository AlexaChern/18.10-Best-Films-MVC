using System.Diagnostics;
using _18._10_Best_Films_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BestFilmsMVC.Controllers
{
    public class FilmController : Controller
    {
        FilmContext db;
        IWebHostEnvironment appEnvironment;
        public FilmController(FilmContext context, IWebHostEnvironment appEnv)
        {
            db = context;
            appEnvironment = appEnv;
        }

        public IActionResult Index() => View();

        public async Task<IActionResult> FilmCard()
        {
            IEnumerable<Film> films = await Task.Run(() => db.Films);
            ViewBag.Films = films.ToList();
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var film = await db.Films.FirstOrDefaultAsync(f => f.Id == id);
            if (film == null) return NotFound();
            return View(film);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,ReleaseYear,Description")] Film film, IFormFile uploadedPoster)
        {
            if (uploadedPoster == null || uploadedPoster.Length == 0)
                return View(film);

            if (ModelState.IsValid)
            {
                string filePath = "\\posters\\" + uploadedPoster.FileName.Replace(" ", "_");
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + filePath, FileMode.Create))
                {
                    await uploadedPoster.CopyToAsync(fileStream);
                }
                film.PosterPath = filePath;
                db.Films.Add(film);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var film = await db.Films.FindAsync(id);
            if (film == null) return NotFound();
            return View(film);
        }

        private bool FilmExists(int id) =>
            db.Films.Any(f => f.Id == id);
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind ("Id,Name,Director,Genre,ReleaseYear,Description")] Film film, IFormFile newPoster)
        {
            if (id != film.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    string filePath = "\\posters\\" + newPoster.FileName.Replace(" ", "_");
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + filePath, FileMode.Create))
                    {
                        await newPoster.CopyToAsync(fileStream);
                    }
                    film.PosterPath = filePath;
                    db.Update(film);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("FilmCard");
            }
            return View(film);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var film = await db.Films.FindAsync(id);
            if (film == null) return NotFound();
            return View(film);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await db.Films.FindAsync(id);
            if (film != null) db.Films.Remove(film);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(FilmCard));
        }
    }
}
