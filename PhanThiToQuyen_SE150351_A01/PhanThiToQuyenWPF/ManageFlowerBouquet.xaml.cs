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
    /// Interaction logic for ManageFlowerBouquet.xaml
    /// </summary>
    public partial class ManageFlowerBouquet : Window
    {
        private readonly IGenericRepository<FlowerBouquet> genericRepository = new GenericRepository<FlowerBouquet>();
        private readonly IFlowerBouquetRepository flowerBouquetRepository = new FlowerBouquetRepository();
        private int CategoryId { get; set; }
        private int? SupplierId { get; set; }

        public ManageFlowerBouquet()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => LoadFlowerBouquets();

        private void LoadFlowerBouquets()
        {
            lvFlowerBouquets.ItemsSource = genericRepository.GetAll();
            txtCategoryName.Clear();
            txtSupplierName.Clear();
            message.Text = "";
            txtSearch.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                try
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to delete this flower bouquet?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        IGenericRepository<OrderDetail> orderDetailRepository = new GenericRepository<OrderDetail>();
                        var orderDetails = orderDetailRepository.GetAll();
                        var check = false;
                        foreach ( var orderDetail in orderDetails )
                        {
                            if(orderDetail.FlowerBouquetId == Int32.Parse(txtId.Text))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check == false)
                        {
                            genericRepository.Delete(Int32.Parse(txtId.Text));
                            message.Text = "Delete successfully!";
                        }
                        else
                        {
                            var flowerBouquet = new FlowerBouquet
                            {
                                FlowerBouquetId = Int32.Parse(txtId.Text),
                                FlowerBouquetName = txtName.Text,
                                Description = txtDescription.Text,
                                UnitPrice = decimal.Parse(txtPrice.Text),
                                UnitsInStock = Int32.Parse(txtUis.Text),
                                CategoryId = CategoryId,
                                SupplierId = SupplierId,
                                FlowerBouquetStatus = 0
                            };
                            genericRepository.Update(flowerBouquet);
                            message.Text = "This flower bouquet is stored in an order!";
                        }
                        LoadFlowerBouquets();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Flower Bouquet");
                }
            }
            else
            {
                message.Text = "Can not find the id!";
            }
        }

        private FlowerBouquetViewModel GetFlowerBouquetDetail()
        {
            FlowerBouquetViewModel flowerBouquet = null;
            try
            {
                flowerBouquet = new FlowerBouquetViewModel
                {
                    FlowerBouquetId = Int32.Parse(txtId.Text),
                    FlowerBouquetName = txtName.Text,
                    Description = txtDescription.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitsInStock = Int32.Parse(txtUis.Text),
                    CategoryId = CategoryId,
                    SupplierId = SupplierId,
                    FlowerBouquetStatus = byte.Parse(txtStatus.Text)
                };
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Get Flower Bouquet Detail");
            }
            return flowerBouquet;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                FlowerBouquetDetails flowerBouquetDetails = new FlowerBouquetDetails(genericRepository, true, GetFlowerBouquetDetail(), "Update a flower bouquet");
                flowerBouquetDetails.Show();
                LoadFlowerBouquets();
            }
            else
            {
                message.Text = "Please choose a flower bouquet to update!";
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            FlowerBouquetDetails flowerBouquetDetails = new FlowerBouquetDetails(genericRepository, false, null, "Insert a flower bouquet");
            flowerBouquetDetails.Show();
            LoadFlowerBouquets();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadFlowerBouquets();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                lvFlowerBouquets.ItemsSource = flowerBouquetRepository.SearchByName(txtSearch.Text);
            }
        }

        private void lvFlowerBouquets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FlowerBouquet flowerBouquet = (FlowerBouquet)lvFlowerBouquets.SelectedItem;
            if(flowerBouquet != null) {
                IGenericRepository<Category> categoryRepository = new GenericRepository<Category>();
                var category = categoryRepository.GetById(flowerBouquet.CategoryId);
                CategoryId = flowerBouquet.CategoryId;
                IGenericRepository<Supplier> supplierRepository = new GenericRepository<Supplier>();
                var supplier = supplierRepository.GetById(flowerBouquet.SupplierId);
                SupplierId = flowerBouquet.SupplierId;
                txtSupplierName.Text = supplier.SupplierName;
                txtCategoryName.Text = category.CategoryName;
            }
        }
    }
}
