using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        
        //GET api/movies
        [HttpGet]
        public IHttpActionResult GetMovies(string query = null) 
        {
            var moviesQuery = _context.Movies.Include(m => m.Genre);
            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(x => x.Name.Contains(query));
            }
            var moviesDto = moviesQuery.Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }

        //GET api/movies/id
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return NotFound();
            }

            var movieDto = Mapper.Map<Movie, MovieDto>(movie);
            return Ok(movieDto);
        }

        //POST api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult AddMovie(MovieDto movieDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;
            movieDto.DateAdded = movie.DateAdded;
            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        //Delete api/movies/id
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Ok();
        }

        //PUT api/movies/id       updates the movie object
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto) 
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDB == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Mapper.Map(movieDto, movieInDB);
            _context.SaveChanges();
            return Ok(movieInDB);   
        }
    }
}
