using DataForwardingWeb.Repository.Base;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Storage;

namespace DataForwardingWeb.Repository.Repositores
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context)
        {
        }
    }
}