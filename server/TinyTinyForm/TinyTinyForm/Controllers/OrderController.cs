using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyTinyForm.Controllers.Contracts;
using TinyTinyForm.Domain;
using TinyTinyForm.Infrastructure;

namespace TinyTinyForm.Controllers;

[ApiController]
[Route("/api/orders")]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateOrderRequest request, CancellationToken ct)
    {
        var order = new Order(
            request.Cake, request.ClientName, 
            request.DeliveryDate, (DeliveryMethod)request.DeliveryMethod,
            request.GiftWrap);
        
        await _context.Orders.AddAsync(order, ct);
        await _context.SaveChangesAsync(ct);
        return Ok(order.Id);
    }

    [HttpGet]
    public async Task<ActionResult<Order[]>> Get([FromQuery] GetOrdersRequest request, CancellationToken ct)
    {
        Thread.Sleep(TimeSpan.FromSeconds(2));
        var orders = await _context.Orders
            .Where(x => string.IsNullOrWhiteSpace(request.ClientName) || EF.Functions.Like(x.ClientName, $"%{request.ClientName}%"))
            .ToArrayAsync(ct);
        return Ok(orders);
    }
}