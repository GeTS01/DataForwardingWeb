using DataForwardingWeb.Domain.Enum;
using Domain;
using DTO;

namespace DataForwardingWeb.DTO.Data
{
    public class DeviceData : Data<Device>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string NitId { get; set; }
        public string NitPriductTypeId { get; set; }
        public string NitPipelineId { get; set; }
        public DeviceType Type { get; set; }
        public NitDeviceType NitType { get; set; }
        public NitOperationType NitOperationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DeviceData(Device entity) : base(entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            NitId = entity.NitId;
            NitPriductTypeId = entity.NitProductTypeId;
            NitPipelineId = entity.NitPipelineId;
            Type = entity.Type;
            NitType = entity.NitType;
            NitOperationType = entity.NitOperationType;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
        }
    }
}
