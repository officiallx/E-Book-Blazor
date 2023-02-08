using ebook.Models;
using ebook.Models.Author;

namespace ebook.Services.Impl
{
    public interface IAuthorService
    {
        Task<MessageResponse> saveAuthorAsync(AuthorModel author);
        Task<List<AuthorModel>> getAuthorAsync();
        Task deleteAuthorAsync(long Id);
    }
}
