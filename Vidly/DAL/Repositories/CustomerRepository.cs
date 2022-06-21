using Vidly.DAL.Services;
using Vidly.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Vidly.DAL.Repositories
{
    public class CustomerRepository : ICustomerService
    {
        private ApplicationDbContext _context;

        public CustomerRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Customer> GetCustomers(string query)
        {
            var customerQuery = _context.Customers.Include(c => c.MembershipType);
            if (!string.IsNullOrWhiteSpace(query))
            {
                customerQuery = customerQuery.Where(c => c.Name.ToLower().Contains(query.ToLower()));
            }
            return customerQuery.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(c => c.MembershipType)
                                     .Where(c => c.Id == id)
                                     .SingleOrDefault();
        }

        public Customer CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            var customerInDB = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
            if (customerInDB != null)
            {
                customerInDB = customer;
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.Where(c => c.Id == id).SingleOrDefault();
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}