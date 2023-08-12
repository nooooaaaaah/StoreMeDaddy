using StoreMeDaddy.Models;
namespace StoreMeDaddy.Tests;

[TestClass]
public class UserModelTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUserNameCannotBeNull()
    {
        _ = new UserModel(null, "p@ssword", "user");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPasswordCannotBeNull()
    {
        _ = new UserModel("XXXXXXXX", null, "user");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestUserNameCannotBeEmpty()
    {
        _ = new UserModel("", "p@ssword", "user");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestPasswordCannotBeEmpty()
    {
        _ = new UserModel("XXXXXXXX", "", "user");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestUserContainsSpecialCharacter()
    {
        _ = new UserModel("XXX@XXXXX", "password", "user");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestPasswordMissingSpecialCharacter()
    {
        _ = new UserModel("XXXXXXXX", "password", "user");
    }

    [TestMethod]
    public void TestUserCanBeCreated()
    {
        UserModel user = new("XXXXXXXX", "p@ssword", "user");
        Assert.IsNotNull(user);
    }
}