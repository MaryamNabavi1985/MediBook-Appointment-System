using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record DateOfBirth
    {
        public DateTime Value { get; }

        private DateOfBirth()
        {

        }
        public DateOfBirth(DateTime value) {
            var date = value.Date;
            if (date > DateTime.Today) throw new ArgumentException("Birth date cannot be in the future" ,nameof(value));
            if (date < DateTime.Today.AddYears(-130)) throw new ArgumentException("Invalid birth date." ,nameof(value));

            Value = date;
        }

        public int CalculateAge(DateTime today)
        {
            var age = today.Year - Value.Year;
            if (Value.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
