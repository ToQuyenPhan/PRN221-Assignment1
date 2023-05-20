using BusinessObject.Models;
using DataAccess.Repositories;
using PhanThiToQuyenWPF.ViewModels;
using System;
using System.Windows;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for ManageCustomers.xaml
    /// </summary>
    public partial class ManageCustomers : Window
    {
        private readonly ICustomerRepository customerRepository = new CustomerRepository();
        private readonly IGenericRepository<Customer> genericRepository = new GenericRepository<Customer>();

        public ManageCustomers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => LoadCustomers();

        public void LoadCustomers()
        {
            lvCustomers.ItemsSource = genericRepository.GetAll();
            message.Text = "";
            txtSearch.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(txtId.Text.Length > 0)
            {
                try
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure to delete this customer?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        genericRepository.Delete(Int32.Parse(txtId.Text));
                        message.Text = "Delete successfully!";
                        LoadCustomers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Customer");
                }
            }
            else
            {
                message.Text = "Can not find the id!";
            }
        }

        private CustomerViewModel GetCustomerDetail()
        {
            CustomerViewModel customer = null;
            try
            {
                customer = new CustomerViewModel
                {
                    CustomerId = int.Parse(txtId.Text),
                    Email = txtEmail.Text,
                    CustomerName = txtName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                    Birthday = DateTime.Parse(txtBirthday.Text)
                };
            }catch(Exception e)
            {
                MessageBox.Show(e.Message, "Get Customer Detail");
            }
            return customer;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Length > 0)
            {
                CustomerDetails customerDetails = new CustomerDetails(genericRepository, true, GetCustomerDetail(), "Update a customer");
                customerDetails.Show();
                LoadCustomers();
            }
            else
            {
                message.Text = "Please choose a customer to update!";
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
             CustomerDetails customerDetails = new CustomerDetails(genericRepository, false, null, "Insert a customer");
             customerDetails.Show();
             LoadCustomers();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadCustomers();

        private void btnSearch_Click(object sender, RoutedEventArgs e) {
            if (txtSearch.Text.Length > 0) {
                lvCustomers.ItemsSource = customerRepository.SearchByName(txtSearch.Text);
            }
        }
    }
}
