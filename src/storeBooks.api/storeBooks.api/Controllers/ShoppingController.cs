using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using storeBooks.domain.models;
using storeBooks.domain.Shared;
using storeBooks.repository.Dto;
using storeBooks.service.interfaces;
using System;
using System.Collections.Generic;

namespace storeBooks.api.Controllers
{
    [EnableCors("Privacy")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingController : Controller
    {
        private readonly ISalesBookService _salesBookService;
        private readonly DbContextModels _context;
        private readonly ILogger<ShoppingController> _logger;
        public ShoppingController(ISalesBookService salesBookService, DbContextModels context, ILogger<ShoppingController> logger)
        {
            _context = context;
            _logger = logger;
            _salesBookService = salesBookService;
        }
        
        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<SalesBook>> Index()
        {
            var result = _salesBookService.TakeAllShopping();

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        [Route("Buy/{currency?}")]
        public ActionResult<IEnumerable<SalesBook>> Buy([FromBody] SalesBook buy, EnumExchange.Exchanges? currency = null)
        {
            _logger.LogInformation($"Requisition for Buy in {DateTime.Now}");

            try
            {
                var result = _salesBookService.BuyMade(buy, currency.ToString());

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
