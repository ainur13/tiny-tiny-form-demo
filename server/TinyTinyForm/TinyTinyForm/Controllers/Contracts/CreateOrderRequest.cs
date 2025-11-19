namespace TinyTinyForm.Controllers.Contracts;

public record CreateOrderRequest
{
    public string Cake { get; init; }   
    public string ClientName { get; init; }
    public DateTime DeliveryDate { get; init; }
    public byte DeliveryMethod { get; init; }
    public bool GiftWrap { get; init; }
}