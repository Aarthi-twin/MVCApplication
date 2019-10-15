using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Movie
    {
        public int id { get; set; }

        [Display(Name = "Movie Name")]
        [StringLength(30)]
        [Required]
        [Column(TypeName = "varchar")]
        public string MovieName { get; set; }

        [Display(Name = "Date of Release")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Date Added")]
        [Required]
        public DateTime? DateAdded { get; set; }

        //Create Foreign Key

        //Reference Table
      
        public Genre Genre { get; set; }

        //Reference Column
        [Required]
        [Display(Name ="Genre")]
        public int? GenreId { get; set; }

        [Required]
        [Display(Name = "Stocks Available")]
        public int? AvailableStock { get; set; }
    }
}