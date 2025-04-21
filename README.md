# AI-Supported Patient Tracking Platform (Lite)

# **Overview**

The **AI-Supported Patient Tracking Platform (Lite)** is a streamlined clinical monitoring system designed for managing patient records, viewing histories, and integrating AI-driven predictions. Built with a modern tech stack, it emphasizes security, scalability, and clean architecture.

# **Purpose**

**The system provides:**

- Secure user authentication and authorization.
  
- Basic patient management (Create, Read, Update, Delete).
- Historical patient record display.
- AI-supported predictions via a mock API.

# **Technologies Used**

**Frontend**

- **Angular 19:** For a reactive, component-based UI.

- **Reactive Forms:** For efficient form handling and validation.

- **HttpClient:** For secure API communication.

- **JWT Authentication:** For protecting routes and API requests.

**Backend**

- .NET 9 (ASP.NET Core Web API): For a robust RESTful API.

- Entity Framework Core: For data management and ORM.

- JWT Authentication: For secure AAA (Authentication, Authorization, Accounting).

- Mock AI Prediction Endpoint: Returns static JSON for AI predictions.

**Database**

- **MSSQL:** Relational database for storing user and patient data.

**Features**

- **Swagger:** For API documentation.

- **Docker:** For containerized deployment.

**Functional**

1. **Login Page**

- Implements **JWT-based authentication**.

- Angular frontend communicates with the backend for login.

- Restricts unauthorized access to the patient page.



2. **Patient List Page**

- Displays patient data: Name, Surname, Birthdate.

- Includes View and Delete buttons for each patient.

- Features an Add New Patient button.



3. **Patient Detail Page**

- Shows a list of historical records (sample data).

- Displays a read-only Doctor's Remarks field.

- Includes an AI-supported prediction field (fetched from a mock API).



4. **Patient Creation Page**

- Simple form for Name, Surname, and Birthdate.

- Submits data via a POST request to the backend.

 # **Technical Support**

**Frontend**

- Built with **Angular 19+**.

- Uses **Reactive Forms** for form management.

- Leverages **HttpClient** for secure API calls.

- Secures routes with **JWT**.

 **Backend**

- Developed with **.NET 9+ (ASP.NET Core Web API)**.

- Manages data using **Entity Framework Core**.

- Supports **CRUD operations** for patients.

- Implements **JWT-based AAA** (Register, Sign-in, Sign-out).

- Provides a mock **/api/prediction** endpoint with static JSON.


**Database**
- Uses **MSSQL** as the RDBMS.

# **Setup Instructions**

**Prerequisites**

- **Node.js** (v20+): For Angular development.

- **.NET SDK (9+)**: For backend development.

- **MSSQL**: For the database.

- **Docker** (optional): For containerized deployment.

- **Git**: For version control.

**Installation**

1. **Clone the Repository**
   - git clone <repository-url>
   - cd AI-Supported-Patient-Tracking
2. **Frontend Setup**
   - cd frontend
   - npm install
   - ng serve
Access the app at http://localhost:4200.

3. **Backend Setup**
   - cd backend
   - dotnet restore
   - dotnet ef database update
   - dotnet run
The API runs at http://localhost:4000, http://localhost:5000, http://localhost:7000.

4. **Database Setup**

 - For MSSQL:

   - Update the connection string in appsettings.json.

   - Apply migrations: dotnet ef database update.
  
5. **Swagger**
   - Access API documentation at http://localhost:4000/swagger,  http://localhost:5000/swagger,  http://localhost:7000/swagger
   
6. **Docker** 
   - Run the application:
    - docker-compose up --build
   
# **API Endpoints**

 - **POST /api/auth/register**: Register a new user.

 - **POST /api/auth/signin**: Authenticate a user and return a JWT.

 - **POST /api/auth/signout**: Sign out a user.

 - **GET /api/patients**: List all patients.

 - **GET /api/patients/{id}**: Get a patient by ID.

 - **POST /api/patients**: Create a new patient.

 - **DELETE /api/patients/{id}**: Delete a patient.

 - **GET /prediction/api**: Mock AI prediction (static JSON).

# **Running the Application**

1. Start the backend (*dotnet run*).

2. Start the frontend (*ng serve*).

3. Navigate to http://localhost:4200.

4. Log in to access patient management features.

# **Evaluation Criteria**

 - **Code Quality**: Clean, modular, and well-documented.

 - **Architecture**: Effective integration of Angular and .NET Core.

 - **Principles**: Adherence to SOLID, DRY, and other best practices.

 - **Timeline**: Completion within the specified timeframe.
