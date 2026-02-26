using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MediBook.Domain.ValueObjects;
namespace MediBook.Domain.Tests.ValueObjects
{
    public class AddressTest
    {
        [Fact]
        public void Should_Create_Address_With_Valid_Parameters()
        {
            //Arrange
            var street = "123 Main St";
            var city = "Berlin";
            var postalCode = "10115";
            var country = "Germany";
            //Act
            var address =new Address(street, city, postalCode, country);
            //Assert
            address.Street.Should().Be(street);
            address.City.Should().Be(city);
            address.PostalCode.Should().Be(postalCode);
            address.Country.Should().Be(country);
        }
        [Fact]
        public void Should_Trim_All_Parameters()
        {
             //Act
             var address = new Address(" 123 Main St ", " Berlin ", " 10115 ", " Germany ");
            //Assert
            address.Street.Should().Be("123 Main St");
            address.City.Should().Be("Berlin");
            address.PostalCode.Should().Be("10115");
            address.Country.Should().Be("Germany");
       }
        [Theory]
        [InlineData(null, "street")]
        [InlineData("", "street")]
        [InlineData("   ", "street")]
        [InlineData(null, "city")]
        [InlineData("", "city")]
        [InlineData("   ", "city")]
        [InlineData(null, "postalCode")]
        [InlineData("", "postalCode")]
        [InlineData("   ", "postalCode")]
        [InlineData(null, "country")]
        [InlineData("", "country")]
        [InlineData("   ", "country")]
        public void Should_Throw_ArgumentNullException_For_Null_Parameters(string value,string paramName)
        {
           // Act
           Action act= () => new Address(
               
               street: paramName == "street" ? value : "123 Main St",
                city: paramName == "city" ? value : "Berlin",
                postalCode: paramName == "postalCode" ? value : "10115",
                country: paramName == "country" ? value : "Germany"
                );


               if(value is null)
               {
                   act.Should().Throw<ArgumentNullException>()
                   .WithParameterName(paramName);
               }
               else
               {
                   act.Should().Throw<ArgumentException>()
                   .WithParameterName(paramName)
                   .WithMessage("Value cannot be empty or whitespace.*");
            }

        }


    }
}
