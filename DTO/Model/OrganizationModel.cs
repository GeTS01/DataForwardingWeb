using Domain;
using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Model
{
    public class OrganizationModel : IModel<Organization>
    {
        [Required(ErrorMessage = "Укажите название организации")]
        [MinLength(3, ErrorMessage = "Минимальная длинна названия организации не менее 3")]
        [MaxLength(255, ErrorMessage = "Максимальная длинна названия организации не более 255")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите адрес организации")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Укажите Email")]
        public string Email { get; set; }

        public string WebSite { get; set; }  //??

        [Required(ErrorMessage = "Укажите описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите тип организации")]
        public OrganizationType OrganizationType { get; set; }

       

        public OrganizationModel(
            string name,
            string address,
            string phoneNumber,
            string email,
            string webSite,
            string description,
            OrganizationType organizationType           
            )
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            WebSite = webSite;
            Description = description;
            OrganizationType = organizationType;
        }

        public Organization toEntity()
        {
            Organization organization = new Organization();
            organization.Name = Name;
            organization.Address = Address;
            organization.PhoneNumber = PhoneNumber;
            organization.Email = Email;
            organization.WebSite = WebSite;
            organization.Description = Description;
            organization.OrganizationType = OrganizationType;
            return organization;

        }
        public Organization toEntity(long id)
        {
            Organization organization = new Organization();
            organization.Name = Name;
            organization.Address = Address;
            organization.PhoneNumber = PhoneNumber;
            organization.Email = Email;
            organization.WebSite = WebSite;
            organization.Description = Description;
            organization.OrganizationType = OrganizationType;
            return organization;
        }

        public OrganizationModel() { }
    }
}
