using BusinessObject.Context;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();

        private OrderDAO()
        {
        }

        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null) instance = new OrderDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Order> SearchByCustomerID(int id)
        {
            using (var context = new FUFlowerBouquetManagement())
            {
                return context.Orders.Where(c => c.CustomerId == id).ToList();
            }
        }

        public IEnumerable<Order> ReportOrders(DateTime startDate, DateTime endDate)
        {
            using (var context = new FUFlowerBouquetManagement())
            {
                return context.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).OrderByDescending(o => o.OrderDate).ToList();
            }
        }
    }
}
