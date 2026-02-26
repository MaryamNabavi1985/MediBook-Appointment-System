using FluentAssertions;
using MediBook.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MediBook.Domain.Tests.ValueObjects
{
    public class DateOfBirthTests
    {
        [Fact]
        public void CalculateAge_WhenBirthdayIsToday_ShouldReturnCorrectAge()
        {
            //arrange
            var birthDate = new DateOfBirth(new DateTime(1985, 07, 17));
            var today = new DateTime(2025, 08, 7);

            //act 
            var age = birthDate.CalculateAge(today);

            //assert
            age.Should().Be(40);

        }

        [Fact]
        public void CalculateAge_WhenBirthdayeHasnotOccurredYet_ShouldSubtractOneYear()
        {
            //Arrange
            var dob =  new DateOfBirth(new DateTime(1985,9 ,29));
            var today = new DateTime(2025, 9, 28);

            //Act
            var age = dob.CalculateAge(today);

            age.Should().Be(39);
            
        }
        [Fact]
        public void CalculateAge_WhenBirthdayHasPassed_ShouldReturnFullAge()
        {
            //Arrange
            var dob = new DateOfBirth(new DateTime(1985, 9, 29));
            var today = new DateTime(2025, 9, 30);

            //Act
            var age = dob.CalculateAge(today);

            age.Should().Be(40);

        }
        [Fact]
        public void Constructor_WhenBirthdayIsInFuture_ShouldReturnException()
        {
            //Arrange
            var futureDate = DateTime.Today.AddDays(1);
            //Act
            Action act =  () => new DateOfBirth(futureDate);

            act.Should().Throw<ArgumentException>()
                .WithMessage("*future*")
                ;
        }
        [Fact]
        public void Constructor_WhenDateIsOld_ShouldThrowException() {

            //Arrange
            var tooOldDate = DateTime.Now.AddYears(-131);

            //Action
             Action act = () => new DateOfBirth(tooOldDate);

            //Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Invalid*");
        
        }
        [Fact]
        public void CalculateAge_ForLeapYearBirthday_ShouldWorkCorrectly()
        {
            //Arrange
            var dob =  new DateOfBirth(new DateTime(2000, 2, 29));
            var today = new DateTime(2025, 2, 28);

            //Act
            var age = dob.CalculateAge(today);

            //Assert
            age.Should().Be(24);

        }

    }
}
