using FluentAssertions;
using Xunit;
using MediBook.Domain.Entities;
using MediBook.Domain.ValueObjects;
using MediBook.Domain.Events;
using MediBook.Domain.Common;
using MediBook.Domain.Tests.Common;

namespace MediBook.Domain.Tests.Appointment
{
    public class AppointmentTests
    {
        [Fact]
        public void CreateAppointment_ShouldRaiseAppointmentCreatedEvent()
        {
                
             //arange
                var patientId = Guid.NewGuid();
                var doctorId = Guid.NewGuid();
                var startTime = DateTime.UtcNow.AddHours(1);
                var endTime = startTime.AddMinutes(30);
                var timeSlotId = Guid.NewGuid();
                string patientNote = string.Empty;
                var clock = new FixedClock(DateTime.UtcNow);
            //act

            var appointment = MediBook.Domain.Entities.Appointment.Create(
                patientId, doctorId,
                timeSlotId, 
                startTime, endTime, clock, 
                patientNote);

           //assert
                appointment.DomainEvents.Should().ContainSingle(e => e is AppointmentCreatedEvent);


        }
    }
}
