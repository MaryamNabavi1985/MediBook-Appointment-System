using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public  record PhoneNumber
    {
        public string Value { get; init; }
        public PhoneNumber() { }
        public PhoneNumber(string value) {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Phone is required");
            Value = value.Trim();
        }
        public override string ToString() => Value;
       
    }
}
