# WebSocket ASP.NET Core

This project is a simple WebSocket implementation that mimics a real-world one-on-one chat and group chat application. The goal was to understand WebSockets and how they handle real-time messaging efficiently. The architecture follows SOLID principles to keep the code modular and maintainable. 🚀

## ⚙️ Technologies Used

- **.NET** (ASP.NET Core)
- **WebSockets** for real-time communication
- **Dependency Injection** for maintainability
- **SOLID Principles** for clean architecture

## 🎯 Features

- ✅ One-to-one private chat
- ✅ Group chat functionality
- ✅ Users can join/leave groups dynamically
- ✅ Frontend compatibility with WebSocket clients

## 🔧 Installation & Setup

### 1️⃣ Clone the repository

```sh
git clone https://github.com/Sahil2k07/WebSockets-ASP.NET-Core.git

cd WebSockets-ASP.NET-Core
```

### 2️⃣ Install dependencies

```sh
dotnet restore
```

### 3️⃣ Run the server

```sh
dotnet run
```

## 📡 WebSocket Endpoints

| Endpoint                          | Description             |
| --------------------------------- | ----------------------- |
| `/api/chat?userID=1&receiverID=2` | Private chat connection |
| `/api/group?groupID=1`            | Group chat connection   |

## 🛠️ Future Enhancements

- 🔹 **Database Integration** (Message persistence)
- 🔹 **User Authentication**
- 🔹 **Typing Indicators**
- 🔹 **Read Receipts**

## 📜 License

This project is **MIT Licensed**. Feel free to modify and use it!
