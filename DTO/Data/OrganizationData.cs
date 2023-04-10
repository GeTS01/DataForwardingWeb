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
    public class OrganizationData : Data<Organization>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }  
        public string Description { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public OrganizationData(Organization entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Address = entity.Address;
            PhoneNumber = entity.PhoneNumber;
            Email = entity.Email;
            WebSite = entity.WebSite;
            Description = entity.Description;
            OrganizationType = entity.OrganizationType;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
        }
    }
}
