using LinqToTwitter;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReqFrndApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FriendRequest> FriendRequest { get; set; }
    public DbSet<Users> Users { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // Configure the relationships if needed
    //    modelBuilder.Entity<FriendRequest>()
    //        .HasOne(f => f.User1)
    //        .WithMany(u => u.Friendsh)
    //        .HasForeignKey(f => f.UserId1)
    //        .OnDelete(DeleteBehavior.Restrict);

    //    modelBuilder.Entity<Friendship>()
    //        .HasOne(f => f.User2)
    //        .WithMany()
    //        .HasForeignKey(f => f.UserId2)
    //        .OnDelete(DeleteBehavior.Restrict);
    //}
}