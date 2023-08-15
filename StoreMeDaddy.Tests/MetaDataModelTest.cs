using StoreMeDaddy.Models;
namespace StoreMeDaddy.Tests;


[TestClass]
public class MetaDataModelTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNameCannotBeNull()
    {
        _ = new MetaDataModel(null, "", "path", 100, "type", true, false, null, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPathCannotBeNull()
    {
        _ = new MetaDataModel("name", "", null, 100, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTypeCannotBeNull()
    {
        _ = new MetaDataModel("name", "", "path", 100, null, true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreatedByUserCannotBeNull()
    {
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", null, "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestHashCannotBeNull()
    {
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidSize()
    {
        _ = new MetaDataModel("name", "", "path", -1, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidHash()
    {
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", "invalid-hash");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidVersion()
    {
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, null, "user", "0123456789012345678901234567890123456789012345678901234567");
    }

    [TestMethod]
    public void TestValidMetaData()
    {
        // Console.WriteLine("Testing Valid MetaData");
        string name = "name";
        string description = "";
        string path = "path";
        long size = 100L;
        bool isPublic = true;
        bool isDeleted = false;
        string version = "1";
        string type = "type";
        string createdByUser = "user";
        string hash = "1111111111111111111111111111111111111111111111111111111111111111";

        MetaDataModel metaData = new(name, description, path, size, type, isPublic, isDeleted, version, createdByUser, hash);
        DateTime timeCreated = metaData.CreatedAt;
        Console.WriteLine($"Time created: {timeCreated}");
        Assert.AreEqual(name, metaData.FileName);
        Assert.AreEqual(path, metaData.Path);
        Assert.AreEqual(size, metaData.Size);
        Assert.AreEqual(type, metaData.Type);
        Assert.AreEqual(isPublic, metaData.IsPublic);
        Assert.AreEqual(isDeleted, metaData.IsDeleted);
        Assert.AreEqual(version, metaData.Version);
        Assert.AreEqual(createdByUser, metaData.CreatedByUser);
        Assert.AreEqual(timeCreated, metaData.CreatedAt);
        Assert.AreEqual(timeCreated, metaData.ModifiedAt);
        Assert.AreEqual(timeCreated, metaData.AccessedAt);
        Assert.AreEqual(createdByUser, metaData.CreatedByUser);
        Assert.AreEqual(hash, metaData.Hash);
    }
}
