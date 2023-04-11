using DataForwardingWeb.Repository.Base;
using Domain;
using Domain.Enum;
using DTO;
using DTO.Data;
using DTO.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Repositores.Interfaces;

namespace Service.Implementation
{
    public class ServerService : FilterService<ServerData, Server>, IReadWriteService<ServerData, Server>
    {
        private readonly IServerRepository _serverRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public ServerService(IServerRepository serverRepository, IOrganizationRepository organizationRepository)
        {
            _serverRepository = serverRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Data<Server>> create(IModel<Server> model)
        {
            Server server = model.toEntity();
            var serverOrg = await _organizationRepository
                .GetAll()
                .Where(x => x.Id == server.OrganizationId)
                .FirstOrDefaultAsync();
            if (serverOrg == null)
            {
                throw new Exception($"Network with {server.OrganizationId} not found OrganizationId");
            }
            _serverRepository.Create(server);
            _serverRepository.SaveChanges();
            return new ServerData(server);
        }

        public override Data<Server> read(long id)
        {
            return new ServerData(_serverRepository
                .GetAll()
                .FirstOrDefault(x => x.Id.Equals(id))
             ?? throw new Exception($"Server with {id} not found"));
        }

        public override Page<ServerData, Server> read(int number, int size)
        {
            long totalCount = _serverRepository
                .GetAll()
                .Count();
            return new Page<ServerData, Server>(
                number,
                size,
                totalCount / size,
                totalCount,
                _serverRepository
                .GetAll()
                .Skip(number * size)
                .Take(size)
                .Select(x => new ServerData(x)).ToList() ?? new List<ServerData>()
                );
        }



        public void remove(long id)
        {
            var server = _serverRepository
                .GetAll()
                .FirstOrDefault(x => x.Id.Equals(id))
               ?? throw new Exception($"Server with {id} not found");
            _serverRepository.Remove(server);
            _serverRepository.SaveChanges();
        }

        public Data<Server> updateInId(long id, IModel<Server> model)
        {
            var md = (ServerModel)model;
            var server = _serverRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                ?? throw new Exception($"Server with {id} not found");
            server.Name = md.Name;
            server.IpAddress = md.IpAddress;
            server.Ram = md.Ram;
            server.Cpu = md.Cpu;
            server.ServerType = md.ServerType;

            _serverRepository.Update(server);
            _serverRepository.SaveChangesAsync();
            return new ServerData(server);
        }

        Task<Data<Server>> IReadWriteService<ServerData, Server>.create(IModel<Server> model)
        {
            throw new NotImplementedException();
        }

        public void delete(long id)
        {
            var server = _serverRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                 ?? throw new Exception($"Server with {id} not found");

            _serverRepository.Delete(server);
            _serverRepository.SaveChanges();

        }
        public List<ServerData> GetByName(string? serverName, string? ipAddress, ServerType serverType)
        {
            var result = _serverRepository
                .GetAll();
            if (serverName != null)
                result = result.Where(x => x.Name.StartsWith(serverName));

            if (ipAddress != null)
                result = result.Where(x => x.IpAddress == ipAddress);

            if (serverType != null)
            {
                result = result.Where(x => x.ServerType == serverType);
            }

            return result.Select(x => new ServerData(x)).ToList(); ;
        }

        public List<Server> GetAllServer()
        {
            return _serverRepository
                .GetAll()
                .ToList();
        }

        public override IRepository<Server> GetRepository()
        {
            return _serverRepository;
        }
    }
}
