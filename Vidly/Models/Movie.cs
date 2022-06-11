using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name ="Number In Stock")]
        [Range(0,20)]
        public int NoInStock { get; set; }

        public Genre Genre { get; set; }
        
        [Required]
        [Display(Name ="Genre")]
        public int GenreId { get; set; }

        public int Availability { get; set; }
    }
}