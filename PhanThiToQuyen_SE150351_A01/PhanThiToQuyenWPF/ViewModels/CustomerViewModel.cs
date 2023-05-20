using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PhanThiToQuyenWPF.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
