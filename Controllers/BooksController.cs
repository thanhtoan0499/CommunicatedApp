using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.Data;
using DatingApp.Models;
using DatingApp.ViewModel.BookViewModel;
using AutoMapper;
using DatingApp.Extensions;
using Microsoft.AspNetCore.Hosting;
using DatingApp.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using DatingApp.Entities;

namespace DatingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly BookImageServices bookImageServices;
        private readonly UserManager<AppUser> userManager;

        public BooksController(DataContext context,
                               IMapper mapper,
                               IWebHostEnvironment webHostEnvironment,
                               BookImageServices bookImageServices,
                               UserManager<AppUser> userManager)
        {
            _context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.bookImageServices = bookImageServices;
            this.userManager = userManager;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var book = await _context.Books
                .Include(c => c.Category)
                .Include(c => c.Comments)
                .Include(i => i.Images)
                .ToListAsync();
            var bookVM = mapper.Map<IList<BookViewModel>>(book);
            return Ok(bookVM);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Where(c => c.Id == id)
                .Include(c => c.Category)
                .Include(c => c.Comments)
                .Include(i => i.Images)
                .ToListAsync();
            var bookVM = mapper.Map<IList<BookViewModel>>(book);
            if (bookVM == null)
            {
                return NotFound();
            }
            return Ok(bookVM);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> PutBook([FromForm]UpdateBookViewModel updateBook)
        {
            Book book = mapper.Map<Book>(updateBook);
            book.Slug = updateBook.Name.GenerateSlug();
            //await bookImageServices.SaveImageList(book, updateBook.Images, );
            _context.Books.Add(book);
            if (updateBook.Id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(updateBook.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/Books
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromForm]BookDto bookDto)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                Book book = mapper.Map<Book>(bookDto);
                book.Slug = bookDto.Name.GenerateSlug();
                await bookImageServices.SaveImageList(book, bookDto.ImageFiles, userId);
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            //List<Image> images = await _context.Images.Where(x => x.BookId == book.Id).ToListAsync();
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
