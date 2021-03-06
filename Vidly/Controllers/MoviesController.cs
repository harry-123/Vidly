﻿using System;
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
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            IEnumerable<Genre> genre = _context.Genres.ToList();

            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                Genres = genre
            };
            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            MovieFormViewModel viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                MovieFormViewModel viewModelObj = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModelObj);
            }

            else
            {
                if (movie.Id == 0)
                {
                    movie.DateAdded = DateTime.Now;
                    _context.Movies.Add(movie);
                }
                else
                {
                    var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                    movieInDB.Name = movie.Name;
                    movieInDB.NoInStock = movie.NoInStock;
                    movieInDB.ReleaseDate = movie.ReleaseDate;
                    movieInDB.Genre = movie.Genre;
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }  
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