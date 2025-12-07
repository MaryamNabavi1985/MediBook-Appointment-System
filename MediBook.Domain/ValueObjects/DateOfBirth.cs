using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record DateOfBirth
    {
        public DateTime Value { get; init; }

        public DateOfBirth()
        {

        }
        public DateOfBirth(DateTime value) {
            if (value > DateTime.Today) throw new ArgumentException("Birth date cannot be in the future");
            if (value < DateTime.Today.AddYears(-130)) throw new ArgumentException("Invalid birth date");

            Value = value.Date;
        }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - Value.Year;
            if (Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
