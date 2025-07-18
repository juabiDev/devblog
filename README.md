# DevBlog

**DevBlog** is a modern full-stack blogging platform designed for developers who want to share technical content, ideas, and experiences. The project aims to provide a clean, scalable, and maintainable architecture both on the frontend and backend, following industry best practices.

---

## ✨ About the Project

DevBlog is structured as a full-stack monorepo containing two main components:

- `frontend/` – A modern web interface built with [Next.js](https://nextjs.org), designed to deliver a fast and intuitive user experience.
- `backend/` – A robust RESTful API built with [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/), following clean architecture principles, layered separation of concerns, and scalable design.

This separation allows each part of the application to evolve independently while remaining tightly integrated through well-defined contracts and API communication.

---

## 🧱 Architecture

The project is structured with long-term scalability and clarity in mind:

```
DevBlog/
├── backend/       # Backend codebase (API, Domain, Infrastructure)
├── frontend/      # Frontend codebase (Next.js UI)
└── README.md      # You are here
```

Each module (`backend/` and `frontend/`) contains its own `README.md` file with specific setup instructions, environment configuration, and development workflows.

---

## 🎯 Core Principles

- **Separation of concerns** – Backend and frontend are organized independently, encouraging modular development.
- **Clean architecture** – The backend follows the principles outlined by Uncle Bob, promoting testability and flexibility.
- **Developer experience** – The frontend is optimized for fast iteration and maintainability.
- **Scalability** – The structure is designed to support future growth, including authentication, real-time features, and integrations.

---

## 🚀 Getting Started

To run or contribute to this project:

1. Navigate to the `backend/` or `frontend/` directories.
2. Follow the setup instructions in their respective `README.md` files.
3. Optionally, use Docker for a fully containerized development environment.

---

## 🛠️ Development Workflow

- Issues, improvements, and new features are tracked through Git.
- Each module can be developed, tested, and deployed independently.
- Pull requests should follow clean commit standards and reflect modular, testable code.

---

## 🤝 Contributing

We welcome contributions! If you’d like to report a bug, suggest an enhancement, or submit a pull request, please follow these steps:

1. Fork the repository  
2. Create a new branch  
3. Make your changes  
4. Open a pull request with a clear description

---

## 📄 License

This project is licensed under the **MIT License**.
