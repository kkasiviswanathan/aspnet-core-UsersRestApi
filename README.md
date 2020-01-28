# aspnet-core-UsersRestApi
A Asp.Net Core REST API application to perform CRUD operations on a Users list.

## What .Net framework is required?
* .Net Core Ver 2.2

## What happens when project is built?
* The project uses EntityFramework code first to create UserRestApi database in the localDb and seed 5 users to the Users table.

* Opens a browser window and displays the Swagger documentation for the REST endpoints.

* Users can then use the Swagger interface to make REST endpoint calls to Create/Read/Search by name/Update & Delete users.

## REST Endpoints
* /api/users/ (GET) - Returns all the users in the database

* /api/users/2 (GET) - Returns user with Id value 2

* /api/users/GetByName?name=jo (GET) - Returns users whose Firstname or Lastname contains the name passed as parameter

* /api/users/ (POST) - Adds a user to the database

* /api/users/2 (PUT) - Updates user with Id value 2 with the user object passed in the request body

* /api/users/2 (DELETE) - Deletes a user with Id value 2

## Extras
* Implements latency simulation for requests in development environment.

* There is a powershell script along with the solution file that can add a user, search user by name and also delete a user.
