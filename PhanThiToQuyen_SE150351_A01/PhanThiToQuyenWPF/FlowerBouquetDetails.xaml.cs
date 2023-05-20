using BusinessObject.Models;
using DataAccess.Repositories;
using PhanThiToQuyenWPF.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for FlowerBouquetDetails.xaml
    /// </summary>
    public partial class FlowerBouquetDetails : Window
    {
        private IGenericRepository<FlowerBouquet> FlowerBouquetRepository;
        private bool InsertOrUpdate;
        private FlowerBouquetViewModel FlowerBouquetDetail;

        public FlowerBouquetDetails(IGenericRepository<FlowerBouquet> repository, bool insertOrUpdate, FlowerBouquetViewModel flowerBouquetDetail, string title)
        {
            InitializeComponent();
            FlowerBouquetRepository = repository;
            InsertOrUpdate = insertOrUpdate;
            FlowerBouquetDetail = flowerBouquetDetail;
            Title = title;
        }

        private void FlowerBouquetDetailsWindow_Load(object sender, RoutedEventArgs e) {
            txtId.IsReadOnly = InsertOrUpdate;
            IGenericRepository<Category> categoryRepository = new GenericRepository<Category>();
            IGenericRepository<Supplier> supplierRepository = new GenericRepository<Supplier>();
            var categories = categoryRepository.GetAll();
            var suppliers = supplierRepository.GetAll();
            if (InsertOrUpdate == true)
            {
                txtId.Text = FlowerBouquetDetail.FlowerBouquetId.ToString();
                txtName.Text = FlowerBouquetDetail.FlowerBouquetName.ToString();
                txtDescription.Text = FlowerBouquetDetail.Description.ToString();
                txtPrice.Text = FlowerBouquetDetail.UnitPrice.ToString();
                txtUis.Text = FlowerBouquetDetail.UnitsInStock.ToString();
                txtStatus.Text = FlowerBouquetDetail.FlowerBouquetStatus.ToString();
                var  selectedCategory = categoryRepository.GetById(FlowerBouquetDetail.CategoryId);
                var selectedSupplier = supplierRepository.GetById(FlowerBouquetDetail.SupplierId);
                foreach(Category category in categories)
                {
                    ComboBoxItem categoryItem = new ComboBoxItem();
                    categoryItem.Content = category.CategoryName;
                    categoryItem.Tag = category.CategoryId;
                    cbCategory.Items.Add(categoryItem);
                    if (categoryItem.Content.Equals(selectedCategory.CategoryName))
                    {
                        cbCategory.SelectedItem = categoryItem;
                    }
                }
                foreach(Supplier supplier in suppliers)
                {
                    ComboBoxItem supplierItem = new ComboBoxItem();
                    supplierItem.Content = supplier.SupplierName;
                    supplierItem.Tag = supplier.SupplierId;
                    cbSupplier.Items.Add(supplierItem);
                    if (supplierItem.Content.Equals(selectedSupplier.SupplierName))
                    {
                        cbSupplier.SelectedItem = supplierItem;
                    }
                }
            }
            else
            {
                foreach (Category category in categories)
                {
                    ComboBoxItem categoryItem = new ComboBoxItem();
                    categoryItem.Content = category.CategoryName;
                    categoryItem.Tag = category.CategoryId;
                    cbCategory.Items.Add(categoryItem);
                }
                foreach (Supplier supplier in suppliers)
                {
                    ComboBoxItem supplierItem = new ComboBoxItem();
                    supplierItem.Content = supplier.SupplierName;
                    supplierItem.Tag = supplier.SupplierId;
                    cbSupplier.Items.Add(supplierItem);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => Close();

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var flowerBouquet = new FlowerBouquet
                {
                    FlowerBouquetId = Int32.Parse(txtId.Text),
                    FlowerBouquetName = txtName.Text,
                    Description = txtDescription.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitsInStock = Int32.Parse(txtUis.Text),
                    CategoryId = Convert.ToInt32(cbCategory.SelectedValue),
                    SupplierId = Convert.ToInt32(cbSupplier.SelectedValue),
                    FlowerBouquetStatus = byte.Parse(txtStatus.Text)
                };
                if (InsertOrUpdate == false)
                {
                    FlowerBouquetRepository.Insert(flowerBouquet);
                }
                else
                {
                    FlowerBouquetRepository.Update(flowerBouquet);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a flower bouquet" : "Update a flower bouquet");
            }
        }
    }
}
