using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoreMeDaddy.Classes;
using System;

namespace StoreMeDaddy.Tests
{
    [TestClass]
    public class FileUploadModelTests
    {
        [TestMethod]
        public void Constructor_ValidInput_CreatesObjectWithExpectedProperties()
        {
            // Arrange
            FormFile file = new(null, 0, 0, "file", "file.txt");
            string fileName = "file.txt";
            string description = "description";
            int? folderId = 1;
            bool isPublic = true;
            DateTime? expirationDate = DateTime.Now;
            string version = "1.0";
            string hash = "hash";
            int ownerId = 123;

            // Act
            FileUploadModel model = new(file, fileName, description, folderId, isPublic, expirationDate, version, hash, ownerId);

            // Assert
            Assert.AreEqual(file, model.File);
            Assert.AreEqual(fileName, model.FileName);
            Assert.AreEqual(description, model.Description);
            Assert.AreEqual(folderId, model.FolderId);
            Assert.AreEqual(isPublic, model.IsPublic);
            Assert.AreEqual(expirationDate, model.ExpirationDate);
            Assert.AreEqual(version, model.Version);
            Assert.AreEqual(hash, model.Hash);
            Assert.AreEqual(ownerId, model.OwnerId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFile_ThrowsException()
        {
            // Arrange
            // ...

            // Act
            _ = new FileUploadModel(null, "file.txt", "description", 1, true, DateTime.Now, "1.0", "hash", 123);

            // Assert - Expect exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFileName_ThrowsException()
        {
            // Arrange
            FormFile file = new(null, 0, 0, "file", "file.txt");

            // Act
            _ = new FileUploadModel(file, null, "description", 1, true, DateTime.Now, "1.0", "hash", 123);

            // Assert - Expect exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullHash_ThrowsException()
        {
            // Arrange
            FormFile file = new(null, 0, 0, "file", "file.txt");

            // Act
            _ = new FileUploadModel(file, "file.txt", "description", 1, true, DateTime.Now, "1.0", null, 123);

            // Assert - Expect exception
        }
    }
}
