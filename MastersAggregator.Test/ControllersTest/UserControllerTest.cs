using MastersAggregatorService.Controllers;
using MastersAggregatorService.Models;
using MastersAggregatorService.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace MastersAggregator.Test.ControllersTest;

[TestFixture(TestOf = typeof(UserController))]
public class UserControllerTest
{
    [Test]
    public void GetUserOkResultTest()
    {
        var repository = Substitute.For<UserRepository>();
        repository.GetById(15).Returns(StaticData.TestUser1);
        var controller = new UserController(repository);
        var result = controller.GetUser(15);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
    
    [Test]
    public void GetUserBadResultTest()
    {
        var repository = Substitute.For<UserRepository>();
        repository.GetById(0).ReturnsNullForAnyArgs();
        var controller = new UserController(repository);
        var result = controller.GetUser(76);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void GetUsersOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>(); 
        repository.GetAll().Returns(StaticData.Users); 
        var controller = new UserController(repository); 
        // Act
        var resultGetUsers = controller.GetUsers();  
        var expectedGetUsers = StaticData.Users;
        // Assert
        Assert.That((resultGetUsers as ObjectResult).StatusCode, Is.EqualTo(200));
        Assert.That((resultGetUsers as ObjectResult).Value, Is.EqualTo(expectedGetUsers));
    }

    [Test]
    public void DeleteUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>();
        repository.GetById(15).Returns(StaticData.TestUser1); 
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.DeleteUser(15); 
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public void DeleteUserBadResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>();
        repository.GetById(15).Returns(StaticData.TestUser1);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.DeleteUser(150);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400)); 
    }

    [Test]
    public void CreateUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>();
        repository.GetAll().Returns(StaticData.Users);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.CreateUser(StaticData.TestUser1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public void CreateUserBadResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>();
        repository.GetAll().Returns(StaticData.Users);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.CreateUser(StaticData.TestUser2);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }

    [Test]
    public void UpdateUserOkResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>(); 
        repository.GetAll().Returns(StaticData.Users);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.UpdateUser(StaticData.TestUser2);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(204));
    }

    [Test]
    public void UpdateUserBadResultTest()
    {
        // Arrange
        var repository = Substitute.For<UserRepository>(); 
        repository.GetAll().Returns(StaticData.Users);
        var controller = new UserController(repository);
        // Act
        var resultDeleteUser = controller.UpdateUser(StaticData.TestUser1);
        // Assert
        Assert.That((resultDeleteUser as StatusCodeResult).StatusCode, Is.EqualTo(400));
    }
 
}


static class StaticData
{
    //TestUser1 - уникальный юзер нет в Users
    public static User TestUser1 = new User { Id = 15, UserName = "Vadim", UserFirstName = "Sidor", UserPfone = "+745-34-34-153" };
    //TestUser2 - есть в списке Users
    public static User TestUser2 = new User { Id = 1, UserName = "Sasha", UserFirstName = "Smelov", UserPfone = "+745-88-11-222" };

    public static List<User> Users = new List<User>
    {
        new User { Id = 0, UserName = "Andrey", UserFirstName = "Sidorov", UserPfone = "+745-34-34-153" },
        new User { Id = 1, UserName = "Sasha",  UserFirstName = "Smelov",  UserPfone = "+745-88-11-222" }
    };

}
