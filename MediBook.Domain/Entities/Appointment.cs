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
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed,
        NotShow
    }
    public class Appointment : Entity<Guid>
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public Guid TimeSlotId { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public AppointmentStatus Status { get; private set; } = AppointmentStatus.Pending;
        public string? Notes { get; private set; } = null;

        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        private Appointment() { }
        public static Appointment Create (
            Guid patientId,
            Guid doctorId,
            Guid timeSlotId,
            DateTime startTime,
            DateTime endTime,
            IClock clock,
            string? patientNotes = null
            )
          {
            if (patientId == Guid.Empty) throw new DomainException("Patient is required");
            if (doctorId == Guid.Empty) throw new DomainException("Doctor is required");
            if (startTime < DateTime.UtcNow) throw new DomainException("Cannot create appointment in the past");
            if (endTime <= startTime) throw new DomainException("End time must be after start time");

            var appointment = new Appointment()
            {

                PatientId = patientId,
                DoctorId = doctorId,
                TimeSlotId = timeSlotId,
                StartTime = startTime.ToUniversalTime(),
                EndTime = endTime.ToUniversalTime(),
                Notes = patientNotes?.Trim(),
                CreatedAt = DateTime.UtcNow,
                Status = AppointmentStatus.Pending

            };
            appointment.AddDomainEvent(new DoctorAppointmentBookedEvent(doctorId, appointment.Id, patientId));
            appointment.AddDomainEvent(new AppointmentCreatedEvent(appointment.Id));
            appointment.AddDomainEvent(new PatientAppointmentAddedEvent(patientId, appointment.Id));

            appointment.SetCreatedDate(clock.Now);

            return appointment;
 
        }
        public void Confirm(IClock clock)
        {
            if (Status != AppointmentStatus.Pending)
                throw new DomainException("Only pending appointments can be confirmed");

            Status = AppointmentStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new AppointmentConfirmedEvent(this));
        }
        public void Cancel(IClock clock)
        {
            if (Status is AppointmentStatus.Cancelled or AppointmentStatus.Completed)
                throw new DomainException("Cannot cancel an already processed appointment");

            if (StartTime < clock.Now.AddHours(24))
                throw new DomainException("Cancellation not allowed less than 24 hours before appointment");

            Status = AppointmentStatus.Cancelled;
            UpdatedAt = clock.Now;
            AddDomainEvent(new AppointmentCancelEvent(this.Id));
        }
        public void Complete()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new DomainException("Only confirmed appointments can be completed");

            Status = AppointmentStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new AppointmentCompletedEvent(this));
        }
    }
}
