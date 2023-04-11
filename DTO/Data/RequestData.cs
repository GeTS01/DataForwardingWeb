using DataForwardingWeb.Domain.Enum;
using Domain;
using DTO;
using DTO.Data;

namespace DataForwardingWeb.DTO.Data
{
    public class RequestData : Data<Request>
    {
        public long Id { get; set; }
        public string? MassageId { get; set; }
        public string? CorrelationId { get; set; }
        public string? ServiceId { get; set; }
        public string? RouteId { get; set; }
        public string? Sessionid { get; set; }
        public string? SenderId { get; set; }
        public string Password { get; set; }
        public string? Code { get; set; }
        public long TagId { get; set; }
        public double AverageValue { get; set; }
        public RequestStatusType RequestStatusType { get; set; }
        public MeasureType MeasureType { get; set; }
        public DeviceType DeviceType { get; set; }
        public NitOperationType NitOperationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public RequestData(Request request) : base(request)
        {
            Id = request.Id;
            MassageId = request.MessageId;
            CorrelationId = request.CorrelationId;
            ServiceId = request.ServiceId;
            RouteId = request.RouteId;
            Sessionid = request.SessionId;
            SenderId = request.SenderId;
            Password = request.Password;
            Code = request.Code;
            TagId = request.TagId;
            AverageValue = request.AverageValue;
            RequestStatusType = request.RequestStatusType;
            MeasureType = request.MeasureType;
            DeviceType = request.DeviceType;
            NitOperationType = request.NitOperationType;
            CreatedAt = request.CreatedAt;
            UpdatedAt = request.UpdatedAt;
        }
    }
}