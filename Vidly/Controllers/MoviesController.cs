using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies/
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Sharek!" };

            List<Customer> cust = new List<Customer>()
            {
                new Customer { Name ="Customer 1", Id = 1 },
                new Customer { Name = "Customer 2", Id = 2}
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel();
            viewModel.Movie = movie;
            viewModel.Customer = cust;
            return View(viewModel);
        }

        public ActionResult Index()
        {
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int Id)
        {
            Movie mov = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
            return View(mov);
        }

        [Route("movies/released/{year:range(1900,2050)}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int? year, int? month)
        {
            return Content(String.Format("Year = {0}    Month = {1}", year, month));
        }
    }
}