using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UsersRestApi.Models;
using UsersRestApi.Services;

namespace UsersRestApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
      _service = service;
    }

    [Description("Gets all the users in the system")]
    // GET api/users
    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
      return Ok(_service.GetAllUsers());
    }

    [Description("Gets a user by the user Id")]
    // GET api/users/5
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
      bool userExists = _service.CheckIfUserExists(id);
      if (!userExists)
        return NotFound();

      return Ok(_service.GetUserById(id));
    }

    // GET api/users/GetByName?name=jo
    [Description("Gets all users whose name contains the search term")]
    [HttpGet("[action]")]
    public ActionResult<IEnumerable<User>> GetByName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      var foundUsers = _service.GetUsersByName(name);

      if (foundUsers != null && foundUsers.Count() > 0)
        return Ok(foundUsers);
      else
        return NotFound();
    }

    [Description("Adds a new user")]
    // POST api/users
    [HttpPost]
    public ActionResult Post([FromBody] User value)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (value == null)
        throw new ArgumentNullException(nameof(value));

      bool userExists = _service.CheckIfUserExists(value.Id);
      if (userExists)
        throw new ArgumentException($"User with Id: ${value.Id} already exists!");

      var newUser = _service.AddUser(value);
      return CreatedAtAction("Get", new { id = newUser.Id }, newUser);
    }

    [Description("Updates a user")]
    // PUT api/users/5
    [HttpPut("{id}")]
    public ActionResult<User> Put(int id, [FromBody] User value)
    {
      bool userExists = _service.CheckIfUserExists(id);

      if (!userExists)
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var modifiedUser = _service.UpdateUser(id, value);
      return Ok(modifiedUser);
    }

    [Description("Deletes a user")]
    // DELETE api/users/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
      bool userExists = _service.CheckIfUserExists(id);

      if (!userExists)
      {
        return NotFound();
      }
      _service.RemoveUser(id);
      return Ok();
    }
  }
}
