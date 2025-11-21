using System.Text.Json;

namespace TinyTinyDynamicForm.Models;

public class Submission
{
    public int Id { get; protected set; }
    public JsonElement RawJson { get; protected set; }
    public DateTime SubmittedAt { get; protected set; }

    protected Submission() 
    {
        SubmittedAt = DateTime.UtcNow;
    }

    public Submission(string rawJson) : this()
    {
        using var json = JsonDocument.Parse(rawJson);
        RawJson = json.RootElement.Clone();
    }
}
