using Domain;

namespace DataForwardingWeb.DTO.Filter
{
    public class TagFilterModel : FilterModel<Tag>
    {
        public string? Name { get; set; }
        public DeviceFilterModel? Device { get; set; }
        public DateTime? StartCreatedAt { get; set; }
        public DateTime? EndCreatedAt { get; set; }
        public DateTime? StartUpdatedAt { get; set; }
        public DateTime? EndUpdatedAt { get; set; }
    }
}
