using FluentAssertions;
using MediBook.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Tests.ValueObjects
{
    public class FullNameTests
    {
        [Fact]
        public void Constructor_ValidNames_ShouldCreateFullName()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            // Act
            var fullName = new FullName(firstName, lastName);
            // Assert
            Assert.Equal("John", fullName.FirstName);
            Assert.Equal("Doe", fullName.LastName);
            Assert.Equal("John Doe", fullName.DisplayName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_Should_Throw_Exception_When_FirstName_Is_Invalid(string firstName)
        {
            //Act
            Action act = () => new FullName(firstName, "Navabi");

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*First name is required*");

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Constructor_Should_Throw_Exception_When_LastName_Is_Invalid(string lastName)
        {
            //Act
            Action act = () => new FullName("Maryam", lastName);

            //Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*Last name is required*");
        }

        [Fact]
        public void FullName_Should_Trim_FirstName_And_LastName()
        {
            //Act
            var fullname = new FullName(" Maryam ", " Nabavi ");

            //Assert
            fullname.FirstName.Should().Be("Maryam");
            fullname.LastName.Should().Be("Nabavi");
            
        }

        [Fact]
        public void FullName_With_Same_Values_Should_Be_Equal()
        {
            // Arrange
            var name1 = new FullName("Maryam", "Navabi");
            var name2 = new FullName("Maryam", "Navabi");

            // Assert
            name1.Should().Be(name2);
            (name1 == name2).Should().BeTrue();
        }

        [Fact]
        public void FullName_With_Different_Values_Should_Not_Be_Equal()
        {
            // Arrange
            var name1 = new FullName("Maryam", "Navabi");
            var name2 = new FullName("Maryam", "Ahmadi");

            // Assert
            name1.Should().NotBe(name2);
        }
        [Fact]
        public void DisplayName_Should_Return_Formatted_FullName()
        {
            // Act
            var fullName = new FullName("Maryam", "Navabi");

            // Assert
            fullName.DisplayName.Should().Be("Maryam Navabi");
        }

        [Fact]
        public void ToString_Should_Return_DisplayName()
        {
            // Act
            var fullName = new FullName("Maryam", "Navabi");

            // Assert
            fullName.ToString().Should().Be("Maryam Navabi");
        }

    }
}
