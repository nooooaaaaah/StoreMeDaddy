using System.Security.Cryptography;
using System.Xml.Serialization;
using StoreMeDaddy.Models;
namespace StoreMeDaddy.Tests;


[TestClass]
public class MetaDataModelTests
{
    readonly byte[] salt = new byte[16];
    private readonly Aes _aes = Aes.Create();

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestNameCannotBeNull()
    {
        _aes.GenerateIV();
        // RandomNumberGenerator.Fill(salt);
        _ = new MetaDataModel(null, "", "path", 100, "type", true, false, null, "user", "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestPathCannotBeNull()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", null, 100, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestTypeCannotBeNull()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, null, true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestCreatedByUserCannotBeNull()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", null, "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestHashCannotBeNull()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", null, salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidSize()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", -1, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidHash()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", "invalid-hash", salt, _aes.IV);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestInvalidVersion()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, null, "user", "0123456789012345678901234567890123456789012345678901234567", salt, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestNullSalt()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567", null, _aes.IV);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestNullIV()
    {
        _aes.GenerateIV();
        _ = new MetaDataModel("name", "", "path", 100, "type", true, false, "1", "user", "0123456789012345678901234567890123456789012345678901234567", salt, null);
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

        _aes.GenerateIV();
        MetaDataModel metaData = new(name, description, path, size, type, isPublic, isDeleted, version, createdByUser, hash, salt, _aes.IV);
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
