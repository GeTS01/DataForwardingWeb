using Domain;

namespace DataForwardingWeb.DTO.Filter
{
    public class TagValueFilterModel : FilterModel<TagValue>
    {
        public double? Value { get; set; }
        public TagFilterModel? Tag { get; set; }
        public DateTime? StartCreatedAt { get; set; }
        public DateTime? EndCreatedAt { get; set; }
        public DateTime? StartUpdatedAt { get; set; }
        public DateTime? EndUpdatedAt { get; set; }

        public TagValueFilterModel() { }
    }
}
