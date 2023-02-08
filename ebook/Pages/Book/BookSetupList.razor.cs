using ebook.Models.Books;
using ebook.Services.Interface;
using ebook.Shared.Component;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.Book
{
    public partial class BookSetupList
    {
        private List<BooksModel> bookModel { get; set; } = new();
        [Inject] private IBookService booksService { get; set; }

        private string searchString1 = "";

        protected override async Task OnInitializedAsync()
        {
            await GetBooks();
        }
        private async Task GetBooks()
        {
            bookModel = await booksService.getBooksAsync();
            foreach (var items in bookModel)
            {
                items.publicationDate = DateTime.Parse(items.publication_date.ToString()).ToShortDateString();
            }
        }

        private async Task AddBooks()
        {
            BooksModel books = new BooksModel() { book_id = 0, title = "", isbn = "", num_pages = 0, book_cover = "", publication_date = DateTime.Now, };
            await MenuDialog(books);
        }

        private async Task EditBooks(BooksModel bookModel)
        {
            if (bookModel.book_id != null)
            {
                await MenuDialog(bookModel);
            }
        }

        private async Task DeleteBooks(int id)
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete Book? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<Confirmation>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await booksService.deleteBooksAsync(id);
                await GetBooks();
                _snackBar.Add("Books Deleted Successfully.", Severity.Success);
            }
        }

        private async Task MenuDialog(BooksModel bookModel)
        {
            var dialogOptions = new DialogOptions() { MaxWidth = MaxWidth.Small, FullWidth = true, CloseButton = true };
            var parameters = new DialogParameters { ["booksModel"] = bookModel };

            var dialog = _dialogService.Show<BookSave>("Add Book", parameters, dialogOptions);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetBooks();
                _snackBar.Add("Books Saved Successfully.", Severity.Success);
            }
        }

        private bool FilterFunc1(BooksModel element) => FilterFunc(element, searchString1);
        private bool FilterFunc(BooksModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
