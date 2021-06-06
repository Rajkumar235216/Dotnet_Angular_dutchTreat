using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {

            try
            {
                _logger.LogInformation("Get All Orders Information from DutchRepository was called");
                if(includeItems == true)
                {
                    return _ctx.Orders.Include(o => o.Items).ThenInclude(p => p.Product).ToList();
                }
                else
                {
                    return _ctx.Orders.ToList();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in Retrieving Get All Orders from Dutch Reposity due to :  {ex}");
                return null;
            }

        }


        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {

            try
            {
                _logger.LogInformation("Get All Orders Information from DutchRepository from user was called");
                if (includeItems == true)
                {
                    return _ctx.Orders.Where(u=> u.User.UserName == username).Include(o => o.Items).ThenInclude(p => p.Product).ToList();
                }
                else
                {
                    return _ctx.Orders.Where(u => u.User.UserName == username).ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in Retrieving Get All Orders from Dutch Reposity from  due to :  {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {

            try {
                _logger.LogInformation("Get All Products Information from DutchRepository was called");
                return _ctx.Products
                     .OrderBy(p => p.Title)
                     .ToList();
            }
            catch(Exception ex) {
                _logger.LogInformation($"Error in Retrieving Get All Products from Dutch Reposity due to :  {ex}");
                return null;
            }
         
        }

        public Order GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation("Get Orders By Id Information from DutchRepository was called");
                return _ctx.Orders.Include(o => o.Items).ThenInclude(p => p.Product).Where(o => o.Id==id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in Retrieving Get All Orders from Dutch Reposity due to :  {ex}");
                return null;
            }
        }

        public Order GetOrderById(string name, int id)
        {
            try
            {
                _logger.LogInformation("Get Orders By Id Information from DutchRepository from user was called");
                return _ctx.Orders.Include(i => i.Items).ThenInclude(p => p.Product).Where(o => o.Id == id && o.User.UserName == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error in Retrieving Get All Orders from Dutch Repositydrom user due to :  {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                 .Where(p => p.Category == category)
                 .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
