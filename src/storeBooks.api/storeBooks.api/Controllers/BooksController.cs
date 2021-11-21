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

                if (books.Count == 0)
                    return NoContent();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Requisition for get all books BadRequest {ex.Message}");

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<IEnumerable<BookStore>> Create()
        {
            try
            {
                _logger.LogInformation($"Requisition for create {DateTime.Now}");

                var books = _context.BooksStores.ToList();

                if (books == null)
                    return NoContent();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Requisition for create BadRequest: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ID/{Id}")]
        public ActionResult<IEnumerable<BookStore>> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Requisition for GetById {DateTime.Now}");

                var books = _booksService.GetById(id);
                
                if (books == null)
                    return NoContent();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Requisition for GetById BadRequest: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Author/{author}")]
        public ActionResult<IEnumerable<BookStore>> GetByAuthor(string author)
        {
            try
            {
                _logger.LogInformation($"Requisition for GetByAuthor {DateTime.Now}");

                var books = _booksService.GetByAuthor(author);

                if (books == null)
                    return NoContent();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Requisition for GetByAuthor BadRequest: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Title/{title}")]
        public ActionResult<IEnumerable<BookStore>> GetByTitle(string title)
        {
            try
            {
                _logger.LogInformation($"Requisition for GetByTitle {DateTime.Now}");

                var books = _booksService.GetByTitle(title);

                if (books == null)
                    return NoContent();

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Requisition for GetByTitle BadRequest: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
