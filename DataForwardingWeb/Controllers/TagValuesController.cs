using DataForwardingWeb.Domain.Enum;
using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.DTO.Model;
using DataForwardingWeb.Service.Implementation;
using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;

namespace DataForwardingWeb.Controllers
{
    public class TagValuesController : BaseController
    {
        private readonly TagValuesService _tagValuesService;

        public TagValuesController(TagValuesService tagValuesService)
        {
            _tagValuesService = tagValuesService;

        }

        /// <summary>
        ///Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(TagValuesData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read(long id)
        {
            var responce = _tagValuesService.read(id);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для создания записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(TagValuesData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("Create")]
        public IActionResult Create([FromQuery] TegValueModel request)
        {
            var responce = _tagValuesService.Create(request);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<TagValuesData, TagValue>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage(int page, int size)
        {
            var responce = _tagValuesService.read(page, size);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств созданных в заданном диапазоне времени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Tag>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_interval")]
        public IActionResult readByInterval(string? tagName, long? deviceId, MeasureType? type)
        {
            var responce = _tagValuesService.GetByNameAndDeviceIdAndType(tagName, deviceId, type);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всехзначений тега(TagValue) по его тегу(Tag)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<TagValue>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_tagValue_by_tag")]
        public IActionResult getTagValueByTag(long tagId)
        {
            var responce = _tagValuesService.GetTagValueOnTag(tagId);
            return Ok(responce);
        }
    }
}
