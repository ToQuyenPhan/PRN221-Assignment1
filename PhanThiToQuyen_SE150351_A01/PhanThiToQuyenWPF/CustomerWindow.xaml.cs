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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private int CustomerId;

        public CustomerWindow(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(CustomerId);
            profile.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e) {
            OrderHistory orderHistory = new OrderHistory(CustomerId);
            orderHistory.Show();
        }
    }
}
