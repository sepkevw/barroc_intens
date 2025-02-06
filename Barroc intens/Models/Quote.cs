using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models;

internal class Quote
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ContactPersonId { get; set; }
    public int PackageId { get; set; }
    public bool Signed { get; set; }

    // Foreign models/keys
    public Customer Customer { get; set; }
    public CustomerContactPerson ContactPerson { get; set; }
    public Package Package { get; set; }
}