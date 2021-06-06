using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Model;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger, IMapper mapper, UserManager<StoreUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                _logger.LogInformation("Request send from controller to get all orders from IDutchrepository");
                var username = User.Identity.Name;
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_repository.GetAllOrdersByUser(username, includeItems)));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get orders due to exception: {ex}");
                return BadRequest("Bad Request: Failed to get orders");
            }


        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                _logger.LogInformation("Request send from controller to get order for id from IDutchrepository");
                var order = _repository.GetOrderById(User.Identity.Name, id);
                if(order != null)
                {
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }
                else
                {
                    return NotFound();
                }
               
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get orders due to exception: {ex}");
                return BadRequest("Bad Request: Failed to get orders");
            }


        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                _logger.LogInformation("Request send from controller to add orders and return result from IDutchrepository");
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _repository.AddEntity(newOrder);
                    if (_repository.SaveAll())
                    {
                        var vm = _mapper.Map<Order, OrderViewModel>(newOrder);
                        return Created($"Data added to the database for api/orders/{vm.OrderId}", vm);
                    }
                    else
                    {
                        return BadRequest($"Something wrong with parameters, Data is unable to save in to database");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }


            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to add orders due to exception: {ex}");
                return BadRequest("Bad Request: Failed to add orders");
            }


        }

    }
}
