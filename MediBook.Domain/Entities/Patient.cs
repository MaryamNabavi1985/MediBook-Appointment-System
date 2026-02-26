using MediBook.Domain.Common;
using MediBook.Domain.Events;
using MediBook.Domain.Exceptions;
using MediBook.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Entities
{
    public class Patient :Entity<Guid> , IAggregateRoot
    {
        public FullName Name { get; private set; } = null!;
        public EmailAddress Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public DateOfBirth DateOfBirth { get; private set; } = null!;
        public Address? Address { get; private set; }

        private readonly List<Appointment> _appointments = new();
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

        private Patient() { }

        public static Patient Create (FullName name,
        EmailAddress email,
        PhoneNumber phoneNumber,
        DateOfBirth dateOfBirth,
        DateTime now,
        Address? address = null
        )
         
        {
          

            var patient = new Patient
            {
                Name = name ?? throw new ArgumentNullException(nameof(name)),
                Email = email ?? throw new ArgumentNullException(nameof(email)),
                PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber)),
                DateOfBirth = dateOfBirth ?? throw new ArgumentNullException(nameof(dateOfBirth)),
                Address = address
            };
            patient.SetCreatedDate(now);
            patient.AddDomainEvent(new PatientCreatedEvent(patient));
            return patient;
        }

       public int CalculateAge(DateTime today)
        {
            return DateOfBirth.CalculateAge(today);
        }

        public void UpdateAddres(Address newAddress, DateTime now)
        {
            Address =  newAddress ?? throw new ArgumentNullException(nameof(newAddress));
            SetUpdateDate(now);
        }

        public void AddApointment(Appointment appointment,DateTime now)
        {
            if (appointment == null) throw new ArgumentNullException(nameof(appointment));
            if (appointment.PatientId != Id) throw new DomainException("Appointment does not belong to this patient");
            if (_appointments.Any(a => a.StartTime == appointment.StartTime))
                throw new DomainException("Patient already has an appointment at this time");

            _appointments.Add(appointment);
            SetUpdateDate(now);
            AddDomainEvent(new PatientAppointmentAddedEvent(this.Id, appointment.Id));
             
        }

        public void RemoveAppointment(Guid appointmentId, DateTime now)
        {
            var appointment = _appointments.FirstOrDefault(a => a.Id == appointmentId)
              ?? throw new DomainException("Appointment not found");
            if (appointment.StartTime < now.AddHours(24))
                throw new DomainException("Cannot cancel appointment less than 24 hours before");

            _appointments.Remove(appointment);
            SetUpdateDate(now);
        }
    }
}
