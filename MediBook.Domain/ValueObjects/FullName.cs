using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record FullName
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        //private FullName() { }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required");

            FirstName = firstName.Trim();
            LastName = lastName.Trim();

        }

        public string DisplayName => $"{FirstName} {LastName}";
        public override string ToString() => DisplayName;
    }
}
