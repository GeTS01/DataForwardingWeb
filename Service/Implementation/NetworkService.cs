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
    public class NetworkService : FilterService<NetworkData, Network>, IFullAccessService<NetworkData, Network>
    {
        private readonly INetworkRepository _networkRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public NetworkService(INetworkRepository networkRepository, IOrganizationRepository organizationRepository)
        {
            _networkRepository = networkRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<Data<Network>> create(IModel<Network> model)
        {
            Network network = model.toEntity();

            var networkOrg = await _organizationRepository
                .GetAll()
                .Where(x => x.Id == network.OrganizationId)
                .FirstOrDefaultAsync();

            if (networkOrg == null)
            {
                throw new Exception($"Network with {network.OrganizationId} not found OrganizationId");
            }
            _networkRepository.CreateAsync(network);
            _networkRepository.SaveChangesAsync();
            return new NetworkData(network);
        }

        public override Data<Network> read(long id)
        {
            return new NetworkData(_networkRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
            ?? throw new Exception($"Network with {id} not found"));
        }

        public override Page<NetworkData, Network> read(int number, int size)
        {
            long totalCount = _networkRepository.GetAll().Count();
            return new Page<NetworkData, Network>(
                number,
                size,
                totalCount / size,
                totalCount,
                _networkRepository
                .GetAll()
                .Skip(number * size)
                .Take(size)
                .Select(x => new NetworkData(x))
                .ToList() ?? new List<NetworkData>()
                );
        }

        public void remove(long id)
        {
            var network = _networkRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
                 ?? throw new Exception($"Network with {id} not found");
            _networkRepository.Remove(network);
            _networkRepository.SaveChanges();
        }

        public Data<Network> updateInId(long id, IModel<Network> model)
        {
            var md = (NetworkModel)model;
            var network = _networkRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                ?? throw new Exception($"Network with {id} not found");
            network.Name = md.Name;
            network.Description = md.Description;
            network.NetworkType = md.NetworkType;
            network.ProtoсolType = md.ProtoсolType;
          
            _networkRepository.Update(network);
            _networkRepository.SaveChangesAsync();
            return new NetworkData(network);
        }
        public void delete(long id)
        {
            var network = _networkRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                 ?? throw new Exception($"Device with {id} not found");

            _networkRepository.Delete(network);
            _networkRepository.SaveChanges();
        }
        public List<NetworkData> GetByName(string? networkName, NetworkType networkType, ProtoсolType protoсolType)
        {
            var result = _networkRepository
                .GetAll();
            if (networkName != null)
                result = result.Where(x => x.Name.StartsWith(networkName));

            if (networkType != null)
                result = result.Where(x => x.NetworkType == networkType);

            if (protoсolType != null)
                result = result.Where(x => x.ProtoсolType == protoсolType);

            return result.Select(x => new NetworkData(x)).ToList(); ;
        }

        public List<Network> GetAllNetwork()
        {
            return _networkRepository
                .GetAll()
                .ToList();
        }

        public override IRepository<Network> GetRepository()
        {
            return _networkRepository;
        }
    }
}
