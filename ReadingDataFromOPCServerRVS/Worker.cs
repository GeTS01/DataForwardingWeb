using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Opc.UaFx;
using Opc.UaFx.Client;
using System.ComponentModel;

namespace WorkerOpcReaderRVS
{
    public class Worker : BackgroundService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagValueRepository _tagValueRepository;
        private Timer timer;
        private OpcClient client = new OpcClient("opc.tcp://192.168.88.1:55000");
        private readonly IHostApplicationLifetime _appLifetime;
        public Worker(
                      IDeviceRepository _deviceRepository,
                      ITagRepository _tagRepository,
                      ITagValueRepository _tagValueRepository,
                      IHostApplicationLifetime appLifetime
                      )
        {
            this._deviceRepository = _deviceRepository;
            this._tagRepository = _tagRepository;
            this._tagValueRepository = _tagValueRepository;
            this._appLifetime = appLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer = new Timer(async (t) => await ReadMetriks(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }
        private int _connectAttempts = 0;
        private async Task ReadMetriks()
        {

            var devices = await _deviceRepository
                .GetAll()
                .Where(x => x.Type == DataForwardingWeb.Domain.Enum.DeviceType.RVS)
                .ToListAsync();
            foreach (var device in devices)
            {
                var tags = await _tagRepository
                    .GetAll()
                    .Where(x => x.DeviceId == device.Id)
                    .ToListAsync();
                foreach (var tag in tags)
                {
                    try
                    {
                        client.Connect();
                        _connectAttempts = 0; // Сбросить количество неудачных попыток при успешном подключении
                        var a = $"{device.Name}.{tag.Name}";
                        OpcValue tagValue = client.ReadNode(a);
                        OpcValue dateValue = client.ReadNode($"{device.Name}.DateTime");
                        double? value = ReadValue(tagValue);
                        DateTime? measureDate = ReadDate(dateValue);
                        TagValue tagValueEntity = new TagValue()
                        {
                            CreatedAt = DateTime.UtcNow,
                            Tag = tag,
                            Date = measureDate ?? DateTime.UtcNow,
                            Value = value ?? 0
                        };
                        _tagValueRepository.Create(tagValueEntity);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occurred while reading data from OPC server: {ex.Message}");
                        _connectAttempts++;
                        if (_connectAttempts >= 3) // Если было три неудачных попытки подключения, перезапустить приложение
                        {
                            _appLifetime.StopApplication();
                            return;
                        }
                        await Task.Delay(TimeSpan.FromMinutes(1)); // Подождать 1 минуту перед повторной попыткой
                        continue;
                    }
                }


            }
            await _tagValueRepository.SaveChangesAsync();
            Console.WriteLine("Tag Values Saved");
        }
        private void Client2_Connected(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static double? ReadValue(OpcValue value)
        {
            if (value.DataType != OpcDataType.Double)
            {
                return Convert.ToDouble(value.Value);
            }
            return (double?)value.Value;

        }
        private static DateTime? ReadDate(OpcValue value)
        {
            if (value != null && value.DataType == OpcDataType.DateTime)
                return (DateTime)value.Value;
            return null;
        }
    }
}