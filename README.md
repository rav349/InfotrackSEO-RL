# InfotrackSEO-RL

Web API Setup
- Please use CMD and CD into this folder InfoTrackSEO\InfoTrackSEO.Domain
- Then run dotnet ef --startup-project ../InfoTrackSEO/ database update
- This should set up the database for use and can run it from Visual Studio/Rider

UI
- Please use CMD and CD into this folder InfoTrackSEO\ui\
- run npm install
- run ng serve --o
- This should start the UI side and open a browser with the site open


Design
- This is a .NET 7 Web API solution that has been split into 3 layers - API, Application and Domain. I have made use of CQRS, MediatR, Repository Pattern, Dependency Injection

- API takes care of the controllers and has DTOs defined for incoming request and outgoing responses.

- Application takes care of the business logic 

- Domain contains the models used and the Database config

- Swagger URL: https://localhost:44317/swagger/index.html

- I have created the UI in Angular and made use of bootstrap to style the forms and tables
