using OnlineBookStore.Models.DTOs;

namespace OnlineBookStore.Interfaces
{
    public interface IBookService
    {
        bool Add(BookDTO bookDTO);
        bool Remove(int id);
        BookDTO Update(BookDTO bookDTO);
        BookDTO Get(int id);

        BookDTO GetBooksByGenre(string genre);
        BookDTO GetBookByTitle(string title);
        IEnumerable<BookDTO> GetAll();
    }
}
