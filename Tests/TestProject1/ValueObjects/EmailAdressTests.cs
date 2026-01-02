using FluentAssertions;
using MediBook.Domain.ValueObjects;

namespace MediBook.Domain.Tests.ValueObjects
{
    public class EmailAdressTests
    {
        [Fact]
        public void EmailAdress_Creation_Succeeds_For_Valid_Email()
        {
            //Arrange
            var email = "Test@Medibook.com";

            //Act
            var emailAddress = new EmailAddress(email);

            //Assert
            emailAddress.Email.Should().Be("test@medibook.com");

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void EmailAdress_Shoul_Throw_ArgumentNullException_For_NullOrEmpty(string invalidEmail)
        {
       
            Action act = () => new EmailAddress(invalidEmail);
           
            act.Should().Throw<ArgumentNullException>().WithParameterName("email");
        }
        [Theory]
        [InlineData("invalid")]
        [InlineData("invalid@")]
        [InlineData("@bc.com")]
        [InlineData("a@bc")]
        [InlineData("#%t@bc.com")]
        public void EmailAddress_Should_Throe_ArgumentException_For_InvalidFormat(string invalidEmail)
        {
            Action act = () => new EmailAddress(invalidEmail);
            act.Should().Throw<ArgumentException>().WithMessage("Invalid Email Format*");
        }
    }
}
