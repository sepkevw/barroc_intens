using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int ContactPersonId { get; set; }

        public CustomerContactPerson ContactPerson { get; set; }
    }
}
