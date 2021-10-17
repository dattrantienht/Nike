using Nike.Domain.Common;
using Nike.Domain.Entities;

namespace Nike.Domain.Event
{
    public class CityCreatedEvent : DomainEvent
    {
        public CityCreatedEvent(City city)
        {
            City = city;
        }

        public City City { get; }
    }
}
