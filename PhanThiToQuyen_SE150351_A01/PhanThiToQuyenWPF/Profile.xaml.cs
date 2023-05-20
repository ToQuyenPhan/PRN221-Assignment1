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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        private readonly IGenericRepository<Customer> genericRepository = new GenericRepository<Customer>();
        private int CustomerId;
        public Profile(int customerId)
        {
            InitializeComponent();
            CustomerId = customerId;
        }

        private void ProfileWindow_Load(object sender, RoutedEventArgs e) => LoadCustomer();

        private void LoadCustomer()
        {
            var customer = genericRepository.GetById(CustomerId);
            txtId.Text = customer.CustomerId.ToString();
            txtEmail.Text = customer.Email.ToString();
            txtName.Text = customer.CustomerName.ToString();
            txtCity.Text = customer.City.ToString();
            txtCountry.Text = customer.Country.ToString();
            txtPassword.Text = customer.Password.ToString();
            txtBirthday.Text = customer.Birthday.ToString();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e) => Close();

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newProfile = new CustomerViewModel
                {
                    CustomerId = int.Parse(txtId.Text),
                    Email = txtEmail.Text,
                    CustomerName = txtName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text,
                    Birthday = DateTime.Parse(txtBirthday.Text)
                };
                UpdateProfile updateProfile = new UpdateProfile(newProfile);
                updateProfile.Show();
                LoadCustomer();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Profile");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadCustomer();
    }
}
