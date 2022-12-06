
# DotNet 6 Clean Architecture MVC Solution

This .NET project `Template` demonstrates concept of separation of concerns (SOC) and is a simple implementation of the Clean Architecture. When installed and used to create a new project, all references to ```Template``` will be replaced with the name of your project.

## Core Project

The Core project contains all domain entities and service layer interfaces and has no dependencies.

Password hashing functionality added via the ```Template.Core.Security.Hasher``` class. This is used in the Data project UserService to hash the user password before storing in database.

## Data Project

The Data project encapsulates all data related concerns. It provides an implementation of ```Template.Core.Services.IUserService``` using EntityFramework to handle data storage/retrieval. It defaults to using Sqlite for portability across platforms.

The Service is the only element exposed from this project and consumers of this project simply need reference it to access its functionalty.

## Test Project

The Test project references the Core and Data projects and should implement unit tests to test any service implementations created in the Data project. A template test is provided for implementation of IUserService and the tests should be extended to fully exercise the functionality of your Service.

## Web Project

The Web project uses the MVC pattern to implement a web application. It references the Core and Data projects and uses the exposed services and models to access data management functionality. This allows the Web project to be completely independent of the persistence framework used in the Data project.

### Dependency Injection

The DbContext and IUserService are added to the dependency injection container on startup. The DbContext can be configured in ```Program.cs``` to use either ```Sqlite``` (default), ```MySql```, ```Postres``` or ```SqlServer``` databases. Connection strings for each database should be configured in ```appsettings.json``` with XXX replaced with your particular settings.

```json
"ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\mssqllocaldb;Database=XXX;Trusted_Connection=True;",
    "MySql":     "server=localhost; port=3306; database=XXX; user=XXX; password=XXX",
    "Postgres":  "host=localhost; port=5432; database=XXX; username=XXX; password=XXX",
    "Sqlite":    "Filename=data.db"
  },
```

### Identity

The project provides extension methods to enable:

1. User Identity using cookie authentication is enabled without using the boilerplate template used in the standard web projects (mvc,web). This allows the developer to gain a better appreciation of how Identity is implemented. The core project implements a User model and the data project UserService implementation provides user management functionality such as Authenticate, Register, Change Password, Update Profile etc.

The Web project implements a UserController with actions for Login/Register/NotAuthorized/NotAuthenticated etc. The ```AuthBuilder``` helper class defined in ```Template.Web.Helpers``` provides a ```BuildClaimsPrinciple``` method to build a set of user claims for User Login action when using cookie authentication and this can be modified to amend the claims added to the cookie.

To enable cookie Authentication the following statement is included in Program.cs.

```c#
builder.Services.AddCookieAuthentication();
```

Then Authentication/Authorisation are then turned on in the Application via the following statements in Program.cs

```c#
app.UseAuthentication();
app.UseAuthorization();
```

### Additional Functionality

1. Any Controller that inherits from the Web project BaseController, can utilise:

    a. The Alert functionality. Alerts can be used to display alert messages following controller actions. Review the UserController for an example using alerts.

    ```Alert("The User Was Registered Successfully", AlertType.info);```

2. A ClaimsPrincipal authentication extension method
    * ```User.GetSignedInUserId()``` - returns Id of current logged in user or 0 if not logged in

3. Two custom TagHelpers are included that provide

    a. Authentication and authorisation Tags

    * ```<p asp-authorized>Only displayed if the user is authenticated</p>```

    * ```<p asp-roles="admin,manager">Only displayed if the user has one of specified roles</p>```

    Note: to enable these tag helpers Program.cs needs following service added to DI container
    ```builder.Services.AddHttpContextAccessor();```

    b. Conditional Display Tag

    * ```<p asp-condtion="@some_boolean_expression">Only displayed if the condition is true</p>```

## Install Template

To install this solution as a template (template name is **termonclean**)

1. Download current version of the template

    ```$ git clone https://github.com/termon/DotNetTemplate.git```

2. Install the template so it can be used by ```dotnet new``` command. Use the path (i.e the directory location)to the cloned template directory without trailing '/'

Linux/macOS

```$ dotnet new -i /path/DotNetTemplate```

Windows

```c: dotnet new -i c:\path\DotNetTemplate```

3. Once installed you can create a new project using this template

    ```dotnet new termonclean -o SolutionName```