using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders;
using Order.Application.Features.Orders.Create;

namespace Order.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        await orderService.CreateOrderAsync(request);
        return Ok();
    }
}