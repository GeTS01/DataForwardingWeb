using DataForwardingWeb.Repository.Repositores;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using DataForwardingWeb.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Repository.Repositores;
using Repository.Repositores.Interfaces;
using Service.Implementation;
using Storage;


var builder = WebApplication.CreateBuilder(args);

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=DataForwardingDB;UserId=postgres;Password=123456");
builder.Services.AddScoped(s => new AppDbContext(optionsBuilder.Options));

//builder.Services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql("Server=localhost;Port=5432;Database=DataForwardingdb;UserId=postgres;Password=123456"), ServiceLifetime.Scoped);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagValueRepository, TagValueRepository>();
builder.Services.AddScoped<INetworkRepository, NetworkRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IServerRepository, ServerRepository>();

// Configure the HTTP request pipeline.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<RequestService>();
builder.Services.AddScoped<TagValuesService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NetworkService>();
builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<ServerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());
app.Run();

