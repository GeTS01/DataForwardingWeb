using Domain;
using Domain.Enum;
using DTO;
using DTO.Data;
using DTO.Filter;
using DTO.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using ErrorResponse = Amazon.Runtime.Internal.ErrorResponse;


namespace DataForwardingWeb.Controllers
{
    public class NetworkController : BaseController
    {
        private readonly NetworkService _networkService;

        public NetworkController(NetworkService networkService)
        {
            _networkService = networkService;
        }

        /// <summary>
        /// Контроллер для создания сущности network(cеть)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(NetworkData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] NetworkModel request)
        {
            var network = await _networkService.create(request);
            return Ok(network);
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
            _networkService.delete(id);
        }


        /// <summary>
        /// Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(NetworkData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read([FromQuery] long id)
        {
            var responce = _networkService.read(id);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<NetworkData, Network>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage(int page, int size)
        {
            var responce = _networkService.read(page, size);
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
            _networkService.remove(id);
        }

        /// <summary>
        /// Контроллер для обновления записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(NetworkData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPatch("update_by_id")]
        public IActionResult updateInId(long id, [FromQuery] NetworkModel request)
        {
            var responce = _networkService.updateInId(id, request);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств созданных в заданном диапазоне времени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<NetworkData>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("read_by_interval")]
        public IActionResult readByInterval(string? networkName, NetworkType networkType, ProtoсolType protoсolType)
        {
            var responce = _networkService.GetByName(networkName, networkType, protoсolType);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Network>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_all")]
        public IActionResult getAll()
        {
            var responce = _networkService.GetAllNetwork();
            return Ok(responce);
        }


        /// <summary>
        /// Контроллер для фильтрации устройста
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<NetworkData, Network>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("filter")]
        public IActionResult filter(NetworkFilterModel filter, int number, int size)
        {
            var result = _networkService.readFilter(filter, number, size);
            return Ok(result);
        }
    }
}
