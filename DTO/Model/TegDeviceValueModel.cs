using Domain;
using System.ComponentModel.DataAnnotations;

namespace DataForwardingWeb.DTO.Model
{
    public class TegDeviceValueModel
    {
        [Required(ErrorMessage = "Укажите идентификатор устройства")]
        public long deviceId { get; set; }

        [Required(ErrorMessage = "Укажите значение")]
        public string value { get; set; }

        List<Tag> tags;
        public TegDeviceValueModel(long deviceId, string value, List<Tag> tags)
        {
            this.deviceId = deviceId;
            this.value = value;
            this.tags = tags;
        }
    }
}
