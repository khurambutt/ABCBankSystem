# ABCBankSystem
Web application.
Practical Implementation Development Steps and Fetures of ABcBankSystem.....

----------- Framework And Technologies -------------------

Technologies and Framework used in ABCBankSystem.

-----> Visual Studio 2019 Community Edition
-----> MS .Net Framework Core 3.1
-----> ASP.NET MVC Core
-----> SQL Server
-----> Used SQL Server windows authentication for SQL Server Login
-----> SQL Server Management Studio

----------- Created ASP.NET MVC Project Template---------------

-----> Created with Https enables 
-----> Authentication type input, select Individual User Accounts. ---
--------------> it added the ASP.NET Core Identity features in the project 

------ Nuget Packages ---------

Packages installed through Nuget.

-----> Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore Version=3.1.20
-----> Microsoft.AspNetCore.Identity.EntityFrameworkCore Version=3.1.20"
-----> Microsoft.AspNetCore.Identity.UI" Version=3.1.20
-----> Microsoft.EntityFrameworkCore.SqlServer Version=3.1.20
-----> Microsoft.EntityFrameworkCore.Tools Version=3.1.20

-------------- In Package manager console the command which i use is -----------------------------

-----> dotnet ef migrations add initial -- use for database migrations
-----> dotnet ef database update

-----> this command creatd Databse in SQLServer 
-----> and tables for ASP.NET identity in the database
-----> select the Views folder and right click and Select Add --> New Scaffold Item
-----> In the Scaffold Dialog Box, Select the Identity Option and Click on Ok Button
-----> Used built in Scafolding ui for login and register
-----> Security ---? 

-----> EF code first approach --

-------------------- Areas ------------------------------

----> Add Admin area in Areas folder 
---->  t generated some folders for controllers , models, data and views and scafolding created some configurations.
----> it also created a ScafoldingReamme.txt  file at root.
and include xml code for adding folders.

------------- Admin Login ------------------------------
Create admin user with the following Credentials.

user name: admin@abcgroup.com
password: Admin123!

--------------------------
