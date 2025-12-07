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
            Street = street?.Trim() ?? throw new ArgumentNullException(nameof(street));
            City = City?.Trim() ?? throw new ArgumentNullException(nameof(city));
            PostalCode = PostalCode?.Trim() ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country.Trim();
        }
    }
}
