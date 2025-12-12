using MediBook.Domain.Common;
using MediBook.Domain.Events;
using MediBook.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Entities
{
    public class TimeSlot : Entity<Guid> , IAggregateRoot
    {
        public Guid DoctorId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public bool IsBooked { get; private set; }
        public Guid? AppointmentId { get; private set; }

        private TimeSlot() {}
        public static TimeSlot Create (
             Guid doctorId,
             DateTime startTime,
             DateTime endTime,
             IClock clock)
        {
            if (startTime >= endTime) throw new DomainException("Start time must be before end time");
            if (endTime.Subtract(startTime) < TimeSpan.FromMinutes(15)) throw new DomainException("Minimum slot duration is 15 minutes");

            if (startTime > clock.Now.AddMonths(6)) throw new DomainException("Time slots cannot be created more than 6 months in advance");

            var slot = new TimeSlot
            {
                DoctorId = doctorId,
                StartTime = startTime,
                EndTime = endTime,
                IsBooked = false
            };

            slot.SetCreatedDate(clock.Now);
            slot.AddDomainEvent(new TimeSlotCreatedEvent(slot.Id, doctorId));
            return slot;
        }

        public void Book(Appointment appointment,IClock clock)
        {
            if (IsBooked) throw new DomainException("Time slot is already booked");
            if (StartTime < clock.Now.AddHours(1)) throw new DomainException("Cannot book less than 1 hour before start");
            Appointment = appointment ?? throw new ArgumentNullException(nameof(appointment));
            IsBooked = true;
            SetUpdateDate(clock.Now);
        }
        public void Cancel(IClock clock)
        {
            if (!IsBooked) throw new DomainException("Time slot is not booked");
            if (StartTime < clock.Now.AddHours(2)) throw new DomainException("Cannot cancel less than 2 hours before start");
            Appointment = null;
            IsBooked = false;
            SetUpdateDate(clock.Now);
        }


    }
}
