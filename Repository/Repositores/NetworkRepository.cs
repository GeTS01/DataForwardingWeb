using DataForwardingWeb.Repository.Base;
using Domain;
using Repository.Repositores.Interfaces;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositores
{
    public class NetworkRepository : Repository<Network>, INetworkRepository
    {
        public NetworkRepository(AppDbContext context) : base(context)
        {
        }
    }
}
