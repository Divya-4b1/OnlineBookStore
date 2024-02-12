using Microsoft.Extensions.Logging;
using OnlineBookStore.Interfaces;
using static OnlineBookStore.Services.BookService;
using OnlineBookStore.Models;
using OnlineBookStore.Models.DTOs;
using OnlineBookStore.Repositories;
using OnlineBookStore.Exceptions;



namespace OnlineBookStore.Services
{
    public class BookService : IBookService
    {
        
        private readonly IRepository<int, Book> _bookRepo;
        public BookService(IRepository<int, Book> bookRepo)
            {
                _bookRepo = bookRepo;
            }
        public bool Add(BookDTO bookDTO)
        {
            if (bookDTO == null)
                throw new ArgumentNullException(nameof(bookDTO), "BookDTO cannot be null.");

            
            var bookEntity = ConvertToEntity(bookDTO);
            _bookRepo.Add(bookEntity);
            return bookEntity.BookId > 0;
        }

       

        public BookDTO Get(int id)
        {
           
            var bookEntity = _bookRepo.GetById(id);

           
            return ConvertToDTO(bookEntity);
        }

        public IEnumerable<BookDTO> GetAll()
        {
            // Get all Event Entities
            var eventEntities = _bookRepo.GetAll();

            // Convert Entities to DTOs
            return eventEntities.Select(ConvertToDTO);
        }

        public bool Remove(int id)
        {
            // Remove Event Entity by id
            var deletedEvent = _bookRepo.Delete(id);

            // Return true if deletion was successful
            return deletedEvent != null;
        }

        public BookDTO Update(BookDTO bookDTO)
        {
            if (bookDTO == null)
                throw new ArgumentNullException(nameof(bookDTO), "EventDTO cannot be null.");

            
            var eventEntity = ConvertToEntity(bookDTO);

           
            var updatedEntity = _bookRepo.Update(eventEntity);

          
            return ConvertToDTO(updatedEntity);
        }
        public BookDTO GetBookByTitle(string title)
        {
            try
            {
                var bookByTitle = _bookRepo.GetAll().FirstOrDefault(b => b.Title == title);
                if (bookByTitle == null)
                    return null;
                return new BookDTO
                {
                    BookId = bookByTitle.BookId,
                    Title = bookByTitle.Title,
                    Author = bookByTitle.Author,
                    Genere = bookByTitle.Genere,
                   
                    PublishedDate = bookByTitle.PublishedDate,
                    Username = bookByTitle.Username
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }

        public BookDTO GetBooksByGenre(string genere)
        {
            try
            {
                var existingBook = _bookRepo.GetAll().FirstOrDefault(b => b.Genere == genere);
                if (existingBook != null)
                {
                    return new BookDTO
                    {
                        BookId = existingBook.BookId,
                        Title = existingBook.Title,
                        Author = existingBook.Author,
                        Genere = existingBook.Genere,
                        
                        PublishedDate = existingBook.PublishedDate,
                        Username = existingBook.Username
                    };
                }
                throw new BookNotFoundException();
            }
            catch (Exception ex)
            {
                // Log the exception
                return null;
            }
        }

        // Helper method to convert Event Entity to EventDTO
        private BookDTO ConvertToDTO(Book bookEntity)
        {
            if (bookEntity == null)
                return null;

            return new BookDTO
            {
                // Populate DTO properties from Entity properties
                BookId = bookEntity.BookId,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                Genere = bookEntity.Genere,

                PublishedDate = bookEntity.PublishedDate,
                Username =  bookEntity.Username
            };
        }

        // Helper method to convert EventDTO to Event Entity
        private Book ConvertToEntity(BookDTO bookDTO)
        {
            if (bookDTO == null)
                return null;

            return new Book
            {
                // Populate Entity properties from DTO properties
                BookId = bookDTO.BookId,
                Title = bookDTO.Title,
                Author = bookDTO.Author,
                Genere = bookDTO.Genere,
                
                PublishedDate = bookDTO.PublishedDate,
                Username = bookDTO.Username
            };
        }

    }
}
