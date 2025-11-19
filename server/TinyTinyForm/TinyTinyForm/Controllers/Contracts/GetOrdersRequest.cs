namespace TinyTinyForm.Controllers.Contracts;

public record GetOrdersRequest
{
    public string? ClientName { get; init; } = string.Empty;
}