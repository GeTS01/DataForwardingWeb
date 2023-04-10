using DataForwardingWeb.Repository.Base;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Storage;

namespace DataForwardingWeb.Repository.Repositores
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(AppDbContext context) : base(context)
        {
        }
    }
}