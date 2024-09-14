namespace Persistence.Models;

public class Note
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}
