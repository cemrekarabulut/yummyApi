using Microsoft.EntityFrameworkCore;
using YummyApi.entities;

namespace YummyApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Chef> Chefs => Set<Chef>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Feature> Features => Set<Feature>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    }
}