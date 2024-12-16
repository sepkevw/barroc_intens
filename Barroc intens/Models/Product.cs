using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models
{
    internal class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductNumber { get; set; }
        public int UnitsInStock { get; set; }   
        public double LeaseCost { get; set; }
        public double InstallCost { get; set; }
        public double PricePerKilo { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
