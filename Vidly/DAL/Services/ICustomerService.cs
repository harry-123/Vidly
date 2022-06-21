using Vidly.Models;
using System.Collections.Generic;

namespace Vidly.DAL.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers(string query);
        Customer GetCustomer(int id);
        Customer CreateCustomer(Customer customer);
        void UpdateCustomer(int id, Customer customer);
        void DeleteCustomer(int id);
    }
}
