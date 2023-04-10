using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.Service.Implementation;
using Domain;
using Microsoft.AspNetCore.Mvc;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;

namespace DataForwardingWeb.Controllers
{
    public class TagController : BaseController
    {
        private readonly DeviceService _deviceService;
        private readonly TagValuesService _tagsValuesService;

        public TagController(DeviceService deviceService, TagValuesService tagsValuesService)
        {
            _deviceService = deviceService;
            _tagsValuesService = tagsValuesService;
        }

        [ProducesResponseType(typeof(List<Tag>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_tags")]
        public IActionResult getTagOnDevice(long deviceId)
        {
            var responce = _deviceService.GetTagsOnDevice(deviceId);
            return Ok(responce);
        }

        /// <summary>
        ///Контроллер для получения всех тегов
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(TagValuesData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_all_tags")]
        public IActionResult GetAll()
        {
            var responce = _tagsValuesService.GetAllTag();
            return Ok(responce);
        }
    }
}
