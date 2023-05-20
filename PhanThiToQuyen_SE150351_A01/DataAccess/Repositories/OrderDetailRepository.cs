using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void AddOrderDetail(OrderDetail orderDetail, Order order) => OrderDetailDAO.Instance.AddOrderDetail(orderDetail, order);

        public OrderDetail CheckFlowerBouquet(int flowerBouquetiId, int orderId) => OrderDetailDAO.Instance.CheckFlowerBouquet(flowerBouquetiId, orderId);

        public OrderDetail CheckForUpdate(int flowerBouquetiId, int orderId, int newFlowerBouquetId) => OrderDetailDAO.Instance.CheckForUpdate(flowerBouquetiId, orderId, newFlowerBouquetId);

        public void DeleteOrderDetails(int orderId, int flowerBouquetiId) => OrderDetailDAO.Instance.DeleteOrderDetails(orderId, flowerBouquetiId);

        public IEnumerable<OrderDetail> GetOrderDetails(int id) => OrderDetailDAO.Instance.GetOrderDetails(id);

        public void UpdateOrderDetails(OrderDetail orderDetail) => OrderDetailDAO.Instance.UpdateOrderDetails(orderDetail);
    }
}
