using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApplication.ViewModel
{
    public class CustomerMovieViewModel2
    {
        public Customer Customer { get; set; }
        public List<Movie> Movies { get; set; }
    }
}