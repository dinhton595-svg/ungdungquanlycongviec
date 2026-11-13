# Todo List API — ASP.NET Core

## Công nghệ
- .NET 8 Web API
- Entity Framework Core 8
- SQLite
- Swagger / OpenAPI

## Chạy dự án

```bash
dotnet restore
dotnet run
```

Mở đường dẫn Swagger được in trong terminal, thường có dạng:

```text
https://localhost:xxxx/swagger
```

Database `todolist.db` sẽ được tạo tự động ở lần chạy đầu tiên.

## API endpoints

| Method | Endpoint | Mục đích |
|---|---|---|
| GET | `/api/todoitems` | Lấy danh sách công việc |
| GET | `/api/todoitems/{id}` | Lấy một công việc |
| POST | `/api/todoitems` | Tạo công việc mới |
| PUT | `/api/todoitems/{id}` | Cập nhật công việc |
| DELETE | `/api/todoitems/{id}` | Xóa công việc |

Có thể lọc theo trạng thái:

```text
GET /api/todoitems?completed=true
```

## Ví dụ POST

```json
{
  "title": "Hoàn thành bài tập backend",
  "description": "Xây dựng Todo List API bằng ASP.NET Core",
  "dueDate": "2026-12-01T17:00:00Z"
}
```

## Gợi ý nâng cấp
- Thay `EnsureCreated` bằng EF Core Migrations khi triển khai thật.
- Thêm xác thực JWT và phân quyền theo người dùng.
- Bổ sung phân trang, tìm kiếm, unit test và integration test.
