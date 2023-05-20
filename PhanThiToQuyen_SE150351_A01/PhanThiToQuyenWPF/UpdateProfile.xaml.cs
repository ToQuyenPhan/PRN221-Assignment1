using BusinessObject.Models;
using DataAccess.Repositories;
using PhanThiToQuyenWPF.ViewModels;
using System;
using System.Windows;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Window
    {
        private readonly IGenericRepository<Customer> genericRepository = new GenericRepository<Customer>();
        private CustomerViewModel Customer;

        public UpdateProfile(CustomerViewModel customer)
        {
            InitializeComponent();
            Customer = customer;
        }

        private void UpdateProfile_Load(object sender, RoutedEventArgs e)
        {
            txtId.Text = Customer.CustomerId.ToString();
            txtEmail.Text = Customer.Email.ToString();
            txtName.Text = Customer.CustomerName.ToString();
            txtCity.Text = Customer.City.ToString();
            txtCountry.Text = Customer.Country.ToString();
            txtPassword.Text = Customer.Password.ToString();
            txtBirthday.Text = Customer.Birthday.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProfile = new Customer
                {
                    CustomerId = int.Parse(txtId.Text),
                    Email = txtEmail.Text,
                    CustomerName = txtName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                    Birthday = DateTime.Parse(txtBirthday.Text)
                };
                genericRepository.Update(newProfile);
                this.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Profile");
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();
    }
}
