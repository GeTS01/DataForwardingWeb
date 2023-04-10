using Domain;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class NetworkModel : IModel<Network>
    {
        [Required(ErrorMessage = "Укажите название сети")]
        [MinLength(3, ErrorMessage = "Минимальная длинна названия сети не менее 3")]
        [MaxLength(255, ErrorMessage = "Максимальная длинна названия сети не более 255")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите тип сети")]
        public NetworkType NetworkType { get; set; }

        [Required(ErrorMessage = "Укажите тип протокола")]
        public ProtoсolType ProtoсolType { get; set; }

        [Required(ErrorMessage = "Опишите настройки вашей сети")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор организации")]
        public long OrganizationId { get; set; }

        public NetworkModel() { }
        public NetworkModel(
            string name,
            NetworkType networkType,
            ProtoсolType protoсolType,
            string description,
            long organizationId)
        {
            Name = name;
            NetworkType = networkType;
            ProtoсolType = protoсolType;
            Description = description;
            OrganizationId = organizationId;
        }

        public Network toEntity()
        {
            Network network = new Network();
            network.Name = Name;
            network.NetworkType = NetworkType;
            network.ProtoсolType = ProtoсolType;
            network.Description = Description;
            network.OrganizationId = OrganizationId;

            return network;
        }

        public Network toEntity(long id)
        {
            Network network = new Network();
            network.Name = Name;
            network.NetworkType = NetworkType;
            network.ProtoсolType = ProtoсolType;
            network.Description = Description;
            network.OrganizationId = OrganizationId;
            return network;
        }
    }
}
