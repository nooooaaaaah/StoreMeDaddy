# StuffIt CLI: Like the GUI but cooler cause its in the Terminal

Encapsulates the WrapMeDaddy API into a CLI

- StuffIt is a CLI that is integrated with StoreMeDaddy file bucket
- Provides a simple and easy to use interface for managing your StoreMeDaddy instance

## Features

- User Directory
- Server Activity
- Basic File Management
- Server Settings

## Getting Started

The Default port for stuffit is 7277

- To configure a custom port edit the Docker file

Authorize your self with the default admin credentials

```bash
stuffit authME -u admin -p daddy
```

**Note:** It is recommended to change the default credentials upon initial setup

From there you can explore the features included in StuffIt CLI

## Commands

authME: Authorize yourself with the server

whoAmI: Get the current user

listUsers: List all users

addUser: Add a user

removeUser: Remove a user

listFiles: List all files

addFile: Add a file

removeFile: Remove a file

listActivity: List all activity

listSettings: List all settings

updateSettings: Update a setting

## Examples

```bash
stuffit addUser -u <username> -p <password>
```

```bash
stuffit removeUser -u <username>
```

```bash
stuffit addFile -f <file path>
```

*Add more examples here...*
