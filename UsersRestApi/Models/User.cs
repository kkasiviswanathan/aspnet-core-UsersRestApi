using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsersRestApi.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Firstname { get; set; }

    [Required]
    public string Lastname { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zipcode { get; set; }

    [Required]
    public int Age { get; set; }

    public string Interests { get; set; }
  }
}
