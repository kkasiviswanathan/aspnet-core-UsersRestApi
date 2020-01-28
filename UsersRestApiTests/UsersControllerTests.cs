using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UsersRestApi.Controllers;
using UsersRestApi.Models;
using UsersRestApiTests.Services;
using Xunit;

namespace UsersRestApiTests
{
  public class UsersControllerTests
  {
    private UsersController _controller;
    private UsersServiceFake _service;

    public UsersControllerTests()
    {
      _service = new UsersServiceFake();
      _controller = new UsersController(_service);
    }

    #region Get

    [Fact]
    public void Get_Called_ReturnsOkResult()
    {
      var result = _controller.Get();
      Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void Get_Called_ReturnAllUsers()
    {
      var result = _controller.Get().Result as OkObjectResult;

      var users = Assert.IsType<List<User>>(result.Value);
      Assert.Equal(5, users.Count);
    }

    #endregion Get

    #region GetById

    [Fact]
    public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
    {
      var notFoundResult = _controller.Get(2500);
      Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    [Fact]
    public void GetById_ExistingIdPassed_ReturnsOkResult()
    {
      var okResult = _controller.Get(2);
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetById_ExistingIdPassed_ReturnsRightUser()
    {
      int testId = 2;
      var okResult = _controller.Get(testId).Result as OkObjectResult;

      Assert.IsType<User>(okResult.Value);
      Assert.Equal(testId, (okResult.Value as User).Id);
      Assert.Equal("Winter", (okResult.Value as User).Firstname);
    }

    #endregion GetById

    #region GetByName

    [Fact]
    public void GetByName_UnknownNamePassed_ReturnsNotFoundResult()
    {
      var notFoundResult = _controller.GetByName("Kobe");
      Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    [Fact]
    public void GetByName_ExistingNamePassed_ReturnsOkResult()
    {
      string testUserName = "Lebron";

      var okResult = _controller.GetByName(testUserName);
      Assert.IsType<OkObjectResult>(okResult.Result);
    }

    [Fact]
    public void GetByName_FirstNamePassed_ReturnsRightUser()
    {
      string testUserName = "Lebron";
      var okResult = _controller.GetByName(testUserName).Result as OkObjectResult;

      Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
      Assert.Equal(testUserName, (okResult.Value as IEnumerable<User>).First().Firstname);
    }

    [Fact]
    public void GetByName_LastNamePassed_ReturnsRightUser()
    {
      string testUserName = "wu";
      var okResult = _controller.GetByName(testUserName).Result as OkObjectResult;

      Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
      Assert.Equal(testUserName, (okResult.Value as IEnumerable<User>).First().Lastname, true);
    }

    [Fact]
    public void GetByName_PartialNamePassed_ReturnsRightUsers()
    {
      string testPartialName = "jo";
      var okResult = _controller.GetByName(testPartialName).Result as OkObjectResult;

      Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
      Assert.All(okResult.Value as IEnumerable<User>, (item) => Assert.Contains(testPartialName, item.Firstname, StringComparison.CurrentCultureIgnoreCase));
    }

    [Fact]
    public void GetByName_UnknownPartialNamePassed_ReturnsNotFoundResult()
    {
      string testPartialName = "zha";
      var notFoundResult = _controller.GetByName(testPartialName);

      Assert.IsType<NotFoundResult>(notFoundResult.Result);
    }

    #endregion GetByName

    #region AddUser

    [Fact]
    public void Add_InvalidObjectPassed_ReturnsBadRequest()
    {
      var nameMissingUser = new User()
      {
        Firstname = "Dwayne",
        Address = "567 Elk Grove Dr"
      };
      _controller.ModelState.AddModelError("Lastname", "Required");
      _controller.ModelState.AddModelError("Age", "Required");

      var badResponse = _controller.Post(nameMissingUser);

      Assert.IsType<BadRequestObjectResult>(badResponse);
    }


    [Fact]
    public void Add_ValidObjectPassed_ReturnsCreatedResponse()
    {
      User testUser = new User()
      {
        Firstname = "Dwayne",
        Lastname = "Wade",
        Address = "567 Elk Grove Dr",
        City = "Salt Lake City",
        State = "UT",
        Zipcode = "84011",
        Age = 24,
        Interests = "Soccer, Biking"
      };

      var createdResponse = _controller.Post(testUser);

      Assert.IsType<CreatedAtActionResult>(createdResponse);
    }


    [Fact]
    public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
    {
      User testUser = new User()
      {
        Firstname = "Dwayne",
        Lastname = "Wade",
        Age = 24,
      };

      var createdResponse = _controller.Post(testUser) as CreatedAtActionResult;
      var createdUser = createdResponse.Value as User;

      Assert.IsType<User>(createdUser);
      Assert.Equal("Dwayne", createdUser.Firstname);
    }

    #endregion AddUser

    #region DeleteUser

    [Fact]
    public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
    {
      int notExistingId = 250;
      var badResponse = _controller.Delete(notExistingId);
      Assert.IsType<NotFoundResult>(badResponse);
    }

    [Fact]
    public void Delete_ExistingIdPassed_ReturnsOkResult()
    {
      int existingId = 3;
      var okResponse = _controller.Delete(existingId);
      Assert.IsType<OkResult>(okResponse);
    }

    [Fact]
    public void Delete_ExistingIdPassed_RemovesOneUser()
    {
      int existingId = 3;
      var okResponse = _controller.Delete(existingId);
      Assert.Equal(4, _service.GetAllUsers().Count());
    }

    #endregion DeleteUser

    #region UpdateUser

    [Fact]
    public void Update_NotExistingIdPassed_ReturnsNotFoundResponse()
    {
      int notExistingId = 250;

      User testUser = new User()
      {
        Id = 250,
        Firstname = "Dwayne",
        Lastname = "Wade",
        Address = "567 Elk Grove Dr",
        City = "Salt Lake City",
        State = "UT",
        Zipcode = "84011",
        Age = 24,
        Interests = "Soccer, Biking"
      };

      var badResponse = _controller.Put(notExistingId, testUser) as ActionResult<User>;
      Assert.IsType<NotFoundResult>(badResponse.Result);
    }

    [Fact]
    public void Update_ExistingIdPassed_ReturnsBadRequest()
    {
      int existingId = 3;
      User testUser = new User()
      {
        Id = 3,
        Firstname = "Lebron",
        Lastname = "Wu"
      };

      _controller.ModelState.AddModelError("Age", "Required");

      var badResponse = _controller.Put(existingId, testUser) as ActionResult<User>;
      Assert.IsType<BadRequestObjectResult>(badResponse.Result);
    }

    [Fact]
    public void Update_ExistingIdPassed_ReturnsOkResult()
    {
      int existingId = 3;
      User testUser = new User()
      {
        Id = 3,
        Firstname = "Lebron",
        Lastname = "Wu",
        Address = "1816 Hickory Ct Ft",
        City = "Sandy",
        State = "GA",
        Zipcode = "30905",
        Age = 55,
        Interests = "Pickle ball, Painting"
      };
      var okResponse = _controller.Put(existingId, testUser).Result as OkObjectResult;
      Assert.IsType<User>(okResponse.Value);
    }

    [Fact]
    public void Update_ExistingIdPassed_ReturnsUpdatedUser()
    {
      int existingId = 3;
      User testUser = new User()
      {
        Id = 3,
        Firstname = "Lebron",
        Lastname = "Wu",
        Address = "1816 Hickory Ct Ft",
        City = "Sandy",
        State = "GA",
        Zipcode = "30905",
        Age = 55,
        Interests = "Pickle ball, Painting"
      };
      var okResponse = _controller.Put(existingId, testUser).Result as OkObjectResult;
      var modifiedUser = okResponse.Value as User;
      Assert.Equal("Sandy", modifiedUser.City);
    }

    #endregion UpdateUser
  }
}
