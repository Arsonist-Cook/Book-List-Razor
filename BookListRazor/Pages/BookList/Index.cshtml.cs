using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public IndexModel(ApplicationDbContext database)
        {
            _database = database;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await _database.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var BookFromDb = await _database.Book.FindAsync(id);

            if (BookFromDb == null)
            {
                return NotFound();
            }

            _database.Book.Remove(BookFromDb);

            var result = await _database.SaveChangesAsync();

            if (result <= 0)
            {
                return BadRequest();
            }

            return RedirectToPage();
        }
    }
}
