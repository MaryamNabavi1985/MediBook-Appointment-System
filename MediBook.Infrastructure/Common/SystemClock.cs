using MediBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Infrastructure.Common
{
    public sealed class SystemClock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
