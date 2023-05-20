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
    /// Interaction logic for OrderReport.xaml
    /// </summary>
    public partial class OrderReport : Window
    {
        private readonly IOrderRepository orderRepository  = new OrderRepository();

        public OrderReport()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lvOrders.ItemsSource = orderRepository.ReportOrders(DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), 
                    DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Order Report");
            }
        }
    }
}
