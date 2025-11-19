namespace TinyTinyForm.Domain;

public class Order : AuditableEntity
{
    public int Id { get; protected set; }
    public string Cake { get; protected set; }   
    public string ClientName { get; protected set; }
    public DateTime DeliveryDate { get; protected set; }
    public DeliveryMethod DeliveryMethod { get; protected set; }
    public bool GiftWrap { get; protected set; }

    public Order(string cake, string clientName, DateTime deliveryDate, DeliveryMethod deliveryMethod, bool giftWrap) : base()
    {
        Cake = cake;
        ClientName = clientName;
        DeliveryDate = deliveryDate;
        DeliveryMethod = deliveryMethod;
        GiftWrap = giftWrap;
    }
}