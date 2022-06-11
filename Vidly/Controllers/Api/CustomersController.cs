using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //GET   /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType);
            if (!string.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(x => x.Name.Contains(query));
            }
            var customers = customersQuery.ToList();
            var customerDtos = customers.Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //Get   /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST    /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Customer customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customer);
        }

        //PUT   /api/customer/id
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customerInDB == null)
            {
                return NotFound();
            }

            Mapper.Map(customerDto, customerInDB);
            _context.SaveChanges();
            return Ok();
        }

        //DELETE    /api/customer/id
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDB);
            _context.SaveChanges();
            return Ok();
        }
    }
}
