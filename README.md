# OneApp
OneApp is a framework built using ASP.net/C# for backend  and Angular2 for Frontend.
Front end is based mainly on ng2-admin Angular 2 template(https://github.com/akveo/ng2-admin). we extended it to add support for authentication(register with email confirmation - login - reset password using email code - change password - logout)
The backend of authentication supports SQLserver + MongoDB + Mock data repositories, and we are looking forward to supporting LDAP soon.

The application is divided into three layers
  - Data access layer
  - Web services(business) layer 
  - UI(frontend) layer

# Techinical Notes
----------------------------
- We use Ninject library (http://www.ninject.org/) to manage dependency injection in the backend. 
- We use Automapper library (http://automapper.org/) to manage mapping between database objects and data transfer objects (DTOs).
- For logging, we use log4net library. 
- For sql server queries, we are using EF code first, with migrations, 
- we are using MongDb driver for dealing with MonogDB.
- We use Repository, and UnitOfWork patterns to access data stores. Currently, it's fully applied to OneApp.Modules.Styles project, and other projects will be refactored to use the same patterns.
 
# Demo url
http://aligebily-001-site1.dtempurl.com

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
- Currently, you can register, login, logout, change password, reset password. When you register or reset password, you will be asked for the email first and then application will send you an email with code that you provide in next step of registration of resetting password. 
- We have implemented a feature that enables user to style the frontend. These styles, global or user level, can be updated from side menu in styles section
- global styles are styles that affects all users of the system, while user's styles are only affecting current user. Global styles should be available only to admin users(will be implemented later).
- The landing page in this application does not have accurate data or design.
- All data shown in portal is mocked at client, except for authentication and styles data, that can mocked at server or retrieved from database based on configuration set on web.config in OneApp.StartUp project
- For sending emails, you have to configure appsettings related to smtp server and sender email. you can hit this url(http://localhost:55475/api/textEncryption/Encrypt?clearText=yourpassword) to get the encoded password and set it in you mail settings. the "EmailFromEncodedPassword" key in web.config is not valid, set your email and your own password encoded using previous mentioned url. 
- Unit and integration tests are not implemented yet.




