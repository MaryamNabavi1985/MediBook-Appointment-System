using MediBook.Domain.Entities;
using MediBook.Domain.Exceptions;
using MediBook.Domain.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Tests.Entities
{
    public class TimeSlotTests
    {
        [Fact]
        public void Create_Should_Create_TimeSlot_When_Data_Is_Valid()
        {
            var clock = new FixedClock(new DateTime(2025, 1, 1, 10, 0, 0));
            var doctorId = Guid.NewGuid();
            var start = clock.Now.AddDays(1);
            var end = clock.Now.AddDays(30);

            //act
            var slot = TimeSlot.Create(doctorId, start, end, clock);

            //assert
            Assert.Equal(doctorId, slot.DoctorId);
            Assert.Equal(start, slot.StartTime);
            Assert.Equal(end, slot.EndTime);
            Assert.False(slot.IsBooked);
            Assert.Null(slot.AppointmentId);

        }

        [Fact]
        public void Create_Should_Throw_Error_When_Start_After_End()
        {
            var clock = new FixedClock(DateTime.UtcNow);
            var doctorId = Guid.NewGuid();
            var start = clock.Now.AddDays(1);
            var end = start;

            var act = () => TimeSlot.Create(doctorId, start, end, clock);
            Assert.Throws<DomainException>(act);

        }
        [Fact]
        public void Create_should_Throw_Error_When_Duration_Less_Than_15_Minutes()
        {
            var clock = new FixedClock(DateTime.UtcNow);
            var doctorId = Guid.NewGuid();
            var start = clock.Now.AddDays(1);
            var end = start.AddMinutes(14);

            var act = () => TimeSlot.Create(doctorId, start, end, clock);

            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Create_Should_Throw_Error_When_More_Than_6_Months_In_Future()
        {
            var clock = new FixedClock(DateTime.UtcNow);
            var doctorId = Guid.NewGuid();
            var start = clock.Now.AddMonths(7);
            var end = start.AddMinutes(30);

            var act = () => TimeSlot.Create(doctorId, start, end, clock);

            Assert.Throws<DomainException>(act);

        }


        [Fact]
        public void Book_should_Book_TimeSlot_When_Data_Is_Valid()
        {
            var clock = new FixedClock(new DateTime(2025, 1, 1, 10, 0, 0));
            var slot = TimeSlot.Create(Guid.NewGuid(), clock.Now.AddDays(1), clock.Now.AddDays(30), clock);

            var appointmentId = Guid.NewGuid();

            slot.Book(appointmentId, clock);

            Assert.True(slot.IsBooked);
            Assert.Equal(appointmentId, slot.AppointmentId);
        }
        [Fact]
        public void Book_Should_Throw_Error_When_Slot_Booked()
        {
            var clock = new FixedClock(DateTime.Now);
            var slot = TimeSlot.Create(Guid.NewGuid(), clock.Now.AddDays(1), clock.Now.AddDays(30), clock);
            slot.Book(Guid.NewGuid(), clock);

            var act = () => slot.Book(Guid.NewGuid(), clock);

            Assert.Throws<DomainException>(act);

        }
        [Fact]
        public void Book_Should_Throw_Error_When_Start_Less_Than_1_Hour_To_Start()
        {
            var clock = new FixedClock(DateTime.Now);
            var start = clock.Now.AddMinutes(30);
            var slot = TimeSlot.Create(Guid.NewGuid(), start, start.AddHours(1), clock);

            var act = () => slot.Book(Guid.NewGuid(), clock);

            Assert.Throws<DomainException>(act);

        }
        [Fact]
        public void Cancel_Should_Cancel_Booking_When_Data_Is_Valid()
        {
            var clock = new FixedClock(new DateTime(2025, 1, 1, 10, 0, 0));
            var slot = TimeSlot.Create(Guid.NewGuid(), clock.Now.AddDays(1), clock.Now.AddDays(30), clock);
            var appointmentId = Guid.NewGuid();
            slot.Book(appointmentId, clock);
            slot.Cancel(clock);
            Assert.False(slot.IsBooked);
            Assert.Null(slot.AppointmentId);

        }
        [Fact]
        public void Cancel_Should_Throw_Error_When_Not_Booked()
        {
            var clock = new FixedClock(DateTime.Now);
            var slot = TimeSlot.Create(Guid.NewGuid(), clock.Now.AddDays(1), clock.Now.AddDays(30), clock);
            var act = () => slot.Cancel(clock);
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Cancel_Should_Throw_Error_When_Start_Less_Than_2_Hours_To_Start()
        {
            var clock = new FixedClock(DateTime.Now);
            var start = clock.Now.AddHours(1);
            var slot = TimeSlot.Create(Guid.NewGuid(), start, start.AddHours(1), clock);
            slot.Book(Guid.NewGuid(), clock);
            var act = () => slot.Cancel(clock);
            Assert.Throws<DomainException>(act);
        }
    }
}
