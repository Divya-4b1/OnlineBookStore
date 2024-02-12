using System.Runtime.Serialization;
namespace OnlineBookStore.Exceptions
{
   
    [Serializable]
    internal class BookNotFoundException : Exception
    {
        string message;
        public BookNotFoundException()
        {
            message = "Book with given Id/Title/Author/Genre not Found";
        }
        public override string Message => message;

    }
}
