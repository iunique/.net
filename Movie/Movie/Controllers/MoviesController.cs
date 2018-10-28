using Movie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();        public ActionResult Index()        {
            return View(db.Movies.ToList());        }

        public ActionResult Create()        {            return View();        }
        [HttpPost]        public ActionResult Create(Movies movie)        {
            db.Movies.Add(movie);            db.SaveChanges();            return RedirectToAction("Index");        }        public ActionResult Delete(int id)        {
            Movies movies=db.Movies.Find(id);            db.Movies.Remove(movies);            db.SaveChanges();            return RedirectToAction("Index");        }        public ActionResult Edit(int id)        {            Movies movies = db.Movies.Find(id);            return View(movies);        }        [HttpPost]        public ActionResult Adj(Movies movie)        {
            var result = from m in db.Movies
                         where m.ID==movie.ID
                         select m;            foreach (Movies m in result)
            {
                m.Price = movie.Price;
                m.ReleaseDate = movie.ReleaseDate;
                m.Title = movie.Title;
            }
            db.SaveChanges();
            return RedirectToAction("Index");        }
    }
}