using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails(int id);
        void AddOrderDetail(OrderDetail orderDetail, Order order);
        OrderDetail CheckFlowerBouquet(int flowerBouquetiId, int orderId);
        void DeleteOrderDetails(int orderId, int flowerBouquetiId);
        void UpdateOrderDetails(OrderDetail orderDetail);
        OrderDetail CheckForUpdate(int flowerBouquetiId, int orderId, int newFlowerBouquetId);
    }
}
