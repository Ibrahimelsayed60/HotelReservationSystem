using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.CQRS
{
    public interface IQuery<out T> : IRequest<T> where T: notnull
    {
    }
}
