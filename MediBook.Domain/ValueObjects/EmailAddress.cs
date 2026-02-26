using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record EmailAddress
    {
        private static readonly Regex EmailRegEx= new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

        public string Email { get; init; }
        //private EmailAddress() { }

        public EmailAddress(string email) { 
        if(string.IsNullOrEmpty(email)) throw new ArgumentNullException("email");
        if (!EmailRegEx.IsMatch(email)) throw new ArgumentException("Invalid Email Format");

        Email = email.ToLowerInvariant();

        }
        public override string ToString() => Email;
        
  
    }
}
