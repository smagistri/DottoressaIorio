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
- [Deployment](#deployment)
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

1. Log in with doctor credentials.
2. Manage patients, therapies, and file attachments through the user interface.
3. Ensure patient data is accurately entered, and PDF files are appropriately attached.

## Database

The application uses a local SQLite database. Database setup, including schema and migrations, is handled by the application.

## File Storage

PDF files are generated and downloaded locally on the doctor's computer. The application manages the linking of patient records to their respective files.


## Deployment

Deploy the application to the doctor's computer for local use.

## Testing

Thoroughly test the application to ensure it meets the specified requirements. Include unit tests and integration tests as needed.

## Contributing

Contribute to the project by submitting bug reports, feature requests, or code contributions. Follow the guidelines outlined in the CONTRIBUTING.md file.

## License

This project is licensed under the Papa_Mazinga License - see the LICENSE.md file for details.
