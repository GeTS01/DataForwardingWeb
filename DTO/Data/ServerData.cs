using Domain;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Data
{
    public class ServerData : Data<Server>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public ServerType ServerType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long OrganizationId { get; set; }

        public ServerData(Server entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            IpAddress = entity.IpAddress;
            Cpu = entity.Cpu;
            Ram = entity.Ram;
            ServerType = entity.ServerType;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
            OrganizationId = entity.OrganizationId;
        }
    }
}
