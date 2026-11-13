using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Data;
using TodoListApi.DTOs;
using TodoListApi.Models;

namespace TodoListApi.Controllers;

[ApiController]
[Route("api/todoitems")]
public class TodoItemsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponse>>> GetAll(
        [FromQuery] bool? completed = null)
    {
        var query = db.TodoItems.AsNoTracking().AsQueryable();

        if (completed.HasValue)
            query = query.Where(x => x.IsCompleted == completed.Value);

        var items = await query
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => ToResponse(x))
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoResponse>> GetById(int id)
    {
        var item = await db.TodoItems.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return item is null ? NotFound() : Ok(ToResponse(item));
    }

    [HttpPost]
    public async Task<ActionResult<TodoResponse>> Create(CreateTodoRequest request)
    {
        var item = new TodoItem
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            DueDate = request.DueDate,
            CreatedAt = DateTime.UtcNow
        };

        db.TodoItems.Add(item);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, ToResponse(item));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTodoRequest request)
    {
        var item = await db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();

        item.Title = request.Title.Trim();
        item.Description = request.Description?.Trim();
        item.IsCompleted = request.IsCompleted;
        item.DueDate = request.DueDate;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();

        db.TodoItems.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static TodoResponse ToResponse(TodoItem item) => new(
        item.Id,
        item.Title,
        item.Description,
        item.IsCompleted,
        item.DueDate,
        item.CreatedAt
    );
}
