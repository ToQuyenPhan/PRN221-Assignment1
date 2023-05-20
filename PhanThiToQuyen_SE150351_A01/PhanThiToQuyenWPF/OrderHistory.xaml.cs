using DataAccess.Repositories;
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
    /// Interaction logic for OrderHistory.xaml
    /// </summary>
    public partial class OrderHistory : Window
    {
        private readonly OrderRepository orderRepository = new OrderRepository();
        private int CustomerId;

        public OrderHistory(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void OrderHistory_Load(object sender, RoutedEventArgs e)
        {
            lvOrders.ItemsSource = orderRepository.SearchByCustomerID(CustomerId);
        }
    }
}
