using Domain;

namespace DataForwardingWeb.DTO.Filter
{
    public class RequestFilterModel : FilterModel<Request>
    {
        public string? MessageId { get; set; }
        public string? CorrelationId { get; set; }
        public TagValueFilterModel? TagValue { get; set; }
        public string? ServiceId { get; set; }
        public string? RouteId { get; set; }
        public string? SessionId { get; set; }
        public string? SenderId { get; set; }
        public RequestFilterModel() { }
    }
}
