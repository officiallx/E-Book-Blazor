using ebook.Models;
using ebook.Models.Books;

namespace ebook.Services.Interface
{
    public interface IBookService
    {
        Task<MessageResponse> saveBooksAsync(BooksModel books);
        Task<List<BooksModel>> getBooksAsync();
        Task deleteBooksAsync(long Id);
        Task<MessageResponse> saveBooksImageAsync(EBookModel books);
        Task<List<EBookModel>> getEBooksAsyncByBookId(int bookId);
    }
}
