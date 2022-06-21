using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using Vidly.DAL.Services;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        //GET   /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customers = _customerService.GetCustomers(query);
            var customerDtos = customers.Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //Get   /api/customers/id
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetCustomer(id);
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
            customer = _customerService.CreateCustomer(customer);

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
          
            var customer =  Mapper.Map<Customer>(customerDto);
            _customerService.UpdateCustomer(id, customer);
            return Ok();
        }

        //DELETE    /api/customer/id
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            _customerService.DeleteCustomer(id);
            return Ok();
        }
    }
}
