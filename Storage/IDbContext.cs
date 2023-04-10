using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Storage
{
    public interface IDbContext
    {
        DbSet<Device> Devices { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<TagValue> TagValues { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Network> Networks { get; set; }
        DbSet<Organization> Organizations { get; set; }
        DbSet<Server> Servers { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
