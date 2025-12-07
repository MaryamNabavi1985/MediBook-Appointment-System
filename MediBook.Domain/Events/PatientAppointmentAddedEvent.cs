using MediBook.Domain.Common;
using MediBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Events
{
    public sealed record PatientAppointmentAddedEvent(Guid PatientId, Guid AppointmentId) :IDomainEvent;
    }
