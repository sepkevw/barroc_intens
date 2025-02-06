using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models;

internal class Invoice
{
    public int Id { get; set; }

    // public DateOnly Period { get; set; }
    // public string CompanyName { get; set; }
    // public string Address { get; set; }
    public int CustomerId { get; set; }
    public int QuoteId { get; set; }
    public int PackageId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime SendAt { get; set; }

    // Foreign key assigning
    public Customer Customer { get; set; }
    public Quote Quote { get; set; }
    public Package package { get; set; }
}