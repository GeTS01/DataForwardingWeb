using DataForwardingWeb.Domain.Base;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain
{
    [Table("network")]
    public class Network : PersistentObject
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("networkType")]
        public NetworkType NetworkType { get; set; }

        [Column("protoсolType")]
        public ProtoсolType ProtoсolType { get; set; }

        [Column("description")]
        public string Description { get; set; }
        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }

        public Network(){}

        public Network(
            string name,
            NetworkType networkType, 
            ProtoсolType protoсolType,
            string description,
            long organizationId
             )
        {
            Name = name;
            NetworkType = networkType;
            ProtoсolType = protoсolType;
            Description = description;
            OrganizationId = organizationId;
        }
    }
}
