using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models;

internal class PackageProduct
{
    public int Id { get; set; }
    public int PackageId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
    public Package Package { get; set; }
    public Product product { get; set; }
}