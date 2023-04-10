using DataForwardingWeb.Domain.Enum;
using Domain;

namespace DataForwardingWeb.DTO.Filter
{
    public class DeviceFilterModel : FilterModel<Device>
    {
        //name, nit_id, nit_product_type_id, nit_pipeline_id, 
        public string? SearchText { get; set; }
        public string? DeviceName { get; set; }
        public long? OwnfieldMin { get; set; }
        public long? OwnfieldMax { get; set; }
        public DeviceType[]? TypeItems { get; set; }
        public NitDeviceType[]? NitTypeItems { get; set; }
        public NitOperationType[]? NitOperationItems { get; set; }
        public DateTime? CreatedAtStart { get; set; }
        public DateTime? CreatedAtEnd { get; set; }
        public DateTime? UpdatedAtStart { get; set; }
        public DateTime? UpdatedAtEnd { get; set; }

        public DeviceFilterModel() { }
    }
}