# 📝 Blog API

A simple but well-structured ASP.NET Core Web API for managing Users, Posts, Categories, and Comments — built with clean architecture in mind and developed as part of a self-paced learning journey.

---

## 🚀 Features

- ✅ CRUD operations for Users, Posts, Comments, and Categories  
- ✅ Entity relationships (User → Posts, Post → Comments, Category → Posts)
- ✅ Clean separation using **Services**, **DTOs**, and **Interfaces**
- ✅ **Validation** on DTOs for safe data input
- ✅ Simple custom wrapper: `ServiceResult<T>` to handle success/failure responses
- ✅ Unit tests using **xUnit** + **InMemoryDb**
- ✅ Swagger UI for easy testing and visualization
- ✅ Centralized error handling and proper HTTP responses
- ✅ Folder structure follows clean code and organization principles

---

## 📂 Project Structure

UserPostApi/
├── Controllers/
├── Models/
│ ├── Entities/
│ ├── DTOs/
├── Services/
│ ├── Interfaces/
│ └── Implementation/
├── Helpers/
│ └── ServiceResult.cs
├── Extensions/
│ └── ServiceCollectionExtensions.cs
└── Program.cs


---

## 🛠️ Tech Stack

- **ASP.NET Core Web API (.NET 6)**
- **Entity Framework Core** (Code-First)
- **SQL Server**
- **xUnit** (Unit Testing)
- **Swagger / Swashbuckle**

---

## ✅ Sample Endpoints

- `GET /api/user` - Get all users  
- `POST /api/post` - Add a new post  
- `GET /api/category` - List categories with related posts  
- `DELETE /api/user/{id}/posts` - Delete all posts for a user  
- `GET /api/comment?postId=3` - Get comments for a post

---
Feedback is always welcome!
Feel free to open an issue or reach out on LinkedIn. 
