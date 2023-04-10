using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Storage
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Request> Requests { get; set; }

        public DbSet<Network> Networks { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagValue> TagValues { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=DataForwardingDB;UserId=postgres;Password=123456");
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }
        public new IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
    }
}
