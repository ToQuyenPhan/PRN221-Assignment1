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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, RoutedEventArgs e)
        {
            ManageCustomers manageCustomers = new ManageCustomers();
            manageCustomers.Show();
        }

        private void btnFlower_Click(object sender, RoutedEventArgs e)
        {
            ManageFlowerBouquet manageFlowerBouquet = new ManageFlowerBouquet();
            manageFlowerBouquet.Show();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            ManageOrder manageOrder = new ManageOrder();
            manageOrder.Show();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            OrderReport orderReport = new OrderReport();
            orderReport.Show();
        }
    }
}
