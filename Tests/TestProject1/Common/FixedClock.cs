using MediBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Tests.Common
{
    public class FixedClock:IClock
    {
        public DateTime Now { get; }

        public FixedClock(DateTime fixedUtcTime)
        {
            Now = fixedUtcTime.Kind == DateTimeKind.Utc
                ? fixedUtcTime 
                : fixedUtcTime.ToUniversalTime();
        }
    }
}
