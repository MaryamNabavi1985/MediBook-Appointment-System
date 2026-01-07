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
    public class Doctor : Entity<Guid>, IAggregateRoot
    {
        public FullName FullName { get; private set; } = null!;
        public string Specialty { get; private set; } = null!;
        public EmailAddress Email { get; private set; } = null!;
        public PhoneNumber PhoneNumber { get; private set; } = null!;
        public string City { get; private set; } = "Berlin";

        private readonly List<TimeSlot> _availableTimeSlots = new();
        
          

        private Doctor() {}

        public static Doctor Create(
            FullName name,
            string specialty,
            EmailAddress email,
            PhoneNumber phoneNumber,
            DateTime now,
            string city = "Berlin"
          )
        {
          
            if (string.IsNullOrWhiteSpace(specialty))
                throw new ArgumentException("Specialty is required");

            var doctor = new Doctor
            {
                FullName = name ?? throw new ArgumentNullException(nameof(name)),
                Specialty = specialty.Trim(),
                Email = email ?? throw new ArgumentNullException(nameof(email)),
                PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber)),
                City = city.Trim() ?? throw new ArgumentNullException(nameof(city)),
            };

            doctor.SetCreatedDate(now);
            doctor.AddDomainEvent(new DoctorCreatedEvent(doctor));
            return doctor;
        }

        public void AddTimeSlot(TimeSlot timeSlot , DateTime now)
        {
            if (timeSlot == null) throw new ArgumentNullException(nameof(timeSlot));
            if (timeSlot.DoctorId != Id) throw new DomainException("TimeSlot does not belong to this doctor");

             if (_availableTimeSlots.Any(ts => ts.StartTime == timeSlot.StartTime))
                throw new DomainException("Doctor already has a time slot at this time");

            _availableTimeSlots.Add(timeSlot);
            SetUpdateDate(now);
            AddDomainEvent(new DoctorTimeSlotAddedEvent(this, timeSlot));
        }




    }
}
