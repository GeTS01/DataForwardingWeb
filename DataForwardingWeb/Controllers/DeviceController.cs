using DataForwardingWeb.Domain.Enum;
using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.DTO.Filter;
using DataForwardingWeb.DTO.Model;
using DataForwardingWeb.Service.Implementation;
using Domain;
using DTO;
using Microsoft.AspNetCore.Mvc;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;

namespace DataForwardingWeb.Controllers
{
    public class DeviceController : BaseController
    {
        private readonly DeviceService _deviceService;

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// Контроллер для создания сущности device(счетчик)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeviceData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("create")]
        public IActionResult Create([FromQuery] DeviceModel request)
        {
            var device = _deviceService.create(request);
            return Ok(device);
        }

        /// <summary>
        /// Контроллер, который помечает запись в базе как удаленную
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpDelete("delete")]
        public void Delete([FromQuery] long id)
        {
            _deviceService.delete(id);
        }



        /// <summary>
        /// Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeviceData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read([FromQuery] long id)
        {
            var responce = _deviceService.read(id);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<DeviceData, Device>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage(int page, int size)
        {
            var responce = _deviceService.read(page, size);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для удаления записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpDelete("remove")]
        public void Remove([FromQuery] long id)
        {
            _deviceService.remove(id);
        }

        /// <summary>
        /// Контроллер для обновления записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(DeviceData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPatch("update_by_id")]
        public IActionResult updateInId(long id, [FromQuery] DeviceModel request)
        {
            var responce = _deviceService.updateInId(id, request);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств созданных в заданном диапазоне времени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<DeviceData>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("read_by_interval")]
        public IActionResult readByInterval(string? deviceName, DeviceType deviceType, string? nitType)
        {
            var responce = _deviceService.GetByName(deviceName, deviceType, nitType);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Device>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_all")]
        public IActionResult getAll()
        {
            var responce = _deviceService.GetAllDevices();
            return Ok(responce);
        }


        /// <summary>
        /// Контроллер для фильтрации устройста
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<DeviceData, Device>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("filter")]
        public IActionResult filter(DeviceFilterModel filter, int number, int size)
        {
            var result = _deviceService.readFilter(filter, number, size);
            return Ok(result);
        }
    }
}