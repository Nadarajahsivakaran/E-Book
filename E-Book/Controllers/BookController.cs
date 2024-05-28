using E_Book.DataAccess.IRepository;
using E_Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace E_Book.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = [];
            try
            {
                books = await _bookRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books.");
            }
            return View(books);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {  
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Book book)
        {   
            try
            {
                 await _bookRepository.Create(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding books.");
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = null;
            try
            {
                 book = await _bookRepository.GetData(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching books.");
            }
            return View(book);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Book book)
        {
            try
            {
                await _bookRepository.Update(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while update books.");
            }
            return RedirectToAction("Index");
        }
    }
}
