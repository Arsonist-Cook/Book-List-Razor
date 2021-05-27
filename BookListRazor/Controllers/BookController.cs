using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _database;

        public BookController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _database.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _database.Book.FirstOrDefaultAsync(book => book.Id == id);
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _database.Book.Remove(bookFromDb);
            await _database.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}
