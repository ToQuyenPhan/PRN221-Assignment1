using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> ReportOrders(DateTime startDate, DateTime endDate) => OrderDAO.Instance.ReportOrders(startDate, endDate);

        public IEnumerable<Order> SearchByCustomerID(int id) => OrderDAO.Instance.SearchByCustomerID(id);
    }
}
