using DataForwardingWeb.Domain.Enum;
using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.DTO.Model;
using DataForwardingWeb.Repository.Base;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using DTO;
using DTO.Data;
using Service;
using Service.Implementation;
using System.Reflection;

namespace DataForwardingWeb.Service.Implementation
{
    public class DeviceService : FilterService<DeviceData, Device>, IFullAccessService<DeviceData, Device>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITagRepository _tagRepository;

        public DeviceService(IDeviceRepository deviceRepository, ITagRepository tagRepository)
        {
            _deviceRepository = deviceRepository;
            _tagRepository = tagRepository;
        }

        //Метод для создания сущности device(счетчик)
        public Data<Device> create(IModel<Device> model)
        {
            Device device = model.toEntity();
            _deviceRepository.Create(device);
            _deviceRepository.SaveChangesAsync();
            return new DeviceData(device);
        }

        // Метод, который помечает запись в базе как удаленную
        public void delete(long id)
        {
            var device = _deviceRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                 ?? throw new Exception($"Device with {id} not found");

            _deviceRepository.Delete(device);
            _deviceRepository.SaveChanges();

        }

        // Метод для нахождения записи в базе
        public override Data<Device> read(long id)
        {
            return new DeviceData(_deviceRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
            ?? throw new Exception($"Device with {id} not found"));
        }

        // Метод для удаления записи в базе
        public void remove(long id)
        {
            var device = _deviceRepository.GetAll().FirstOrDefault(x => x.Id.Equals(id))
                 ?? throw new Exception($"Device with {id} not found");
            _deviceRepository.Remove(device);
            _deviceRepository.SaveChanges();
        }

        // Метод для обновления записи в базе
        public Data<Device> updateInId(long id, IModel<Device> model)
        {
            var md = (DeviceModel)model;
            var device = _deviceRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault()
                ?? throw new Exception($"Device with {id} not found");
            device.Name = md.Name;
            device.NitId = md.NitId;
            device.NitProductTypeId = md.NitProductTypeId;
            device.NitPipelineId = md.NitPipelineId;
            device.Type = md.Type;
            device.NitType = md.NitType;
            device.NitOperationType = md.NitOperationType;
            _deviceRepository.Update(device);
            _deviceRepository.SaveChangesAsync();
            return new DeviceData(device);
        }

        private bool MinExpression(PropertyInfo ff, PropertyInfo deviceField, Device device)
        {
            long filterValue = (long)ff.GetValue(ff);
            long deviceValue = (long)deviceField.GetValue(device);
            bool result = deviceValue >= filterValue;
            return true;
        }

        // Метод для получения всех устройств созданных в заданном диапазоне времени
        public List<DeviceData> GetByName(string? deviceName, DeviceType deviceType, string? nitType)
        {
            var result = _deviceRepository
                .GetAll();
            if (deviceName != null)
                result = result.Where(x => x.Name.StartsWith(deviceName));

            if (deviceType != null)
                result = result.Where(x => x.Type == deviceType);

            if (nitType != null)
                result = result.Where(x => x.NitId == nitType);

            return result.Select(x => new DeviceData(x)).ToList(); ;
        }

        public List<Device> GetAllDevices()
        {
            return _deviceRepository.GetAll().ToList();
        }

        public List<Tag> GetTagsOnDevice(long devoiceId)
        {
            var device = _deviceRepository
                .GetAll()
                .Where(x => x.Id == devoiceId)
                .FirstOrDefault();

            var tags = _tagRepository
                .GetAll()
                .Where(x => x.DeviceId == device.Id)
                .ToList();
            return tags;
        }

        public override IRepository<Device> GetRepository()
        {
            return _deviceRepository;
        }

        public override Page<DeviceData, Device> read(int number, int size)
        {
            long totalCount = _deviceRepository.GetAll().Count();
            return new Page<NetworkData, Network>(
                number,
                size,
                totalCount / size,
                totalCount,
                _deviceRepository.GetAll().Skip(number * size).Take(size).Select(x => new NetworkData(x)).ToList() ?? new List<NetworkData>()
                );
        }
    }
}