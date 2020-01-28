using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Models;

namespace UsersRestApi.Data
{
  public static class DbInitializer
  {
    public static void Initialize(UsersContext context)
    {
      context.Database.EnsureCreated();

      if(context.Users.Any())
      {
        return;
      }

      List<User> users = GetSeedUsers();
      foreach (var user in users)
      {
        context.Users.Add(user);
      }
      context.SaveChanges();
    }

    private static List<User> GetSeedUsers()
    {
      List<User> users = new List<User>()
      {
        new User(){ Firstname = "John", Lastname = "Doe", Address = "1234 Millrock Dr", City = "Salt Lake City", State = "UT", Zipcode = "84121", Age = 25, Interests = "Skiing, Golf" },
        new User(){ Firstname = "Winter", Lastname = "Kabel", Address = "876 130th Rd", City = "Ponca City", State = "OK", Zipcode = "74601", Age = 33, Interests = "Photography, Sky diving" },
        new User(){ Firstname = "Daron", Lastname = "Wu", Address = "1816 Hickory Ct Ft", City = "Augusta", State = "GA", Zipcode = "30905", Age = 55, Interests = "Pickle ball, Painting"},
        new User(){ Firstname = "Josh", Lastname = "Burford", Address = "111 Zingaro Dr", City = "Roxana", State = "IL", Zipcode = "62084", Age = 18, Interests = "Hiking, Video Games"},
        new User(){ Firstname = "Samara", Lastname = "Cutsforth", Address = "2665 Trenley Ct", City = "Simi Valley", State = "CA", Zipcode = "93063", Age = 63, Interests = "Tennis, Books" }
      };

      return users;
    }
  }
}
