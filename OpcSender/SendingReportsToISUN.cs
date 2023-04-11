using DataForwardingWeb.Domain.Enum;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Helper;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Request = Domain.Request;

namespace OpcSender
{
    public class SendingReportsToISUN : BackgroundService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagValueRepository _tagValueRepository;
        private readonly IRequestRepository _requestRepository;
        private Timer timer;

        public SendingReportsToISUN(

            IDeviceRepository deviceRepository,
            ITagRepository tagRepository,
            ITagValueRepository tagValueRepository,
            IRequestRepository requestRepository,
            IServiceScopeFactory serviceScopeFactory)
        {
            _deviceRepository = deviceRepository;
            _tagRepository = tagRepository;
            _tagValueRepository = tagValueRepository;
            _requestRepository = requestRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(async (t) => await GetRequestOnDays(), null, TimeSpan.Zero, TimeSpan.FromDays(1));
        }

        public static Binding GetBinding2()
        {

            // The security mode is set to Message.
            BasicHttpsBinding binding = new BasicHttpsBinding();
            binding.Security.Mode = BasicHttpsSecurityMode.TransportWithMessageCredential;
            return binding;
        }

        private async Task GetRequestOnDays()
        {
            string bindingType = "https://176.98.225.244";
            EndpointAddress endpointAddress = new EndpointAddress(bindingType);

            var binding = GetBinding2();
            ServiceReference1.ISyncChannel syncChannel = new ServiceReference1.SyncChannelClient(binding, endpointAddress);
            _ = syncChannel.SendMessageAsync(new ServiceReference1.SendMessageRequest()
            {
                SendMessage = new ServiceReference1.SendMessage()
                {
                    request = new ServiceReference1.SyncSendMessageRequest()
                    {
                        requestData = new ServiceReference1.RequestData()
                        {
                            data = new ServiceReference1.RequestData()
                        },
                        requestInfo = new ServiceReference1.SyncMessageInfo()
                        {
                            messageDate = DateTime.Now,
                            correlationId="11111",
                            Code = 200,
                            messageId = "1544911984184",
                            sender = new ServiceReference1.SenderInfo()
                            {
                                password = "112199299597871992E.",
                                senderId = "Zharas"
                            },
                            routeId = "159",
                            serviceId = "ISUN_Service2",
                            sessionId = "9b569176-73a2-4168-aab2-ObcObaee0314",
                            Password = "112199299597871992E.",
                        }
                    }
                }
            });

            List<Request> requests = new List<Request>();

            var devices = await _deviceRepository
                .GetAll()
                .ToListAsync();

            foreach (var device in devices)
            {
                var tags = await _tagRepository
               .GetAll()
               .Where(tag => tag.Device.Equals(device))
               .ToListAsync();
                foreach (var tag in tags)
                {
                    var list = _tagValueRepository
                   .GetAll().Include(x => x.Tag)
                   .Where(tv => tv.Tag.Equals(tag) && tv.Date.Date.Equals(DateTime.UtcNow.Date))
                   .ToList();

                    var tagValues = list
                   .Average(x => x.Value);
                    var request = new Request();

                    if (tag.Name == "MassaNakopl")
                    {
                        var value = _tagValueRepository.GetAll()
                          .Where(tv => tv.Tag.Equals(tag) && tv.Date.Date.Equals(DateTime.UtcNow.Date))
                          .OrderBy(x => x.Date)
                          .Select(x => x.Value);

                        request = new Request()
                        {

                            Date = DateTime.UtcNow.ToString(),
                            RequestStatusType = RequestStatusType.NEW,
                            MeasureType = tag.MeasureType,
                            DeviceName = device.Name,
                            NitOperationType = device.NitOperationType,
                            DeviceType = device.Type,
                            TagId = tag.Id,
                            AverageValue = tagValues,
                            Massflowbegin = value.FirstOrDefault(),
                            Massflowend = value.LastOrDefault(),
                            Mass = (value.LastOrDefault() - value.FirstOrDefault()).ToString()
                        };
                    }
                    else
                    {
                        request = new Request()
                        {

                            Date = DateTime.UtcNow.ToString(),
                            RequestStatusType = RequestStatusType.NEW,
                            MeasureType = tag.MeasureType,
                            DeviceName = device.Name,
                            NitOperationType = device.NitOperationType,
                            DeviceType = device.Type,
                            TagId = tag.Id,
                            AverageValue = tagValues,
                        };
                    }


                    requests.Add(request);
                    await _requestRepository.CreateAsync(request);
                }
            }
            await _requestRepository.SaveChangesAsync();

            var htmlResult = HTMLHelper.ToHtmlTable(requests);
            string path = @"C:\Users\Pavel\Desktop\HtmlRequests\RequestAt_" + DateTime.Now.Second + DateTime.Now.Millisecond + ".html";
            File.WriteAllText(path, htmlResult);
        }
    }
}