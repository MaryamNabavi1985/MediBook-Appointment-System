using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public sealed record FullName
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        //private FullName() { }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentNullException("First name is required", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException("Last name is required", nameof(lastName));
            if (firstName.Length > 50) throw new ArgumentException("First name cannot exceed 50 characters", nameof(firstName));
            if (lastName.Length > 50) throw new ArgumentException("Last name cannot exceed 50 characters", nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();

        }

        public string DisplayName => $"{FirstName} {LastName}";
        public override string ToString() => DisplayName;
    }
}
