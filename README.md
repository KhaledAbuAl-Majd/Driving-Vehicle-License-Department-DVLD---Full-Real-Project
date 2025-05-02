# Driving & Vehicle License Department (DVLD) System

## Project Overview

> **Note:** This is a high-level summary and does not cover every detail; the full project is more extensive and includes additional modules and workflows.

## Project Overview

This project is a comprehensive **Driving & Vehicle License Department (DVLD)** management system designed to automate the issuance, renewal, replacement, and suspension handling of driver licenses and related services. Built as a Windows Forms application, it follows a 3-Tier Architecture (Presentation, Business Logic, Data Access) and implements Object-Oriented Programming (OOP) principles.

## Technologies & Tools

* **Language & Framework**: C# with .NET Framework
* **UI Library**: Guna UI 2 for modern Windows Forms controls
* **Data Access**: ADO.NET
* **Database**: Microsoft SQL Server
* **Architecture**: 3-Tier (Presentation, Business Logic, Data Access)

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

   * CRUD operations for system users (roles)
   * CRUD operations for personal records (no duplicate national IDs)
4. **Testing & Validation**

   * Schedule and record vision, theoretical, and practical driving tests
   * Enforce age and previous-license checks per class
   * Record test results, allow retakes upon failure
5. **License Classes & Rules**

   * Maintain configurable license classes (age limit, validity, fees)
   * Automatic rejection for invalid age or duplicate license class
6. **Audit & Logging**

   * Record every operation with timestamp and responsible user
   * Filter and review actions for compliance
7. **Search & Reporting**

   * Query licenses by national ID or license number
   * Filter requests by status, date range, and type

## Installation & Setup

1. **Clone the Repository** 
2. **Restore Database**
3. **Build & Run**

   * Open DVLD Project File
   * Open DVLDPresentation File
   * Open the solution file `DVLDPresentation.sln` in Visual Studio.
   * Build the solution and run the application.

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
