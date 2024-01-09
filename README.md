# Medical Management App

The Medical Management App is a Blazor Web Application designed for individual doctors to manage their patients efficiently. The application utilizes a local SQLite database to store patient information, therapy records, and associated PDF files. This README provides essential information on setting up, using, and deploying the application.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Requirements](#requirements)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Database](#database)
- [File Storage](#file-storage)
- [Deployment](#deployment-steps)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Overview

The Medical Management App enables individual doctors to manage patient records, therapy sessions, and generated PDF files. With a focus on simplicity and efficiency, the app provides a secure and user-friendly environment for doctors to track and organize their patient information.

## Features

- Patient management with detailed information including title, first name, last name, date of birth, place of birth, gender, phone number, and email.
- Therapy tracking for each patient.
- Attachment of one or more PDF files to each patient record.

## Requirements

- Windows 10 operating system on the doctor's computer.
- Visual Studio with ASP.NET and web development workload.
- SQLite NuGet package for database access.

## Getting Started

1. Clone the repository.
2. Configure the SQLite database connection in `Startup.cs`.
3. Set up authentication and authorization.
4. Run the application locally.

## Usage

1. Manage patients, therapies, and therapy templates through the user interface.
2. Ensure patient data is accurately entered, and PDF files are appropriately attached.

## Database

The application uses a local SQLite database. Database setup, including schema and migrations, is handled by the application.

## File Storage

PDF files are generated and downloaded locally on the doctor's computer. The application manages the linking of patient records to their respective files.

## Deployment Steps

### 1. Build and Publish the Application

1. Open the application solution in Visual Studio.
2. Build the solution to ensure all dependencies are resolved.
3. Use Visual Studio Publish Profiler to publish the application.

### 2. Create Service Archive

1. Navigate to the `setup/installer` directory.
2. Run `archive.bat` to create the `Service.zip` file containing the necessary files for installation.

### 3. Install the Windows Service

1. Run `install.bat` to install the application as a Windows service.
   
### 4. Set Up Task Scheduler for Database Backup

1. Open Task Scheduler on the doctor's computer.
2. Create a new task to schedule database backups.
3. Configure the task to run at specified intervals.
   - Set the action to run a batch script or executable that triggers the database backup process.

**Example Batch Script for Database Backup:**

```batch
REM Replace placeholders with actual paths and commands
-ExecutionPolicy Bypass -File "C:\path\to\your\backup_script.ps1"
```

## Testing

Thoroughly test the application to ensure it meets the specified requirements. Include unit tests and integration tests as needed.

## Contributing

Contribute to the project by submitting bug reports, feature requests, or code contributions. Follow the guidelines outlined in the CONTRIBUTING.md file. https://chat.openai.com/share/f6f0854a-aecb-43c7-aff6-d5e35711ac9b

## License

This project is licensed under the Papa_Mazinga License - see the LICENSE.md file for details.
