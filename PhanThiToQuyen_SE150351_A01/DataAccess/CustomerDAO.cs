using BusinessObject.Context;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private static readonly object instanceLock = new object();

        private CustomerDAO() {
        }

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) instance = new CustomerDAO();
                    return instance;
                }
            }
        }

        public Customer Login(string email, string password)
        {
            using(var context = new FUFlowerBouquetManagement())
            {
                return context.Customers.FirstOrDefault(c => c.Email.Equals(email) && c.Password.Equals(password));
            }
        }

        public IEnumerable<Customer> SearchByName (string name)
        {
            using (var context = new FUFlowerBouquetManagement())
            {
                return context.Customers.Where(c => c.CustomerName.Contains(name)).ToList();
            }
        }
    }
}
