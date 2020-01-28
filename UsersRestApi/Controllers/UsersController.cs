using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UsersRestApi.Models;
using UsersRestApi.Services;

namespace UsersRestApi.Controllers
{
  /// <summary>
  /// REST API to perform CRUD operation on User entity
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
      _service = service;
    }

    /// <summary>
    /// Gets all the users
    /// </summary>
    /// <returns>A List of User entities</returns>
    [Description("Gets all the users in the system")]
    // GET api/users
    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
      return Ok(_service.GetAllUsers());
    }

    /// <summary>
    /// Get a user whose Id matches the value passed in as parameter
    /// </summary>
    /// <param name="id">User Id of the user</param>
    /// <returns>If user is found, returns the user entity, else returns not found response</returns>
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

    /// <summary>
    /// Finds Users whose Firstname or Lastname contains the name passed in as parameter.
    /// It returns the user even if there is a partial match of name.
    /// </summary>
    /// <param name="name">Name to search for</param>
    /// <returns>List of users who has the name in Firstname or Lastname</returns>
    // GET api/users/GetByName?name=jo
    [Description("Gets all users whose name contains the search term")]
    [HttpGet("[action]")]
    public ActionResult<IEnumerable<User>> GetByName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        return BadRequest(new ArgumentNullException(nameof(name)));
      }

      var foundUsers = _service.GetUsersByName(name);

      if (foundUsers != null && foundUsers.Count() > 0)
        return Ok(foundUsers);
      else
        return NotFound();
    }

    /// <summary>
    /// Adds a new User
    /// </summary>
    /// <param name="value">User entity to be added</param>
    /// <returns>Returns the created user entity</returns>
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
        return BadRequest(new ArgumentNullException(nameof(value)));

      bool userExists = _service.CheckIfUserExists(value.Id);
      if (userExists)
        return BadRequest(new ArgumentException($"User with Id: ${value.Id} already exists!"));

      var newUser = _service.AddUser(value);
      return CreatedAtAction("Get", new { id = newUser.Id }, newUser);
    }

    /// <summary>
    /// Updates a user entity.
    /// </summary>
    /// <param name="id">Id of the user to update</param>
    /// <param name="value">User entity with updated values</param>
    /// <returns>Returns the updated user entity</returns>
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

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id">User Id of the user to delete</param>
    /// <returns>Ok result of user was deleted</returns>
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
