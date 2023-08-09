using StoreMeDaddy.Models;
namespace StoreMeDaddy.Tests;

[TestClass]
public class UserModelTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUserNameCannotBeNull()
    {
        _ = new UserModel(null, "p@ssword");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPasswordCannotBeNull()
    {
        _ = new UserModel("XXXXXXXX", null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestUserNameCannotBeEmpty()
    {
        _ = new UserModel("", "p@ssword");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestPasswordCannotBeEmpty()
    {
        _ = new UserModel("XXXXXXXX", "");
    }

    [TestMethod]
    public void TestUserCanBeCreated()
    {
        UserModel user = new("XXXXXXXX", "p@ssword");
        Assert.IsNotNull(user);
    }
}