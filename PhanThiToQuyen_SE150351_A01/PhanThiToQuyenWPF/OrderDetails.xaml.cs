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
using System.Xml.Linq;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        private IGenericRepository<Order> genericRepository = new GenericRepository<Order>();
        private IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        private int OrderId;
        private bool InsertOrUpdate;
        private OrderViewModel OrderDetail;

        public OrderDetails(int orderId, bool insertOrUpdate, OrderViewModel orderDetail, string title)
        {
            InitializeComponent();
            OrderId = orderId;
            InsertOrUpdate = insertOrUpdate;
            OrderDetail = orderDetail;
            Title = title;
        }

        private void OrderDetailWindow_Load(object sender, RoutedEventArgs e)
        {
            LoadOrderDetails();
            txtId.IsReadOnly = InsertOrUpdate;
            IGenericRepository<Customer> customerRepository = new GenericRepository<Customer>();
            var customers = customerRepository.GetAll();
            if (InsertOrUpdate == true)
            {
                txtId.Text = OrderDetail.OrderId.ToString();
                txtOrderDate.Text = OrderDetail.OrderDate.ToString();
                txtShippedDate.Text = OrderDetail.ShippedDate.ToString();
                txtStatus.Text = OrderDetail.OrderStatus.ToString();
                var selectedCustomer = customerRepository.GetById(OrderDetail.CustomerId);
                foreach (Customer customer in customers)
                {
                    ComboBoxItem customerItem = new ComboBoxItem();
                    customerItem.Content = customer.CustomerName;
                    customerItem.Tag = customer.CustomerId;
                    cbCustomerName.Items.Add(customerItem);
                    if (customerItem.Content.Equals(selectedCustomer.CustomerName))
                    {
                        cbCustomerName.SelectedItem = customerItem;
                    }
                }
            }
            else
            {
                foreach (Customer customer in customers)
                {
                    ComboBoxItem customerItem = new ComboBoxItem();
                    customerItem.Content = customer.CustomerName;
                    customerItem.Tag = customer.CustomerId;
                    cbCustomerName.Items.Add(customerItem);
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadOrderDetails();

        private void LoadOrderDetails()
        {
            if (OrderId != 0)
            {
                var total = 0m;
                var orderDetails = orderDetailRepository.GetOrderDetails(OrderId);
                lvOrderDetails.ItemsSource = orderDetails;
                foreach(var item in orderDetails)
                {
                    total +=  (item.UnitPrice * item.Quantity) * (decimal)((100 - item.Discount) / 100);
                }
                txtTotal.Text = total.ToString();
            }
            message.Text = string.Empty;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            message.Text = string.Empty;
            if (GetOrderDetail() != null)
            {
                OrderId = Int32.Parse(txtId.Text);
                AddOrderDetail addOrderDetail = new AddOrderDetail(GetOrderDetail(), true, GetOrderDetailViewModel(), "Update to the order");
                addOrderDetail.Show();
                LoadOrderDetails();
            }
            else
            {
                message.Text = "Please write your order information before add flower bouquet!";
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            message.Text = string.Empty;
            if (GetOrderDetail() != null)
            {
                OrderId = Int32.Parse(txtId.Text);
                AddOrderDetail addOrderDetail = new AddOrderDetail(GetOrderDetail(), false, null, "Add to the order");
                addOrderDetail.Show();
                LoadOrderDetails();
            }
            else
            {
                message.Text = "Please write your order information before add flower bouquet!";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            message.Text = string.Empty;
            OrderDetail orderDetail = lvOrderDetails.SelectedItem as OrderDetail;
            if (orderDetail != null)
            {
                try
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to delete this order?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        orderDetailRepository.DeleteOrderDetails(orderDetail.OrderId, orderDetail.FlowerBouquetId);
                        message.Text = "Delete successfully!";
                        LoadOrderDetails();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Order");
                }
            }
            else
            {
                message.Text = "Cannot find the id!";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = new Order
                {
                    OrderId = Int32.Parse(txtId.Text),
                    CustomerId = Convert.ToInt32(cbCustomerName.SelectedValue),
                    OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ShippedDate = DateTime.ParseExact(txtShippedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Total = decimal.Parse(txtTotal.Text),
                    OrderStatus = txtStatus.Text

                };
                if (InsertOrUpdate == false)
                {
                    if(lvOrderDetails.Items.Count == 0)
                    {
                        message.Text = "Please add a flower bouquet to make an order!";
                    }else
                    {
                        genericRepository.Insert(order);
                        this.Close();
                    }
                }
                else
                {
                    genericRepository.Update(order);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a flower bouquet" : "Update a flower bouquet");
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
                    CustomerId = Convert.ToInt32(cbCustomerName.SelectedValue),
                    OrderDate = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    ShippedDate = DateTime.ParseExact(txtShippedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    Total = decimal.Parse(txtTotal.Text),
                    OrderStatus = txtStatus.Text
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Order Detail");
            }
            return order;
        }

        private OrderDetailViewModel GetOrderDetailViewModel()
        {
            OrderDetailViewModel orderDetail = null;
            try
            {
                OrderDetail selectedOrderDetail = lvOrderDetails.SelectedItem as OrderDetail;
                orderDetail = new OrderDetailViewModel
                {
                    OrderId = selectedOrderDetail.OrderId,
                    FlowerBouquetId = selectedOrderDetail.FlowerBouquetId,
                    Quantity = selectedOrderDetail.Quantity,
                    UnitPrice = selectedOrderDetail.UnitPrice,
                    Discount = selectedOrderDetail.Discount
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Order Detail View Model");
            }
            return orderDetail;
        }
    }
}
