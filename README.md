# AcademicInfo 🎓 📚

A comprehensive academic management system designed to streamline educational processes for students and teachers. This full-stack application handles everything from student enrollment to grade management and curriculum planning.

## Overview 🔍

AcademicInfo tackles the time-consuming process of managing academic information in educational environments. Built with a modern tech stack, it provides an intuitive interface for tracking student progress from undergraduate through master's programs, managing specializations, and handling contracts of studies and curriculum planning.

The system maintains complete academic records for each student, supports different faculty specializations for both undergraduate and master degrees, and manages curriculum requirements including mandatory and optional disciplines.

## Features ✨

- **User Authentication** - Secure JWT-based login system with role-based access control
- **Student Management** - Complete tracking of student academic progress
- **Curriculum Planning** - Management of mandatory and optional disciplines
- **Grade Recording** - Comprehensive grade entry and statistics
- **Optional Course Selection** - Interface for students to select optional courses
- **Contract of Studies** - Digital management of academic contracts
- **Multi-role Support** - Different interfaces for students, teachers, and administrators
- **Data Export** - Export functionality for academic records

## Tech Stack 💻

### Backend
- **Framework**: .NET Core (C#)
- **Database Access**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Architecture**: RESTful API design

### Frontend
- **Framework**: React 17
- **State Management**: React Hooks
- **Styling**: CSS
- **Internationalization**: i18n support
- **Package Management**: npm/yarn

## Architecture 🏗️

The application follows a modern client-server architecture:

1. **Backend API Layer** - RESTful services built with .NET Core
2. **Data Access Layer** - Entity Framework Core for database operations
3. **Frontend React Application** - Component-based UI with React 17
4. **Authentication Layer** - JWT implementation for secure access

The repository includes comprehensive UML documentation:
- Use Case diagrams
- Database schema diagrams
- Sequence diagrams
- Class diagrams

## Getting Started 🚀

### Backend Setup
1. Open the AcademicInfo solution in Visual Studio
2. Restore NuGet packages
3. Update database connection string in configuration
4. Run migrations to set up the database
5. Launch the API

### Frontend Setup
1. Install Node.js from [https://nodejs.org/en/](https://nodejs.org/en/)
2. Install a suitable IDE: VSCode/Atom
3. Navigate to the front-end-react-17/shop-front-end-main/react directory
4. Install dependencies: `npm install` (or `npm ci` if using Node version other than 16.14.0)
5. Start the application: `npm start`

## Project Structure 📁

```
ISSAltuzaante/
├── AcademicInfo/            # Backend .NET Core application
│   ├── Config/              # Configuration files
│   ├── Controllers/         # API controllers
│   ├── Models/              # Data models
│   │   └── DTOs/            # Data transfer objects
│   └── Migrations/          # Database migrations
├── front-end-react-17/      # Frontend React application
│   └── shop-front-end-main/
│       └── react/
│           ├── public/      # Static assets
│           └── src/         # React source code
│               ├── components/  # UI components
│               ├── controllers/ # Frontend controllers
│               └── assets/      # Frontend assets
└── Architecture Diagrams/   # UML documentation (.mdj files)
```
