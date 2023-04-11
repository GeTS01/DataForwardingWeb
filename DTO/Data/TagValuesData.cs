using Domain;
using DTO;
using DTO.Data;

namespace DataForwardingWeb.DTO.Data
{
    public class TagValuesData : Data<TagValue>
    {
        public long Id { get; set; }
        public TagData Tag { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public TagValuesData(TagValue entity) : base(entity)
        {
            Id = entity.Id;
            Value = entity.Value;
            Tag = new TagData(entity.Tag);
            CreatedAt = entity.CreatedAt;
        }
    }
}