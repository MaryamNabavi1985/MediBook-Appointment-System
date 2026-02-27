using MediBook.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Tests.ValueObjects
{
    public class SpecialityTest
    {
        [Fact]
        public void Constructor_should_ThrowException_When_NameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Speciality(null));
        }


        [Fact]
        public void Constructor_Should_SetName_When_NameIsValid()
        {
            var name = "Cardiology";

            var act = new Speciality(name);

            Assert.Equal("Cardiology",act.SpecialityName);

        }

        [Fact]
        public void Constructor_Should_TrimName_When_NameHasExtraSpaces()
        {
            var name = "  Neurology  ";

            var act = new Speciality(name);

            Assert.Equal("Neurology", act.SpecialityName);
        }
        [Fact]
        public void Two_Specialities_With_Same_Name_Should_Be_Equal()
        {
            var s1 = new Speciality("Dermatology");
            var s2 = new Speciality("Dermatology");
                      
            Assert.Equal(s1, s2);
        }


    }
}