using MVCApplication.Models;
using MVCApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace MVCApplication.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {

        private ApplicationDbContext dbContext = null;
        public MoviesController()
        {
            dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                dbContext.Dispose();
            }
        }
        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            var movies = dbContext.Movies.Include(z=>z.Genre).ToList();
            return View(movies);
        }
        public ActionResult DisplayMovie()
        {
            CustomerMovieViewModel viewModel = new CustomerMovieViewModel();
            Movie m1 = new Movie() { MovieName = "Polladhavan" };
            List<Customer> Customers = new List<Customer>
            {
                new Customer{CustomerName="Aarthi"},
                new Customer{CustomerName="Anu"},
                new Customer{CustomerName="Prasanna"},
                new Customer{CustomerName="Jeya"},
                new Customer{CustomerName="Ganapathy"}
            };

            viewModel.Movie = m1;
            viewModel.Customer = Customers;

            return View(viewModel);
        }
        public ActionResult Details(int id)
        {
            var movies = dbContext.Movies.Include(z=>z.Genre).ToList();
            Movie m = new Movie();
            foreach (var movie in movies)
            {
                if(id==movie.id)
                {
                    m = movie;
                }
            }
            return View(m);
        }
        [HttpGet]
       public ActionResult Create()
        {
            var movie = new Movie();
            ViewBag.GenreId = ListGenre();
            return View(movie);
        }
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Create(Movie movieFromView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenreId = ListGenre();
                return View(movieFromView);
            }
            dbContext.Movies.Add(movieFromView);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [HttpGet]
        public ActionResult EditMovie(int id)
        {
            var movie = dbContext.Movies.SingleOrDefault(c => c.id == id);
            if (movie != null)
            {
                ViewBag.GenreId = ListGenre();
                
                return View(movie);
            }
            return HttpNotFound("Movie ID not Exists");
        }
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult EditMovie(Movie movieFromView)
        {
            if (ModelState.IsValid)
            {
                ViewBag.GenreId = ListGenre();
                var MovieInDB = dbContext.Movies.FirstOrDefault(c => c.id == movieFromView.id);
                MovieInDB.MovieName = movieFromView.MovieName;
                MovieInDB.DateAdded= movieFromView.DateAdded;
                MovieInDB.ReleaseDate = movieFromView.ReleaseDate;
                MovieInDB.AvailableStock = movieFromView.AvailableStock;
                MovieInDB.GenreId = movieFromView.GenreId;
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                ViewBag.Genre = ListGenre();
                return View(movieFromView);
            }
        }

        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            var movieInDB = dbContext.Movies.FirstOrDefault(c => c.id == id);
            if (movieInDB != null)
            {
                dbContext.Movies.Remove(movieInDB);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }

            return HttpNotFound("Movie Id doesn't Exists");
        }
        [NonAction]
        public List<SelectListItem> ListGenre()
        {
            var genre = dbContext.Genres.AsEnumerable().
                 Select(m => new SelectListItem()
                 {
                     Text = m.Name,
                     Value = m.Id.ToString()
                 }).ToList();

            genre.Insert(0, new SelectListItem { Text = "---Select---", Value = "0", Disabled = true, Selected = true });
            return genre;
        }
      
    }
}