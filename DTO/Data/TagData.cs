using DataForwardingWeb.Domain.Enum;
using Domain;
using DTO;
using DTO.Data;

namespace DataForwardingWeb.DTO.Data
{
    public class TagData : Data<Tag>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long DeviceId { get; set; }
        public MeasureType MeasureType { get; set; }

        public TagData(Tag tag) : base(tag)
        {
            if (tag == null)
                return;

            Id = tag.Id;
            Name = tag.Name;
            Description = tag.Description;
            DeviceId = tag.DeviceId;
            MeasureType = tag.MeasureType;
        }
    }
}