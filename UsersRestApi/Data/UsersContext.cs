using Microsoft.EntityFrameworkCore;
using System;
using UsersRestApi.Models;

namespace UsersRestApi.Data
{
  public class UsersContext: DbContext
  {
    public UsersContext(DbContextOptions<UsersContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("Users");
    }
  }
}
