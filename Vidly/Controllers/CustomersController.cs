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

        public ActionResult New()
        {
            IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel newcust = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", newcust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);            
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDB.Name = customer.Name;
                customerInDB.DateOfBirth = customer.DateOfBirth;
                customerInDB.MembershipTypeId = customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id) 
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return HttpNotFound();

            CustomerFormViewModel viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
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