using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.ValueObjects
{
    public record Speciality
    {
         public string SpecialityName { get;init; }
         
        private Speciality() { }

        public Speciality(string name) { 
            if(name == null) 
                throw new ArgumentNullException("name");

            name = name.Trim();
            SpecialityName = name;

        }

        public override string ToString() => SpecialityName;
    }
}
