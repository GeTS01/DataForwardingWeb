using DataForwardingWeb.Domain.Enum;
using Domain;
using DTO;
using System.ComponentModel.DataAnnotations;

namespace DataForwardingWeb.DTO.Model
{
    public class DeviceModel : IModel<Device>
    {
        [Required(ErrorMessage = "Укажите имя")]
        [MinLength(3, ErrorMessage = "Минимальная длинна имени не менее 3")]
        [MaxLength(255, ErrorMessage = "Максимальная длинна имени не более 255")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор отчета АО 'НИТ'")]
        public string NitId { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор типа продукта АО 'НИТ'")]
        public string NitProductTypeId { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор магистрального нефтепровода АО 'НИТ'")]
        public string NitPipelineId { get; set; }

        [Required(ErrorMessage = "Укажите тип устройстра")]
        public DeviceType Type { get; set; }

        [Required(ErrorMessage = "Укажите тип прибора учета операции в АО 'НИТ'")]
        public NitDeviceType NitType { get; set; }

        [Required(ErrorMessage = "Укажите идентификатор типа операции в АО 'НИТ'")]
        public NitOperationType NitOperationType { get; set; }

        public long Ownfield { get; set; }

        public DeviceModel(
           string name,
           string nitId,
           string nitProductTypeId,
           string nitPipelineId,
           DeviceType type,
           NitDeviceType nitType,
           NitOperationType nitOperationType,
           long ownfield)
        {
            Name = name;
            NitId = nitId;
            NitProductTypeId = nitProductTypeId;
            NitPipelineId = nitPipelineId;
            Type = type;
            NitType = nitType;
            NitOperationType = nitOperationType;
            Ownfield = ownfield;
        }

        public Device toEntity()
        {
            Device device = new Device();
            device.Name = Name;
            device.NitId = NitId;
            device.NitPipelineId = NitPipelineId;
            device.Type = Type;
            device.NitOperationType = NitOperationType;
            device.NitProductTypeId = NitProductTypeId;
            device.CreatedAt = DateTime.UtcNow;
            device.Ownfield = Ownfield;
            return device;
        }

        public Device toEntity(long id)
        {
            Device device = new Device();
            device.Id = id;
            device.Name = Name;
            device.NitId = NitId;
            device.NitPipelineId = NitPipelineId;
            device.Type = Type;
            device.NitOperationType = NitOperationType;
            device.NitProductTypeId = NitProductTypeId;
            device.CreatedAt = DateTime.UtcNow;
            device.Ownfield = Ownfield;
            return device;
        }

        public DeviceModel()
        {

        }
    }
}