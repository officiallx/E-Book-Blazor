using ebook.Models.Books;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ebook.Pages.Book
{
    partial class EBookView
    {
        [Parameter] public string bookId { get; set; }
        [Inject] public IBookService bookService { get; set; }
        private List<EBookModel> eBooksList { get; set; } = new();
        protected async override Task OnInitializedAsync()
        {
            await getEbooks();
        }

        public async Task getEbooks()
        {
            eBooksList = await bookService.getEBooksAsyncByBookId(int.Parse(bookId));
            if (eBooksList.Count > 0)
            {
                foreach (var items in eBooksList)
                {
                    items.book_image = string.Concat("data:image/png;base64,", items.book_image);
                    items.book_cover = string.Concat("data:image/png;base64,", items.book_cover);
                }
            }
        }
    }
}
