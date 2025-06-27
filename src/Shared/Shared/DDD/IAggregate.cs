using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DDD
{

    public interface IAggregate<T>: IAggregate, IEntity<T>
    {

    }

    public interface IAggregate
    {
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }

        public IDomainEvent[] ClearDomainEvents();

    }
}
