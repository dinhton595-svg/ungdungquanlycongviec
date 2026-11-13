using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
}
