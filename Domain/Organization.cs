using DataForwardingWeb.Domain.Base;
using Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("organization")]
    public class Organization : PersistentObject
    {

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("email")]

        public string Email { get; set; }
        [Column("webSite")]

        public string WebSite { get; set; }
        [Column("description")]

        public string Description { get; set; }

        [Column("organizationType")]
        public OrganizationType OrganizationType { get; set; }

        public Organization(
            string name , 
            string address,
            string phoneNumber,
            string webSite , 
            string description ,
            OrganizationType organization) 
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            WebSite = webSite;
            Description = description;
            OrganizationType = organization;
        }

        public Organization()
        {
        }
    }
}
