using Domain;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace DTO.Model
{
    public class ServerModel : IModel<Server>
    {
        [Required(ErrorMessage = "Укажите название сервера")]
        [MinLength(3, ErrorMessage = "Минимальная длинна названия сервера не менее 3")]
        [MaxLength(255, ErrorMessage = "Максимальная длинна названия сервера не более 255")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите ip address")]
        public string IpAddress { get; set; }
        public string Cpu { get; set; }  
        public string Ram { get; set; } 

        [Required(ErrorMessage = "Укажите тип сервера")]
        public ServerType ServerType { get; set; }
        public string Comment { get; set; } //??

        [Required(ErrorMessage = "Укажите идентификатор организации")]
        public long OrganizationId { get; set; }
        public ServerModel(
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

        public Server toEntity()
        {
            Server server = new Server();
            server.Name = Name;
            server.IpAddress = IpAddress;
            server.Cpu = Cpu;
            server.Ram = Ram;
            server.ServerType = ServerType;
            server.OrganizationId = OrganizationId;
            return server;
        }

        public Server toEntity(long id)
        {
            Server server = new Server();
            server.Name = Name;
            server.IpAddress = IpAddress;
            server.Cpu = Cpu;
            server.Ram = Ram;
            server.ServerType = ServerType;
            server.OrganizationId = OrganizationId;
            return server;
        }

        public ServerModel() { }
    }
}
