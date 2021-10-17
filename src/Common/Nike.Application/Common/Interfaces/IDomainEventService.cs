using System.Threading.Tasks;
using Nike.Domain.Common;

namespace Nike.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
