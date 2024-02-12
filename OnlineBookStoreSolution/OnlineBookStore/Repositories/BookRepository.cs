using Microsoft.Extensions.Logging;
using OnlineBookStore.Interfaces;
using OnlineBookStore.Contexts;
using OnlineBookStore.Models;

namespace OnlineBookStore.Repositories
{
    public class BookRepository : IRepository<int, Book>
    {
        private readonly Bookcontext _context;
        public BookRepository(Bookcontext bookContext)
        {
            _context = bookContext;
        }
        public Book Add(Book entity)
        {
            _context.Books.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Book Delete(int Key)
        {
            var book = GetById(Key);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return book;
            }
            return null;
        }

        public IList<Book> GetAll()
        {
            if (_context.Books.Count() == 0)
                return null;
            return _context.Books.ToList();
        }

        public Book GetById(int Key)
        {
            var book = _context.Books.SingleOrDefault(u => u.BookId == Key);
            return book;
        }

        public Book Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
            return book;
        }
    }
}
