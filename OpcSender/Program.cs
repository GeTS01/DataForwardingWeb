using DataForwardingWeb.Repository.Repositores;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using DataForwardingWeb.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Storage;
using OpcSender;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=DataForwardingDB;UserId=postgres;Password=123456"), ServiceLifetime.Singleton);

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IDeviceRepository, DeviceRepository>();
        services.AddSingleton<IRequestRepository, RequestRepository>();
        services.AddSingleton<ITagRepository, TagRepository>();
        services.AddSingleton<ITagValueRepository, TagValueRepository>();

        services.AddSingleton<UserService>();
        services.AddSingleton<DeviceService>();
        services.AddSingleton<RequestService>();
        services.AddSingleton<TagValuesService>();
        services.AddHostedService<SendingReportsToISUN>();
    })
    .Build();

await host.RunAsync();