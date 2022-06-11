using System;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {

        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult RentMovie(RentalDto newRental)
        {
            try
            {
                var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);
                if (customer == null)
                {
                    return BadRequest("Customer Id is invalid.");
                }

                var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
                if (movies.Count != newRental.MovieIds.Count)
                {
                    return BadRequest("One or more movie Ids are invalid.");
                }                

                foreach (var movie in movies)
                {
                    if (movie.Availability == 0)
                    {
                        return BadRequest("No Movies are available to rent.");
                    }
                    var rental = new Rental();
                    rental.Customer = customer;
                    rental.Movie = movie;
                    rental.RentalDate = DateTime.Now;
                    _context.Rentals.Add(rental);
                    movie.Availability--;
                }
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
