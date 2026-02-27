using MediBook.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Tests.ValueObjects
{
    public class SpecialtyTest
    {
        [Fact]
        public void Constructor_should_ThrowException_When_NameIsNull()
        {
                    Assert.Throws<ArgumentNullException>(() => new Specialty(null));
        }


        [Fact]
        public void Constructor_Should_SetName_When_NameIsValid()
        {
            var name = "Cardiology";

            var act = new Specialty(name);

            Assert.Equal("Cardiology",act.SpecialtyName);

        }

        [Fact]
        public void Constructor_Should_TrimName_When_NameHasExtraSpaces()
        {
            var name = "  Neurology  ";

            var act = new Specialty(name);

            Assert.Equal("Neurology", act.SpecialtyName);
        }
        [Fact]
        public void Two_Specialities_With_Same_Name_Should_Be_Equal()
        {
            var s1 = new Specialty("Dermatology");
            var s2 = new Specialty("Dermatology");
                      
            Assert.Equal(s1, s2);
        }


    }
}