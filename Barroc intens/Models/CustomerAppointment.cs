using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barroc_intens.Models;

internal class CustomerAppointment
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }

    public DateTime CreatedAt { get; set; }
}