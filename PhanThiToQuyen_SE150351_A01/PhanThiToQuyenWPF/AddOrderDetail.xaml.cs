using BusinessObject.Models;
using DataAccess.Repositories;
using PhanThiToQuyenWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for AddOrderDetail.xaml
    /// </summary>
    public partial class AddOrderDetail : Window
    {
        private readonly IGenericRepository<OrderDetail> genericRepository = new GenericRepository<OrderDetail>();
        private readonly IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private OrderViewModel OrderDetail;
        private bool InsertOrUpdate;
        private OrderDetailViewModel OrderDetailViewModel;

        public AddOrderDetail(OrderViewModel orderDetail, bool insertOrUpdate, OrderDetailViewModel orderDetailViewModel, string title)
        {
            InitializeComponent();
            OrderDetail = orderDetail;
            InsertOrUpdate = insertOrUpdate;
            OrderDetailViewModel = orderDetailViewModel;
            Title = title;
        }

        private void AddOrderDetailWindow_Load(object sender, RoutedEventArgs e)
        {
            IGenericRepository<FlowerBouquet> flowerBouquetRepository = new GenericRepository<FlowerBouquet>();
            var flowerBouquets = flowerBouquetRepository.GetAll();
            if (InsertOrUpdate == true)
            {
                txtPrice.Text = OrderDetailViewModel.UnitPrice.ToString();
                txtQuantity.Text = OrderDetailViewModel.Quantity.ToString();
                txtDiscount.Text = OrderDetailViewModel.Discount.ToString();
                var selectedFlowerBouquet = flowerBouquetRepository.GetById(OrderDetailViewModel.FlowerBouquetId);
                foreach (FlowerBouquet flowerBouquet in flowerBouquets)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = flowerBouquet.FlowerBouquetName;
                    item.Tag = flowerBouquet.FlowerBouquetId;
                    cbBouquetName.Items.Add(item);
                    if (item.Content.Equals(selectedFlowerBouquet.FlowerBouquetName))
                    {
                        cbBouquetName.SelectedItem = item;
                    }
                }
            }
            else
            {
                foreach (FlowerBouquet flowerBouquet in flowerBouquets)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = flowerBouquet.FlowerBouquetName;
                    item.Tag = flowerBouquet.FlowerBouquetId;
                    cbBouquetName.Items.Add(item);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = OrderDetail.OrderId,
                    FlowerBouquetId = Convert.ToInt32(cbBouquetName.SelectedValue),
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    Quantity = Int32.Parse(txtQuantity.Text),
                    Discount = float.Parse(txtDiscount.Text)
                };
                var order = new Order
                {
                    OrderId = OrderDetail.OrderId,
                    CustomerId = OrderDetail.CustomerId,
                    OrderDate = OrderDetail.OrderDate,
                    ShippedDate = OrderDetail.ShippedDate,
                    Total = OrderDetail.Total,
                    OrderStatus = OrderDetail.OrderStatus,
                };
                if (InsertOrUpdate == false)
                {
                    var orderFlowerBouquet = orderDetailRepository.CheckFlowerBouquet(Convert.ToInt32(cbBouquetName.SelectedValue), OrderDetail.OrderId);
                    if (orderFlowerBouquet == null)
                    {
                        orderDetailRepository.AddOrderDetail(orderDetail, order);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This flower bouquet is alreay exist!", "Add a new flower bouquet to the order");
                    }
                }
                else
                {
                    var checkForUpdate = orderDetailRepository.CheckForUpdate(OrderDetailViewModel.FlowerBouquetId, orderDetail.OrderId, orderDetail.FlowerBouquetId);
                    if (checkForUpdate == null)
                    {
                        orderDetailRepository.DeleteOrderDetails(orderDetail.OrderId, OrderDetailViewModel.FlowerBouquetId) ;
                        orderDetailRepository.UpdateOrderDetails(orderDetail);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("This flower bouquet is alreay exist!", "Update a flower bouquet to the order");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add to the order" : "Update to the order");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
