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
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Window
    {
        private readonly IGenericRepository<Order> genericRepository = new GenericRepository<Order>();
        private readonly IOrderRepository orderRepository = new OrderRepository();
        private int? CustomerId { get; set; }

        public ManageOrder()
        {
            InitializeComponent();
        }

        private void ManageOrderWindow_Load(object sender, RoutedEventArgs e) => LoadOrders();

        private void LoadOrders()
        {
            lvOrders.ItemsSource = genericRepository.GetAll();
            txtName.Text = string.Empty;
            txtSearch.Text = string.Empty;
            message.Text = string.Empty;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                try
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to delete this order?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        genericRepository.Delete(Int32.Parse(txtId.Text));
                        message.Text = "Delete successfully!";
                        LoadOrders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Order");
                }
            }
            else
            {
                message.Text = "Can not find the id!";
            }
        }

        private OrderViewModel GetOrderDetail()
        {
            OrderViewModel order = null;
            try
            {
                order = new OrderViewModel
                {
                    OrderId = Int32.Parse(txtId.Text),
                    CustomerId = CustomerId,
                    OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ShippedDate = DateTime.ParseExact(txtShippedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Total = decimal.Parse(txTotal.Text),
                    OrderStatus = txtStatus.Text
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Order Detail");
            }
            return order;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                OrderDetails orderDetails = new OrderDetails(Int32.Parse(txtId.Text), true, GetOrderDetail(), "Update an order");
                orderDetails.Show();
                LoadOrders();
            }
            else
            {
                message.Text = "Please choose an order to update!";
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            OrderDetails orderDetails = new OrderDetails(0, false, null, "Insert an order");
            orderDetails.Show();
            LoadOrders();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadOrders();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = string.Empty;
            if (txtSearch.Text.Length > 0)
            {
                try
                {
                    lvOrders.ItemsSource = orderRepository.SearchByCustomerID(Int32.Parse(txtSearch.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Search Order");
                }
            }
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = lvOrders.SelectedItem as Order;
            if (order != null)
            {
                IGenericRepository<Customer> customerRepository = new GenericRepository<Customer>();
                CustomerId = order.CustomerId;
                var customer = customerRepository.GetById(CustomerId);
                txtName.Text = customer.CustomerName;
            }
        }
    }
}

