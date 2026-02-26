using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; init; } = string.Empty;
        public string City { get; init; } = string.Empty;
        public string PostalCode { get; init; } = string.Empty;
        public string Country { get; init; } = "Germany";

        private Address() {}
        public Address(string street, string city, string postalCode, string country = "Germany")
        {
            
            Street = Guard(street, nameof(street));
            City = Guard(city, nameof(city));
            PostalCode = Guard(postalCode, nameof(postalCode));
            Country = Guard(country, nameof(country));
        }


        private static string Guard(string value, string paramName)
        {
            if(value is null)
                throw new ArgumentNullException(paramName);
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be empty or whitespace.", paramName);
            return value.Trim();
        }

    }
}
