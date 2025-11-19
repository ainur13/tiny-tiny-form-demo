namespace TinyTinyForm.Domain;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; }

    public AuditableEntity()
    {
        CreatedAt = DateTime.Now;
    }
}