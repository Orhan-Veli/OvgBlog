# OvgBlog
> With this project you can make your blog with ASP.NET Core.

## Table of contents
* [General info](#general-info)
* [Screenshots](#screenshots)
* [Technologies](#technologies)
* [Setup](#setup)
* [Links](#link)
* [Contact](#contact)

## General info
 This project i used ASP.NET Core 3.1 and Mssql server database. If you want to make this project on your own you should follow this steps for the setup;

* You need to select Class Library .NET Core then ASP.NET Core Web Application.
* Select empty page file. Then you need to create layers for dataaccess, business and Web Api.

### Dal

* Add usefull frameworks to Dal for access to database with DbFirst.
* Create entities file inside the Dal layer and create your tables in it.
* Create a class inside a Dal and name it OvgBlogContext. Inside a class you should follow steps on the link entityframework/Dbfirst page.
* When you followed to steps you should make enables the migrations for the Dal layer. Then you need to add migrations and update database.
* Check your database for the see if there is an error or something else.
* When you followed the steps you must see the migrations file on your Dal layer. If you can see it, you finished the database connection.
* Create abstract and concrete file in Dal.
* Add BaseEntity and IEntityRepository class inside an abstract file. Remember entities classes must be inherit BaseEntity.
* IEntityRepository class methods must be reach database and make crud operations.
* EntityRepository must be get inheritance from IEntityRepository we should make methods to do its job.
* Add constant file inside a Dal layer.
* This file must be hold our messages etc.

### Business
* Configure business project reference to reach Dal.
* Create files Abstract,Constants Services, Utilities.
* Inside this files you need to write Services for entities. Dont forget to inherit Interfaces.

### UI
* First of all you need to configure project reference for two of the layer.
* Add these packages Fluent Validation, Mapster, Mapster.Core,toastr,mailkit.
* Setup your startup configuration. Links below can help you.
* Create these files Extentions,Controllers, Models, Pages, Validation.
* Inside Models you need to create your properties to show client datas.
* When you want show client a data, you need to use Models which to show which to not. Mapster is used for this.
* Login class is checks you verify your id and password.
* You can reach the data with controllers.

## Screenshots
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/AdminMain.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/ArticlePage.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/CategoryPage.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/CommentTable.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/ContactTable.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/TagPage.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/Database.PNG" width="600" />
<img src="https://github.com/Orhan-Veli/OvgBlog/blob/master/Img/LoginPage.PNG" width="600" />


## Technologies
* ASP.NET Core 3.1
* EntityFrameworkCore 3.1
* Mapster 7.1
* Fluent Validation 9.5
* Mssql 18
* Mailkit
* Toastr
* Mailkit
* Summernote


## Links
* [DataFirst](https://www.entityframeworktutorial.net/Data-first/what-is-code-first.aspx)
* [Mapster](https://www.nuget.org/packages/Mapster/)
* [Fluent Validation](https://docs.fluentvalidation.net/en/latest/aspnet.html)
* [Toastr](https://github.com/CodeSeven/toastr)
* [Mailkit](https://github.com/jstedfast/MailKit)
* [Summernote](https://summernote.org)

## Setup
* Download the repository.
* Create Database.
* Configure inside the OvgBlog.Dal/OvgBlogContext.cs connection string server and database name. 
* Inside the Package Manager Console write update-database
* Build and run.

## Contact
Created by [@OrhanVeli](https://github.com/Orhan-Veli)
