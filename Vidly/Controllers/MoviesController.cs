using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Sharek!" };
            //return View(movie);
            //return Content("Hello World");
            //return HttpNotFound();
            return RedirectToAction("Index", "Home", new { page="1", name="Harry"});
        }

        public ActionResult Edit(int id)
        {
            return Content("Id = " + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }
            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name"; 
            }

            return Content(String.Format("pageIndex = {0}     SortBy = {1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:range(1900,2050)}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int? year, int? month)
        {
            return Content(String.Format("Year = {0}    Month = {1}", year, month));
        }
    }
}