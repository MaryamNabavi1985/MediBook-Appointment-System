using MediBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Events
{
    public sealed record AppointmentConfirmedEvent(Entities.Appointment Appointment) : IDomainEvent;
 
}
