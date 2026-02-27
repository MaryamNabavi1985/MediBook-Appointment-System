using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record Specialty
    {
         public string SpecialtyName { get;init; }
         
        private Specialty() { }

        public Specialty(string name) { 
            if(name == null) 
                throw new ArgumentNullException("name");

            name = name.Trim();
            SpecialtyName = name;

        }

        public override string ToString() => SpecialtyName;
    }
}
