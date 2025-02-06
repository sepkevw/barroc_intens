using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models;

internal class Package
{
    public int Id { get; set; }
    public DateOnly Period { get; set; }
    public DateOnly PeriodEnd { get; set; }
}