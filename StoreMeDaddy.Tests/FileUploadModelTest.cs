using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StoreMeDaddy.Classes;
using StoreMeDaddy.Models;
using System;
using System.Security.Claims;
using System.Text;

namespace StoreMeDaddy.Tests
{
    [TestClass]
    public class FileUploadModelTests
    {
        [TestMethod]
        public void Constructor_ValidInput_CreatesObjectWithExpectedProperties()
        {
            // Arrange
            var mockFile = new Mock<IFormFile>();
            var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes("Dummy file content"));
            mockFile.Setup(f => f.FileName).Returns("file.txt");
            mockFile.Setup(f => f.ContentType).Returns("text/plain");
            mockFile.Setup(f => f.Length).Returns(sourceStream.Length);
            mockFile.Setup(m => m.OpenReadStream()).Returns(sourceStream);
            mockFile.Setup(f => f.Name).Returns("file");

            string fileName = "file.txt";
            string description = "description";
            int? folderId = 1;
            bool isPublic = true;
            DateTime? expirationDate = DateTime.Now;
            string version = "1.0";
            int ownerId = 123;

            // Act
            FileUploadModel model = new(mockFile.Object, fileName, description, folderId, isPublic, expirationDate, version, ownerId);

            // Assert
            Assert.AreEqual(mockFile.Object, model.File);
            Assert.AreEqual(fileName, model.FileName);
            Assert.AreEqual(description, model.Description);
            Assert.AreEqual(folderId, model.FolderId);
            Assert.AreEqual(isPublic, model.IsPublic);
            Assert.AreEqual(expirationDate, model.ExpirationDate);
            Assert.AreEqual(version, model.Version);
            Assert.AreEqual(ownerId, model.OwnerId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFile_ThrowsException()
        {
            // Arrange
            // ...

            // Act
            _ = new FileUploadModel(null, "file.txt", "description", 1, true, DateTime.Now, "1.0", 123);

            // Assert - Expect exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullFileName_ThrowsException()
        {
            // Arrange
            FormFile file = new(null, 0, 0, "file", "file.txt");

            // Act
            _ = new FileUploadModel(file, null, "description", 1, true, DateTime.Now, "1.0", 123);

            // Assert - Expect exception
        }

        // Removed the Hash field from upload model
        // [TestMethod]
        // [ExpectedException(typeof(ArgumentNullException))]
        // public void Constructor_NullHash_ThrowsException()
        // {
        //     // Arrange
        //     FormFile file = new(null, 0, 0, "file", "file.txt");

        //     // Act
        //     _ = new FileUploadModel(file, "file.txt", "description", 1, true, DateTime.Now, "1.0", null, 123);

        //     // Assert - Expect exception
        // }
        // [TestMethod]
        // public async Task FileEncryption_Decryption_Success()
        // {
        //     // Arrange
        //     IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("File Content")), 0, 11, "Data", "file.txt");
        //     string fileName = "file.txt";
        //     string description = "description";
        //     int? folderId = 1;
        //     bool isPublic = true;
        //     DateTime? expirationDate = DateTime.Now;
        //     string version = "1.0";
        //     string hash = "hash";
        //     int ownerId = 123;

        //     var principal = new ClaimsPrincipal();
        //     // ... (add necessary claims to the principal)

        //     var context = new StoreMeDaddyContext();
        //     var uploadPath = "YourUploadPathHere"; // specify your upload path here
        //     var service = new FileUploadService(context, uploadPath);

        //     FileUploadModel uploadModel = new(file, fileName, description, folderId, isPublic, expirationDate, version, hash, ownerId);

        //     // Act
        //     var metaDataModel = await service.UploadFileAsync(uploadModel, principal);

        //     // After uploading, attempt to read back and decrypt the file (assuming you have a method in service to do this)
        //     var decryptedContent = await service.ReadFileContentAsync(metaDataModel); // This method needs to be implemented in your service

        //     var originalContent = Encoding.UTF8.GetBytes(new StreamReader(file.OpenReadStream()).ReadToEnd());

        //     // Assert
        //     Assert.AreEqual("File Content", decryptedContent);
        //     Assert.AreEqual(originalContent, decryptedContent);
        // }
    }
}
