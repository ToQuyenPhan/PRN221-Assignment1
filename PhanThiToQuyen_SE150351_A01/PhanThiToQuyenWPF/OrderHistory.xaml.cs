using DataAccess.Repositories;
using System.Windows;

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
