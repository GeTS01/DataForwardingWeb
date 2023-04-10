using DataForwardingWeb.Domain.Enum;
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
    public class NetworkFilterModel : FilterModel<Network>
    {
        public string? Name { get; set; }
        public NetworkType NetworkType { get; set; }
        public ProtoсolType ProtoсolType { get; set; }
        public DateTime CreatedAt { get; set; }

        public NetworkFilterModel() { }
    }
}
