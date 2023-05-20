using BusinessObject.Models;
using DataAccess.Repositories;
using PhanThiToQuyenWPF.ViewModels;
using System;
using System.Windows;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        private IGenericRepository<Customer> CustomerRepository;
        private bool InsertOrUpdate;
        private CustomerViewModel CustomerDetail;

        public CustomerDetails(IGenericRepository<Customer> repository, bool insertOrUpdate, CustomerViewModel customerDetail, string title)
        {
            InitializeComponent();
            CustomerRepository = repository;
            InsertOrUpdate = insertOrUpdate;
            CustomerDetail = customerDetail;
            Title = title;
        }

        private void CustomerDetailsWindow_Load(object sender, RoutedEventArgs e)
        {
            txtId.IsReadOnly = InsertOrUpdate;
            if(InsertOrUpdate == true)
            {
                txtId.Text = CustomerDetail.CustomerId.ToString();
                txtEmail.Text = CustomerDetail.Email.ToString();
                txtName.Text = CustomerDetail.CustomerName.ToString();
                txtCity.Text = CustomerDetail.City.ToString();
                txtCountry.Text = CustomerDetail.Country.ToString();
                txtPassword.Text = CustomerDetail.Password.ToString();
                txtBirthday.Text = CustomerDetail.Birthday.ToString();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) => Close();

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerId = int.Parse(txtId.Text),
                    Email = txtEmail.Text,
                    CustomerName = txtName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                    Birthday = DateTime.Parse(txtBirthday.Text)
                };
                if(InsertOrUpdate == false)
                {
                    CustomerRepository.Insert(customer);
                }
                else
                {
                    CustomerRepository.Update(customer);
                }
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a customer" : "Update a customer");
            }
        }
    }
}
