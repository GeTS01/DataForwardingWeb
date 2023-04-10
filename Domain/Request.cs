using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table(name: "request", Schema = "public")]
    public class Request : PersistentObject
    {
        //Идентификатор сообщения
        [Column("message_id")]
        public string? MessageId { get; set; }

        //Идентификатор цепочки сообщений
        [Column("correlation_id")]
        public string? CorrelationId { get; set; }

        //Идентификатор сервиса
        [Column("service_id")]
        public string? ServiceId { get; set; }

        //Идентификатор маршрута
        [Column("route_id")]
        public string? RouteId { get; set; }

        //Идентификатор сессии ШЭП
        [Column("session_id")]
        public string? SessionId { get; set; }

        //Идентификатор отправителя
        [Column("sender_id")]
        public string? SenderId { get; set; }

        //Пароль отправителя
        [Column("password")]
        public string Password { get; set; }

        //Код статуса
        [Column("code")]
        public string Code { get; set; }

        //Сообщение статуса
        [Column("date")]
        public string Date { get; set; }

        [Column("requestStatusType")]
        public RequestStatusType RequestStatusType { get; set; }

        [Column("measureType")]
        public MeasureType MeasureType { get; set; }

        [Column("deviceType")]
        public DeviceType DeviceType { get; set; }

        [Column("operationType")]
        public NitOperationType NitOperationType { get; set; }

        [Column("deviceName")]
        public string DeviceName { get; set; }

        [Column("tag_id")]
        public long TagId { get; set; }

        [Column("averageValue")]
        public double AverageValue { get; set; }

        [Column("mass_flowbegin")]
        public double Massflowbegin { get; set; }
        [Column("massflowend")]
        public double Massflowend { get; set; }
        [Column("mass")]
        public string Mass { get; set; }
        public Request() { }
    }
}
