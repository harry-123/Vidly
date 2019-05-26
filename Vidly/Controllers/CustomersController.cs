using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> custList = _context.Customers.Include(c => c.MembershipType).ToList();
            return View("Customers" ,custList);
        }

        public ActionResult CustomerDetails(int id)
        {
            Customer cust = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if(cust == null) 
            {
                return HttpNotFound();
            }
            return View(cust);
        }
    }
}