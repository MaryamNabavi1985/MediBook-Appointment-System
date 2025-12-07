using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediBook.Domain.Common
{
    public abstract class Entity<TId> where TId : notnull
    {
        public TId Id { get; protected set; } = default!;
        public DateTime? CreatedDate { get;protected set; }
        public DateTime? UpdateDate { get;protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        
        protected void AddDomainEvent(IDomainEvent domainEvent) 
            => _domainEvents.Add(domainEvent);

        protected void SetCreatedDate(DateTime created) => CreatedDate = created;
        protected void SetUpdateDate(DateTime dateTime) => UpdateDate = dateTime;
        
    }
}
