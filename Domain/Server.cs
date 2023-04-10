using DataForwardingWeb.Domain.Base;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("server")]
    public class Server : PersistentObject
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("ipAddress")]
        public string IpAddress { get; set; }

        [Column("cpu")]
        public string? Cpu { get; set; }

        [Column("ram")]
        public string? Ram { get; set; }

        [Column("serverType")]
        public ServerType ServerType { get; set; }

        [Column("comment")]
        public string? Comment { get; set; }

        public Organization Organization { get; set; }
        public long OrganizationId { get; set; }


        public Server(
            string name,
            string ipAddress,
            string cpu,
            string ram, 
            ServerType serverType, 
            string comment, 
            long organizationId)
        {
            Name = name;
            IpAddress = ipAddress;
            Cpu = cpu;
            Ram = ram;
            ServerType = serverType;
            Comment = comment;
            OrganizationId = organizationId;
        }
        public Server() { }
    }
}
