using DataForwardingWeb.Repository.Base;
using Domain;
using Repository.Repositores.Interfaces;
using Storage;

namespace Repository.Repositores
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        public ServerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
