using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/orders/{orderId}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public OrderItemController(IDutchRepository repository, ILogger<OrderItemController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            try
            {
                _logger.LogInformation("Request send from controller to get items for order id from IDutchrepository");
                var order = _repository.GetOrderById(User.Identity.Name, orderId);
                if (order != null)
                {
                    var items = order.Items;
                    return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(items));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get items in orders due to exception: {ex}");
                return BadRequest("Bad Request: Failed to get items in orders");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                _logger.LogInformation("Request send from controller to get items for order id from IDutchrepository");
                var order = _repository.GetOrderById(User.Identity.Name, orderId);
                if (order != null)
                {
                    var items = order.Items.Where(i => i.Id == id).FirstOrDefault();
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(items));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get items in orders due to exception: {ex}");
                return BadRequest("Bad Request: Failed to get items in orders");
            }
        }


    }
}
