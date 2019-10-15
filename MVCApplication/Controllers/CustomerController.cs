using MVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCApplication.ViewModel;
using System.Data.Entity;

namespace MVCApplication.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ApplicationDbContext dbContext = null;
        public CustomerController()
        {
            dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        // GET: Customer
        [AllowAnonymous]
        public ActionResult Index()
        {
            var customers = dbContext.Customers.Include(z => z.MembershipType).ToList();
            return View("IndexCustomer", customers);
        }

        public ActionResult Details(int id)
        {
            //var customers = GetCustomers();
            //Customer c = new Customer();
            //foreach (var customer in customers)
            //{
            //    if(id==customer.id)
            //    {
            //        c = customer;
            //    }
            //}
            //return View(c);

            //USING LINQ WITH LAMBDA EXPRESSION
            //var customer = GetCustomers().SingleOrDefault(a => a.id == id);
            //return View(customer);

            //USING LINQ
            var customer = from cust in dbContext.Customers.Include(c => c.MembershipType).ToList()
                           where cust.id == id
                           select cust;
            return View(customer.SingleOrDefault());
        }
        //public ActionResult DisplayCustomer()
        //{
        //    CustomerMovieViewModel2 viewModel = new CustomerMovieViewModel2();
        //    Customer c = new Customer() { CustomerName = "Aarthi" };
        //    List<Movie> movie = new List<Movie>
        //    {
        //        new Movie{MovieName="Polladhavan"},
        //        new Movie{MovieName="Paiya"},
        //        new Movie{MovieName="Yaaradi nee mohini"},
        //        new Movie{MovieName="OK OK"}
        //    };

        //    viewModel.Customer = c;
        //    viewModel.Movies = movie;
        //    return View(viewModel);
        //}
        [HttpGet]
        public ActionResult Create()
        {
            var customer = new Customer();

            ViewBag.Gender = ListGender();
            ViewBag.MembershipTypeId = ListMembership();
            return View(customer);
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Create(Customer customerFromView)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customerFromView);
            }
            dbContext.Customers.Add(customerFromView);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.id == id);
            if (customer != null)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customer);
            }
            return HttpNotFound("Customer ID not Exists");
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult EditCustomer(Customer customerFromView)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                var customerInDB = dbContext.Customers.FirstOrDefault(c => c.id == customerFromView.id);
                customerInDB.CustomerName = customerFromView.CustomerName;
                customerInDB.City = customerFromView.City;
                customerInDB.Gender = customerFromView.Gender;
                customerInDB.BirthDate = customerFromView.BirthDate;
                customerInDB.MembershipTypeId = customerFromView.MembershipTypeId;
                dbContext.SaveChanges();
               return RedirectToAction("Index", "Customer");
            }
            else
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeId = ListMembership();
                return View(customerFromView);
            }
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            var customerInDB = dbContext.Customers.FirstOrDefault(c => c.id == id);
            if(customerInDB!=null)
            {
                dbContext.Customers.Remove(customerInDB);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }

            return HttpNotFound("Customer Id doesn't Exists");
        }
       [NonAction]
        public List<SelectListItem> ListGender()
        {

            IEnumerable<SelectListItem> gender = new List<SelectListItem>()
            {
                new SelectListItem{Text="---Select a Gender---",Value="0",Disabled=true,Selected=true},
                new SelectListItem{Text="Male",Value="Male"},
                new SelectListItem{Text="Female",Value="Female"},
                new SelectListItem{Text="Others",Value="Others"}
            };
            return gender.ToList();
        }

       [NonAction]
       public List<SelectListItem> ListMembership()
        {
            var membership = (from m in dbContext.MemberShipTypes
                              select new SelectListItem
                              {
                                  Text = m.Type,
                                  Value = m.Id.ToString()
                              }).ToList();
            membership.Insert(0, new SelectListItem { Text = "---Select---", Value = "0" });
            return membership;
        }
    }
}