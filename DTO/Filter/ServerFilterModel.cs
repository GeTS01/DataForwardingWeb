using DataForwardingWeb.DTO.Filter;
using Domain;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Filter
{
    public class ServerFilterModel : FilterModel<Server>
    {
        public string? Name { get; set; }
        public string IpAddress { get; set; }
        public ServerType ServerType { get; set; }

        public ServerFilterModel() { }
    }
}
