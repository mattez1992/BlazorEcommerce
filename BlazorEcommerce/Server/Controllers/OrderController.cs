﻿using BlazorEcommerce.Server.Services.OrderServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        //[HttpPost]
        //public async Task<ActionResult<ServiceResponse<bool>>> Post()
        //{
        //    var result = await _orderService.PlaceOrder();
        //    return Ok(result);
        //}
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderOverViewDto>>>> GetOrders()
        {
            var result = await _orderService.GetOrders();
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<ServiceResponse<OrderDetailsDto>>> GetOrderDetails(int orderId)
        {
            var result = await _orderService.GetOrderDetails(orderId);
            return Ok(result);
        }
    }
}
