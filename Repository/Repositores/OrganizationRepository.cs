using DataForwardingWeb.Repository.Base;
using Domain;
using Repository.Repositores.Interfaces;
using Storage;

namespace Repository.Repositores
{
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
