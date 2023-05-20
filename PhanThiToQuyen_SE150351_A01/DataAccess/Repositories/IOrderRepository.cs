using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> SearchByCustomerID(int id);
        IEnumerable<Order> ReportOrders(DateTime startDate, DateTime endDate);
    }
}
