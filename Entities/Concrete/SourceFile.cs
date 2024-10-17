using Core.Entities;

namespace Entities.Concrete;

public class SourceFile :IEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? DataPath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}