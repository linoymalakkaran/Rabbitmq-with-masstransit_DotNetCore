using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViewModel;

namespace MicroServiceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OrderController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            Uri uri = new Uri("rabbitmq://localhost/order-queue");

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(order);

            return Ok("Success");
        }
    }
}