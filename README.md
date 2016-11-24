# OneApp
OneApp is a framework built using ASP.net/C# for backend  and Angular2 for Frontend.
Front end is based mainly on ng2-admin Angular 2 template(https://github.com/akveo/ng2-admin). we currenly extended it to add support for authentication(register with email confirmation - login - reset password using email code - change password )
The backend of authentication supports SQLserver + MongoDB + Mock data repositories, and we are looking forward to supporting LDAP soon.

The application is divided into three layers
  - Data access layer
  - Web services(business) layer 
  - UI(frontend) layer

We use Ninject library (http://www.ninject.org/) to manage dependency injection in the backend. And we also use Automapper library (http://automapper.org/) to manage mapping between database objects and Data transfer objects (DTO).
For logging, we use log4net library. 
For sql server queries, we are using EF code first, while we are using MongDb driver for dealing with MonogDB.
 
# Demo url
http://aligebily-001-site1.dtempurl.com/frontend/index.html 

# To run the project
---------------
- Tools [git, nodejs, Visual studio 2015, visual studio code]
- open git bush, then hit this command
	- git clone https://github.com/Ali-Gebily/OneApp
- open backend/OneApp.sln using VS2015
- Right click on solution and click "Restore Nuget packages"
- Run OneApp.StartUp project, then copy the base url as it will used in frontend as the service url, for example: 

- open frontend folder(using visual studio code or any other tool), and open this file "src\app\common\oneAppProxy\services\oneAppConfiguration.service.ts" then set BackEndServicePath to the url you copied when you ran the server.

- open nodejs.command prompt
- navigate to frontend folder, and hit
	- npm install 
	- npm start
- after you see that server is ready in comand prompt, hit localhost:3000 in browser and start using portal.
- Note: you can check ng2-admin(https://github.com/akveo/ng2-admin) for more details about running front end portal. 


# Notes
- you can register, login, logout, change password, reset password. When you register or reset password, you will be asked for the email first and then application will send you an email with code that you provide in next step of registration of resetting password. 
- The landing page in this application does not have accurate data or design.
- All data shown in portal is mocked at client, except for authentication data that can mocked at server or retrieved from database based on configuration set on web.config in OneApp.StartUp project
- for sending emails, you have to configure appsettings related to smtp server and sender email.
- Unit and integration tests are not implemented yet.


