using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Domain
{
    [Table("tag")]
    public class Tag : PersistentObject
    {
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }
        // id устройства
        public long DeviceId { get; set; }
        public Device Device { get; set; }

        [Column("measure_type")]
        public MeasureType MeasureType { get; set; }

        [Column("path_to_tag")]
        public string PathToTag { get; set; }
        public Tag() { }
    }
}