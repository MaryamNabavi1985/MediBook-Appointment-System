using FluentAssertions;
using MediBook.Domain.Entities;
using MediBook.Domain.Events;
using MediBook.Domain.Tests.Common;

namespace MediBook.Domain.Tests.Entities
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
                var now = DateTime.Now;
            //act

            var appointment = Appointment.Create(
                patientId, doctorId,
                timeSlotId, 
                startTime, endTime, now, 
                patientNote);

           //assert
                appointment.DomainEvents.Should().ContainSingle(e => e is AppointmentCreatedEvent);


        }
    }
}
