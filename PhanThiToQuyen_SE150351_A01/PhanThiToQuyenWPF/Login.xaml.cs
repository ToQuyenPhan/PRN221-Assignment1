using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Windows;

namespace PhanThiToQuyenWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ICustomerRepository customerRepository = new CustomerRepository();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                errorMessage.Text = "Enter an email!";
                txtEmail.Focus();
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorMessage.Text = "Enter a valid email!";
                txtEmail.Select(0, txtEmail.Text.Length);
                txtEmail.Focus();
            }else if(txtPassword.Password.Length == 0)
            {
                errorMessage.Text = "Enter a password!";
                txtPassword.Focus();
            }
            else
            {
                var email = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("DefaultAccount:Email").Value;
                var password = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("DefaultAccount:Password").Value;
                var customer = customerRepository.Login(txtEmail.Text, txtPassword.Password);
                if(txtEmail.Text.Equals(email) && txtPassword.Password.Equals(password))
                {
                    errorMessage.Text = "Admin login successfully!";
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else if(customer != null)
                {
                    errorMessage.Text = "Login successfully!";
                    CustomerWindow customerWindow = new CustomerWindow(customer.CustomerId);
                    customerWindow.Show();
                    this.Close();
                }
                else
                {
                    errorMessage.Text = "Login fail!";
                }
            }
        }
    }
}
