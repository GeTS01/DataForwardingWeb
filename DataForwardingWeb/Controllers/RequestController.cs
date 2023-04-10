using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.Service.Implementation;
using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;

namespace DataForwardingWeb.Controllers
{
    public class RequestController : BaseController
    {
        private readonly RequestService _requestService;

        public RequestController(RequestService requestService)
        {
            _requestService = requestService;
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<RequestData, Request>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read([FromQuery] long id)
        {
            var responce = _requestService.read(id);
            return Ok(responce);
        }

        /// <summary>
        ///Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(RequestData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage([FromQuery] int number, int size)
        {
            var responce = _requestService.read(number, size);
            return Ok(responce);
        }

        /// <summary>
        ///Контроллер для получения всех устройств созданных в заданном диапазоне времени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<RequestData>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("read_by_interval")]
        public IActionResult readByInterval(DateTime? startDate, DateTime? endDate)
        {
            var responce = _requestService.GetByInterval(startDate, endDate);
            return Ok(responce);
        }
    }
}
