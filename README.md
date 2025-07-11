# Driving & Vehicle License Department (DVLD) System

## Project Overview

> ⚠️ This is a simplified explanation and does not cover the full scope of the project. The actual system is larger and includes more features.

## Project Overview

This project is a comprehensive **Driving & Vehicle License Department (DVLD)** management system designed to automate the issuance, renewal, replacement, and suspension handling of driver licenses and related services. Built as a Windows Forms application, it follows a 3-Tier Architecture (Presentation, Business Logic, Data Access) and implements Object-Oriented Programming (OOP) principles.

## Technologies & Tools

* **Language & Framework**: C# with .NET Framework
* **UI Library**: Guna UI 2 for modern Windows Forms controls
* **Data Access**: ADO.NET
* **Database**: Microsoft SQL Server
* **Architecture**: 3-Tier (Presentation, Business Logic, Data Access)
* **Object-Oriented Programming (OOP)**: The project is built using Object-Oriented Programming (OOP) principles to ensure better structure, modularity, and code reusability.
* **Asynchronous Programming** (async/await)
* **Multithreading** (Task.Run for background data operations)
* **Event Logging via Windows Event Log**
* **Windows Registry for Remember Me feature**
* **Security Enhancements**:

  * SHA-256 Hashing with Salt for password storage (with `Salt` column added to Users table)
  * AES Symmetric Encryption for storing credentials in Windows Registry
  * Event Logging using Windows Event Log for error tracking and diagnostics
  * Windows Registry is used instead of plain text file for "Remember Me" credentials
  * Connection string is now stored and managed via `App.config`

* **Optimized Search in People List**:

   * Implemented real-time search with debounce mechanism to improve performance.
   * Prevents lag when typing in the search box, even with large data sets (100,000+ records).
   * Filtering is applied only after the user stops typing for a short period (e.g., 400ms).
* **Optmize Performance Data Loading**:

   * Uses `async` / `await` for non-blocking data access.
   * Utilizes `Task.Run` and multithreading to prevent UI freezing while loading large datasets.
   * Tested with more than 100,000 records in the People table.
---



## Key Features

1. **License Application & Management**

   * Issue new driver licenses (7 classes: Motorcycles, Cars, Commercial Vehicles, Agricultural Vehicles, Buses, Trucks)
   * Renew existing licenses
   * Replace lost or damaged licenses
   * Issue international driving permits
   * Release suspended licenses
2. **Application Request Workflow**

   * Create and track service requests with status: New, Cancelled, Completed
   * Ensure no duplicate or pending requests per user and per service
   * Calculate and record application fees (standard fee: \$5)
3. **User & Person Management**

   * CRUD for system users and roles
   * Secure user registration with salted password hashing (SHA-256)
   * Store usernames/passwords encrypted in Windows Registry (AES)
   * Prevent duplicate national IDs

4. **Testing & Validation**

   * Schedule and record vision, theoretical, and practical driving tests
   * Enforce age and previous-license checks per class
   * Record test results, allow retakes upon failure
5. **License Classes & Rules**

   * Maintain configurable license classes (age limit, validity, fees)
   * Automatic rejection for invalid age or duplicate license class
6. **Audit & Logging**

   * Record every operation with timestamp and responsible user
   * Log errors and system issues via Windows Event Log
7. **Search & Reporting**

   * Query licenses by national ID or license number
   * Filter requests by status, date range, and type


## Installation & Setup

1. **Clone the Repository**
2. **Restore the SQL Server Database**
3. **Configure the Project**

   * Open `DVLDPresentation.sln` in Visual Studio
   * Update the connection string in `App.config` (no hard-coded strings)
4. **Run the Application**

   * Login using:

     * Username: `Admin`
     * Password: `1234` (stored securely via hashing)

---


## Project Architecture

```
Presentation Layer (Windows Forms)  <--->  Business Logic Layer  <--->  Data Access Layer (ADO.NET)  <--->  SQL Server
```

* **Presentation Layer**: UI forms built with Guna UI 2 controls for all modules (Login, Dashboard, Applications, Tests, License Management, Admin).
* **Business Logic Layer**: Implements rules for application processing, test workflows, user permissions, and license validations.
* **Data Access Layer**: ADO.NET classes managing SQL commands, queries, and transactions.

## Usage

1. **Login**: Authenticate as Admin or Operator.
2. **Person Management**: Add, update, or search for individuals by national ID.
3. **Application Requests**: Create a new request, select service type, schedule tests, and collect fees.
4. **Testing Module**: Record results for vision, theoretical, and practical tests.
5. **License Issuance**: After passing tests, generate license with expiry date and class details.
6. **Renewals & Replacements**: Process renewals, lost, or damaged license requests.
7. **International Permits**: Issue or revoke international driving permits for valid class-3 license holders.
8. **Administration**: Manage system users, adjust fees, license class rules, and audit logs.

---
## Security Features Summary

| Feature                         | Description                                                       |
|----------------------------------|-------------------------------------------------------------------|
| Password Hashing                | SHA-256 + Salt (per user)                                        |
| Credentials Storage             | Encrypted using AES and saved in Windows Registry                |
| Error Logging                   | Written to Windows Event Log                                     |
| Connection String               | Managed via `App.config` for security and flexibility            |
| "Remember Me" Implementation    | Stores credentials in encrypted form in Registry (not plain text)|
