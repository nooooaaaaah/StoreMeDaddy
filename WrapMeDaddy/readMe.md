# WrapMeDaddy: A library for StoreMeDaddy

Helps you integrate the StoreMeDaddy API into your application.

## Features

- Authentication
- User Management
- CRUD Management

## Getting Started

Install the Package from Nuget

```bash
Install-Package WrapMeDaddy
```

or

```bash
dotnet add package WrapMeDaddy
```

Start up your instance of the StorMeDaddy server and get your API key.

- For help installing and starting up your instance of StoreMeDaddy, refer to the [StoreMeDaddy](<https://github.com/nooooaaaaah/StoreMeDaddy/blob/main/readMe.md>) readMe.

## Introduction

### Authentication

Retrieve your Api key either using the StuffIt GUI or CLI.

```bash
stuffit getApiKey "<username>" "<password>"
```

In your program.cs file, initialize the StoryMeDaddy service:

```csharp
using WrapMeDaddy;

// ...other services
builder.Services.AddWrapMeDaddy(<Your API Key>)
```

### User Management

```csharp
using WrapMeDaddy;

// Use the default admin account
var user = AuthMeDaddy("admin", "admin");

// Creates a new user and returns the user object
var user = CreateWmdUser("Username", "Password", "Email@email.com");

// Authorize a user
var user = AuthMeDaddy("Username", "Password");

// Edit a user
user.Username = "newUsername";
user.Email = "newEmail@test.com";
user.Password = "newPassword";

// Delete a user
await user.Delete(); 

// Create an administrator account
// Once created admin accounts can't be changed and only deleted through the StuffIt GUI or CLI
var admin = CreateWmdAdmin("Admin", "Password", "Email@admin.com");
```

### CRUD Management

Depending on use case you can either create a user or use the default admin account

```csharp
using WrapMeDaddy;

// Upload a file
var metaData = new WmdMetaDataDTO
{
    Name = "File Name",
    Description = "File Description",
    Tags = new List<string> { "tag1", "tag2" },
    Public = true,
};

byte[] fileContents = File.ReadAllBytes("C:/path/to/file");

var wmdFile = new WmdFileDTO
{
    Contents = fileContents,
    MetaData = metaData,
};

user.UploadEnsureSuccess(wmdFile);

// Retrieve metaData for a uploaded file by Name or Id
WmdMetaData metaData = user.GetFile("File Name");
WmdMetaData metaData = user.GetFile(1);

// Retrieve metaData for all files
List<WmdMetaData> metaDataList = user.GetFile(); 

// Retrieve a file with contents by Name or file Id
WmdFile wmdFile = user.Download("File Name");
WmdFile wmdFile = user.Download(metaData.Id);
byte[] fileContents = wmdFile.Contents(); 
string path = "C:/path/to/save/file";
File.WriteAllBytes(path ,fileContents);

// Reupload a file
byte[] newFileContents = File.ReadAllBytes("C:/path/to/new/file");
user.Reupload(metaData.Id, newFileContents);

// Edit the metaData for a file
metaData.Edit("New Name", "New Description", new List<string> { "newTag1", "newTag2" }, false); 

// Change a Files name
metaData.EditName("New Name"); 

// Change a Files description
metaData.EditDescription("New Description");

// Change a Files visibility
metaData.EditPublic(false); 

// Add tags to a file
metaData.AddTags(new List<string> { "newTag1", "newTag2" });

// Remove tags from a file
metaData.RemoveTags(new List<string> { "newTag1", "newTag2" });

// Delete a file
DeleteFile(metaData.Id);

```
