using Domain;
using Domain.Enum;

namespace DTO.Data
{
    public class NetworkData : Data<Network>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public NetworkType NetworkType { get; set; }
        public ProtoсolType ProtoсolType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long OrganizationId { get; set; }

        public NetworkData(Network entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            NetworkType = entity.NetworkType;
            ProtoсolType = entity.ProtoсolType;
            Description = entity.Description;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
            OrganizationId = entity.OrganizationId;


        }
    }
}
