using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public Customer Login(string email, string password) => CustomerDAO.Instance.Login(email, password);

        public IEnumerable<Customer> SearchByName(string name) => CustomerDAO.Instance.SearchByName(name);
    }
}
