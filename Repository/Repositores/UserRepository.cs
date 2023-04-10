using DataForwardingWeb.Repository.Base;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Storage;

namespace DataForwardingWeb.Repository.Repositores
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {

        }
    }
}