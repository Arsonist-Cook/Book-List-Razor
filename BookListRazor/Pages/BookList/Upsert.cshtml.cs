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
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _database;

        public UpsertModel(ApplicationDbContext database)
        {
            _database = database;
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            if (id.HasValue)
            { //If  id value is not null it is a edit call

                Book = await _database.Book.FirstOrDefaultAsync(book => book.Id == id);

                if (Book == null)
                {
                    return NotFound();
                }
            }
            else
            {
                Book = new Book();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    _database.Book.Add(Book);
                }
                else
                {
                    _database.Book.Update(Book);
                }



                await _database.SaveChangesAsync();

                return RedirectPermanent("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
