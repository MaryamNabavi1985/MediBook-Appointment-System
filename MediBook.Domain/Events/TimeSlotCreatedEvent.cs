using MediBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Events
{
    public sealed record TimeSlotCreatedEvent(Guid Id, Guid DoctorId) : IDomainEvent;
    
}
