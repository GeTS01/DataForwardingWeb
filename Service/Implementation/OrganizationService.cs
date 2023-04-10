using DataForwardingWeb.DTO.Filter;
using Domain;
using Domain.Enum;
using DTO;
using DTO.Data;
using DTO.Model;
using Repository.Repositores;
using Repository.Repositores.Interfaces;

namespace Service.Implementation
{
    public class OrganizationService : IReadWriteService<OrganizationData, Organization>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationService(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        public Data<Organization> create(IModel<Organization> model)
        {
            Organization organization = model.toEntity();
            _organizationRepository.Create(organization);
            _organizationRepository.SaveChanges();
            return new OrganizationData(organization);
        }
        public Data<Organization> read(long id)
        {
            return new OrganizationData(_organizationRepository
                .GetAll()
                .FirstOrDefault(x => x.Id.Equals(id))
            ?? throw new Exception($"Organization with {id} not found"));
        }
        public Page<OrganizationData, Organization> read(int number, int size)
        {
            long totalCount = _organizationRepository.GetAll().Count();
            return new Page<OrganizationData, Organization>(
                number,
                size,
            totalCount / size,
            totalCount,
                _organizationRepository
                .GetAll()
                .Skip(number * size)
                .Take(size)
                .Select(x => new OrganizationData(x))
                .ToList() ?? new List<OrganizationData>()
                );
        }
        public Page<OrganizationData, Organization> readFilter(FilterModel<Organization> filter, int number, int size)
        {
            //var filterService = new FilterService<Organization>();
            //var res = filterService.Read(filter, _organizationRepository)
            //.Select(x => new OrganizationData(x)).ToList()
            //    ?? new List<OrganizationData>();

            //return new Page<OrganizationData, Organization>()
            //{
            //    Size = res.Count,
            //    items = res,
            //    Number = res.Count,
            //    TotalCount = res.Count,
            //    TotalPages = res.Count
            //};
            return null;
        }
        public void remove(long id)
        {
            var organization = _organizationRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
                 ?? throw new Exception($"Organization with {id} not found");
            _organizationRepository.Remove(organization);
            _organizationRepository.SaveChanges();
        }
        public Data<Organization> updateInId(long id, IModel<Organization> model)
        {
            var md = (OrganizationModel)model;
            var organization = _organizationRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                ?? throw new Exception($"Organization with {id} not found");
            organization.Name = md.Name;
            organization.Description = md.Description;
            organization.PhoneNumber = md.PhoneNumber;
            organization.Email = md.Email;
            organization.Address = md.Address;
            organization.OrganizationType = md.OrganizationType;
            
            _organizationRepository.Update(organization);
            _organizationRepository.SaveChangesAsync();
            return new OrganizationData(organization);
        }
        public void delete(long id)
        {
            var network = _organizationRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                 ?? throw new Exception($"Device with {id} not found");

            _organizationRepository.Delete(network);
            _organizationRepository.SaveChanges();

        }
        public List<OrganizationData> GetByName(string? organizationName, OrganizationType  organizationType)
        {
            var result = _organizationRepository
                .GetAll();
            if (organizationName != null)
                result = result.Where(x => x.Name.StartsWith(organizationName));

            if (organizationType != null)
                result = result.Where(x => x.OrganizationType == organizationType);

           
            return result.Select(x => new OrganizationData(x)).ToList(); ;
        }
        public List<Organization> GetAllOrganization()
        {
            return _organizationRepository
                .GetAll()
                .ToList();
        }
        Task<Data<Organization>> IReadWriteService<OrganizationData, Organization>.create(IModel<Organization> model)
        {
            throw new NotImplementedException();
        }
    }
}
