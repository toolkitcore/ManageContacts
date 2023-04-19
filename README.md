# Project Manage Contacts Web API Backend using ASP.NET Core 6.0

## Features
- Authentication and Authorization
- Manage Users
- Manage Contacts

## Running the Project

## Technologies Used

- SQL
- OAuth2
- Entity Framework Core
- Linq
- FluentValidator
- ASP.NET Core 6
- JWT Token

## Database

### Users
    | Column Name  | Data Type     | Description                    |
    |--------------|---------------|--------------------------------|
    | Id           | Guid          | Primary key of the Users table |
    | Username     | nvarchar(100) |                                |
    | Password     | nvarchar(max) |                                |
    | Email        | nvarchar(100) |                                |
    | Fullname     | nvarchar(100) |                                |
    | Image        | nvarchar(max) |                                |
    | CreatedTime  | datetime      |                                |
    | CreatorId    | guid          |                                |
    | ModifiedTime | datetime      |                                |
    | ModifierId   | guid          |                                |
    | Deleted      | bit           |                                |

### Roles
    | Column Name  | Data Type     | Description                    |
    |--------------|---------------|--------------------------------|
    | Id           | Guid          | Primary key of the Roles table |
    | Name         | nvarchar(100) |                                |
    | Description  | nvarchar(max) |                                |
    | CreatedTime  | datetime      |                                |
    | CreatorId    | guid          |                                |
    | ModifiedTime | datetime      |                                |
    | ModifierId   | guid          |                                |
    | Deleted      | bit           |                                |

### UserRole
    | Column Name  | Data Type | Description                              |
    |--------------|-----------|------------------------------------------|
    | RoleId       | Guid      | Foreign key reference to the Roles table |
    | UserId       | Guid      | Foreign key reference to the Users table |


### Contacts Table

    | Column Name  | Data Type     | Description                       |
    |--------------|---------------|-----------------------------------|
    | Id           | Guid          | Primary key of the Contacts table |
    | FirstName    | nvarchar(100) |                                   |
    | LastName     | nvarchar(100) |                                   |
    | FullName     | nvarchar(100) |                                   |
    | Email        | nvarchar(100) |                                   |
    | Company      | nvarchar(100) |                                   |
    | JobTitle     | nvarchar(100) |                                   |
    | CreatedTime  | datetime      |                                   |
    | CreatorId    | guid          |                                   |
    | ModifiedTime | datetime      |                                   |
    | ModifierId   | guid          |                                   |
    | Deleted      | bit           |                                   |


### ContactsDetail Table

    | Column Name  | Data Type     | Description                                 |
    |--------------|---------------|---------------------------------------------|
    | Id           | guid          | Primary key of the ContactsDetail table     |
    | ContactId    | guid          | Foreign key reference to the Contacts table |
    | Email        | nvarchar(100) | Email address of the contact                |
    | Phone        | nvarchar(20)  | Phone of the contact                        |
    | Image        | nvarchar(max) | Image of the contact                        |
    | Etag         | nvarchar(255) | ETag value from Google API response         |
    | Address      | nvarchar(255) | Address of the contact                      |
    | City         | nvarchar(255) | City of the contact                         |
    | State        | nvarchar(255) | State of the contact                        |
    | ZipCode      | nvarchar(255) | Zip code of the contact                     |
    | Country      | nvarchar(255) | Country of the contact                      |
    | CreatedDate  | datetime      | Date when the record is created             |
    | ModifiedDate | datetime      | Date when the record is last modified       |

### Groups Table
    | Column Name | Data Type     | Description                    |
    |-------------|---------------|--------------------------------|
    | Id          | int           | Primary key of the Group table |
    | Name        | nvarchar(100) | Name of the group              |
    | Description | nvarchar(100) | Description of the group       |

### GroupContacts Table
    | Column Name | Data Type | Description                                 |
    |-------------|-----------|---------------------------------------------|
    | ContactId   | guid      | Foreign key reference to the Contacts table |
    | GroupId     | guid      | Foreign key reference to the Groups table   |

## Main function
### Users
    1. Create, Update, Delete, Get
    2. Send Mail To Group Contacts, Send Mail To Contact

### Roles
    1. Create
    2. Update
    3. Delete
    4. Get

### Contacts
    1. Create
        + Input (FirstName, LastName, FullName, Email, ...) 
        + Use Google's OAuth2 to access the Google Contacts API and get one's contact information based on email address and save the Contact Details
    2. Update Contact
    3. Update Contact Details
        + Use the Etag of the Contact Details table to check if the resource has changed since the previous query. 
        The etag received in the previous response can be sent to check if the resource has been updated and, if so,
        download the latest version of the resource. 
        + Dùng Etag của bảng Contact Details để kiểm tra xem liệu tài nguyên đã thay đổi kể từ lần truy vấn trước đó hay chưa.  Có thể gửi etag đã nhận được trong phản hồi trước đó để kiểm tra xem tài nguyên đã được cập nhật hay chưa, và nếu có, tải xuống phiên bản mới nhất của tài nguyên đó.
    4. Delete Contacts

### Groups
    1. Create
    2. Update
    3. Delete
    4. Get



