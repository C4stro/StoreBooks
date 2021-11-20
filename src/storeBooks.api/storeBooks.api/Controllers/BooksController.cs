using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using storeBooks.domain.models;
using storeBooks.repository.Dto;
using storeBooks.service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace storeBooks.api.Controllers
{
    [EnableCors("Privacy")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly DbContextModels _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksService booksService, ILogger<BooksController> logger, DbContextModels context)
        {
            _booksService = booksService;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("All")]
        public ActionResult<IEnumerable<BookStore>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Requisition for get all books actived in {DateTime.Now}");

                var books = _context.BooksStores.ToList();

                return Ok(books);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<IEnumerable<BookStore>> Create()
        {
            try
            {
                _logger.LogInformation($"Requisition for get all books actived in {DateTime.Now}");
                return Ok(_booksService.GetById(1));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
