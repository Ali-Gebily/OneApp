# OneApp
OneApp is a framework built using ASP.net/C# for backend  and Angular2 for Frontend.
Front end is based mainly on ng2-admin Angular 2 template(https://github.com/akveo/ng2-admin). we currenly extended it to add support for authentication(register with email confirmation - login - reset password using email code - change password )
The backend of authentication supports SQLserver + MongoDB + Mock data repositories, and we are looking forward to supporting LDAP soon.

The application is divided into three layers
  - Data access layer
  - Web services(business) layer 
  - UI layer(front end) layer

Application uses Ninject(http://www.ninject.org/) to manage dependency injection in the backend.
Application uses Automapper(http://automapper.org/) to manage mapping between database objects and Data transfer objects (DTO).

 
# Demo url
http://aligebily-001-site1.dtempurl.com/frontend/index.html
