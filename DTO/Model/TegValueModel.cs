using Domain;
using DTO;
using System.ComponentModel.DataAnnotations;

namespace DataForwardingWeb.DTO.Model
{
    public class TegValueModel : IModel<TagValue>
    {
        [Required(ErrorMessage = "Укажите идентификатор устройства")]
        public long TagId { get; set; }

        [Required(ErrorMessage = "Укажите значение")]
        public double Value { get; set; }

        public DateTime createdAt { get; set; }

        public TegValueModel(long tagId, double value)
        {
            this.Value = value;
            this.TagId = tagId;
        }
        public TegValueModel() { }

        public TagValue toEntity()
        {
            TagValue tag = new TagValue();
            tag.Value = Value;
            tag.TagId = TagId;

            return tag;
        }

        public TagValue toEntity(long id)
        {
            TagValue tag = new TagValue();
            tag.Value = Value;
            tag.TagId = TagId;
            return tag;
        }
    }
}
