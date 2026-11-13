using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs;

public class CreateTodoRequest
{
    [Required, MinLength(1), MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }
}

public class UpdateTodoRequest
{
    [Required, MinLength(1), MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? DueDate { get; set; }
}

public record TodoResponse(
    int Id,
    string Title,
    string? Description,
    bool IsCompleted,
    DateTime? DueDate,
    DateTime CreatedAt
);
