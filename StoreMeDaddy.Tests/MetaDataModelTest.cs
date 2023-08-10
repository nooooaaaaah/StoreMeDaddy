using StoreMeDaddy.Models;
namespace StoreMeDaddy.Tests;


[TestClass]
public class MetaDataModelTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNameCannotBeNull()
    {
        _ = new MetaDataModel(null, "path", 100, "type", DateTime.Now, DateTime.Now, DateTime.Now, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPathCannotBeNull()
    {
        _ = new MetaDataModel("name", null, 100, "type", DateTime.Now, DateTime.Now, DateTime.Now, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTypeCannotBeNull()
    {
        _ = new MetaDataModel("name", "path", 100, null, DateTime.Now, DateTime.Now, DateTime.Now, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreatedByUserCannotBeNull()
    {
        _ = new MetaDataModel("name", "path", 100, "type", DateTime.Now, DateTime.Now, DateTime.Now, null, "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestHashCannotBeNull()
    {
        _ = new MetaDataModel("name", "path", 100, "type", DateTime.Now, DateTime.Now, DateTime.Now, "user", null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidSize()
    {
        _ = new MetaDataModel("name", "path", -1, "type", DateTime.Now, DateTime.Now, DateTime.Now, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidHash()
    {
        _ = new MetaDataModel("name", "path", 100, "type", DateTime.Now, DateTime.Now, DateTime.Now, "user", "invalid-hash");
    }

    [TestMethod]
    public void TestValidMetaData()
    {
        Console.WriteLine("Testing Valid MetaData");
        DateTime now = DateTime.Now;
        string name = "name";
        string path = "path";
        long size = 100L;
        string type = "type";
        string createdByUser = "user";
        string hash = "1111111111111111111111111111111111111111111111111111111111111111";

        MetaDataModel metaData = new(name, path, size, type, now, now, now, createdByUser, hash);

        Assert.AreEqual(name, metaData.Name);
        Assert.AreEqual(path, metaData.Path);
        Assert.AreEqual(size, metaData.Size);
        Assert.AreEqual(type, metaData.Type);
        Assert.AreEqual(now, metaData.CreatedAt);
        Assert.AreEqual(now, metaData.ModifiedAt);
        Assert.AreEqual(now, metaData.AccessedAt);
        Assert.AreEqual(createdByUser, metaData.CreatedByUser);
        Assert.AreEqual(hash, metaData.Hash);
    }
}

