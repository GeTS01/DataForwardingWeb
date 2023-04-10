using Domain.Enum;
using Domain;
using DTO.Data;
using DTO.Filter;
using DTO.Model;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;


namespace DataForwardingWeb.Controllers
{
    public class ServerController : BaseController
    {
        private readonly ServerService _serverService;

        public ServerController(ServerService serverService)
        {
            _serverService = serverService;
        }

        /// <summary>
        /// Контроллер для создания сущности server(сервер)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ServerData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] ServerModel request)
        {
            var server = await _serverService.create(request);
            return Ok(server);
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
            _serverService.delete(id);
        }



        /// <summary>
        /// Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ServerData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read([FromQuery] long id)
        {
            var responce = _serverService.read(id);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<ServerData, Server>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage(int page, int size)
        {
            var responce = _serverService.read(page, size);
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
            _serverService.remove(id);
        }

        /// <summary>
        /// Контроллер для обновления записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ServerData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPatch("update_by_id")]
        public IActionResult updateInId(long id, [FromQuery] ServerModel request)
        {
            var responce = _serverService.updateInId(id, request);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств созданных в заданном диапазоне времени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<ServerData>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("read_by_interval")]
        public IActionResult readByInterval(string? serverName, string? ipAddress, ServerType serverType)
        {
            var responce = _serverService.GetByName(serverName,ipAddress,serverType);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Server>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_all")]
        public IActionResult getAll()
        {
            var responce = _serverService.GetAllServer();
            return Ok(responce);
        }


        /// <summary>
        /// Контроллер для фильтрации устройста
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<ServerData, Server>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("filter")]
        public IActionResult filter(ServerFilterModel filter, int number, int size)
        {
            var result = _serverService.readFilter(filter, number, size);
            return Ok(result);
        }
    }
}
