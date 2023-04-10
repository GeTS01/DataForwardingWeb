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
    public class OrganizationController : BaseController
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        /// Контроллер для создания сущности organization(организация)
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(OrganizationData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPost("create")]
        public IActionResult Create([FromQuery] OrganizationModel request)
        {
            var organization = _organizationService.create(request);
            return Ok(organization);
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
            _organizationService.delete(id);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(OrganizationData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_id")]
        public IActionResult Read([FromQuery] long id)
        {
            var responce = _organizationService.read(id);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для нахождения записи в базе в определенном интервале
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<OrganizationData, Organization>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_by_page")]
        public IActionResult ReadPage(int page, int size)
        {
            var responce = _organizationService.read(page, size);
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
            _organizationService.remove(id);
        }

        /// <summary>
        /// Контроллер для обновления записи в базе
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(OrganizationData), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpPatch("update_by_id")]
        public IActionResult updateInId(long id, [FromQuery] OrganizationModel request)
        {
            var responce = _organizationService.updateInId(id, request);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств по имени
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<OrganizationData>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("read_by_interval")]
        public IActionResult readByInterval(string? organizationName, OrganizationType organizationType)
        {
            var responce = _organizationService.GetByName( organizationName, organizationType);
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для получения всех устройств
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(List<Organization>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("get_all")]
        public IActionResult getAll()
        {
            var responce = _organizationService.GetAllOrganization();
            return Ok(responce);
        }

        /// <summary>
        /// Контроллер для фильтрации устройста
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(Page<OrganizationData, Organization>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        [HttpGet("filter")]
        public IActionResult filter(OrganizationFilterModel filter, int number, int size)
        {
            var result = _organizationService.readFilter(filter, number, size);
            return Ok(result);
        }
    }
}
