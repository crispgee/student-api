Student Management API

A simple RESTful API built with .NET 6+ and Dapper ORM for managing student records in a PostgreSQL database.
The API allows you to create, read, update, and delete students, following REST best practices and proper HTTP status codes.

Prerequisites
Download Micrsoft .NET Freamework
Download PostgreSQL 
Install Dapper and Npgsql

Setup your database[
CREATE TABLE Students(
   Id Serial PRIMARY KEY,
   FirstNmae VARCHAR(100) NOT NULL,
   LastNmae VARCHAR(100) NOT NULL,
   DateOfBirth DATE NOT NULL,
   Age INT NOT NULL,
   CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
)]

Setup Instructions

1. Clone the repository
git clone
https://github.com/crispgee/student-api.git
cd student-api

3. Configure database connection

Edit appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=database_name;Username=postgres;Password=yourpassword"
  }
}
3. Run the API
dotnet run

4. Test via Swagger- Test your api endpoints with swagger

Visit:

http://localhost:5000/swagger




API Endpoints

All endpoints and their responses and error handling

1. Create a Student
   Assumption: User must enter age manually and will not be automatically calculated

POST /api/students

Request Body:
{
  "firstName": "John",
  "lastName": "Doe",
  "dateOfBirth": "2000-05-10",
  "age": 24
}

Responses:
Code	Description	Example
201 Created	Student created successfully	{ "id": 1, "firstName": "John", "lastName": "Doe", ... }
400 Bad Request	Missing or invalid data	

2. Get All Students

GET /api/students

Responses:
Code	Description	Example
200 OK	Returns a list of students	[ { "id": 1, "firstName": "John", ... }, ... ]
204 No Content	No students found	(Empty response)

3. Get a Student by ID

GET /api/students/{id}

Example:

GET /api/students/1

Responses:
Code	Description	Example
200 OK	Student found	{ "id": 1, "firstName": "John", ... }
404 Not Found	No student with given ID	{ "error": "Student not found" }

4. Update Student

PUT /api/students/{id}

Request Body:
{
  "firstName": "John",
  "lastName": "Smith",
  "dateOfBirth": "2000-05-10",
  "age": 25
}

Responses:
Code	Description	Example
200 OK	Updated successfully	{ "message": "Student updated" }
400 Bad Request	Invalid input	
404 Not Found	No matching record	{ "error": "Student not found" }

5. Delete Student

DELETE /api/students/{id}

Example:

DELETE /api/students/1

Responses:
Code	Description	Example
200 OK	Deleted successfully	{ "message": "Student deleted" }
404 Not Found	No record found	{ "error": "Student not found" }



To run with docker

Edit appsettings.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=host.docker.internal;Port=5432;Database=database_name;Username=postgres;Password=yourpassword"
  }
}

run the following commands  
docker build -t student-api .
docker run -d -p 5000:5000 student-api
