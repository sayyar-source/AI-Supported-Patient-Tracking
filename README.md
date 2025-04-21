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

