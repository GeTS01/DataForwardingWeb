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
    public class OrganizationFilterModel : FilterModel<Organization>
    {
        public string? Name { get; set; }
        public OrganizationType OrganizationType { get; set; }

        public OrganizationFilterModel() { }
    }
}
