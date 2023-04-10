using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("device")]
    public class Device : PersistentObject
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("own_field")]
        public long Ownfield { get; set; }

        [Column("nit_id")]
        public string NitId { get; set; }

        [Column("nit_product_type_id")]
        public string NitProductTypeId { get; set; }

        [Column("nit_pipeline_id")]
        public string NitPipelineId { get; set; }

        [Column("device_type")]
        public DeviceType Type { get; set; }

        [Column("nit_type")]
        public NitDeviceType NitType { get; set; }

        [Column("nit_operation_type")]
        public NitOperationType NitOperationType { get; set; }

        public Device() { }

        public Device(
            string name,
            string nitId,
            string nitProductTypeId,
            string nitPipelineId,
            DeviceType type,
            NitDeviceType nitType,
            NitOperationType nitOperationType
            )
        {
            Name = name;
            NitId = nitId;
            NitProductTypeId = nitProductTypeId;
            NitPipelineId = nitPipelineId;
            Type = type;
            NitType = nitType;
            NitOperationType = nitOperationType;
        }
    }
}
