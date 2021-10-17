using Nike.Domain.Common;

namespace Nike.Domain.Entities
{
    public class Village : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

    }
}
