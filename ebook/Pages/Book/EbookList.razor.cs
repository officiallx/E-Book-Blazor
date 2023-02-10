using ebook.Models.Books;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;

namespace ebook.Pages.Book
{
    partial class EbookList
    {
        private List<BooksModel> booksModelsList { get; set; } = new();
        private BooksModel booksModel { get; set; } = new();
        [Inject] private IBookService bookService{ get; set; }
        private BooksModel SelectedProduct;
        bool ShowItem = true;
        private bool resetValueOnEmptyText;
        private string searchString1 = "";

        void ProductSelected(dynamic book_id)
        {
            SelectedProduct = (from x in booksModelsList
                               where x.book_id == Convert.ToInt32(book_id)
                               select x).First();
            ShowItem = true;
        }
        private IEnumerable<string> MaxCharacters(string ch)
        {
            if (!string.IsNullOrEmpty(ch) && 25 < ch?.Length)
                yield return "Max 25 characters";
        }
        private async Task GetBooks()
        {
            booksModelsList = await bookService.getBooksAsync();
            if (booksModelsList.Count > 0)
            {
                foreach(var items in booksModelsList)
                {
                    items.book_cover = string.Concat("data:image/png;base64,", items.book_cover);
                }
            }
        }

        protected async override Task OnInitializedAsync()
        {
            resetValueOnEmptyText = true;
            await GetBooks();
        }

        private bool FilterFunc1(BooksModel element) => FilterFunc(element, searchString1);

        private bool FilterFunc(BooksModel element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.isbn.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        private async Task filterProduct()
        {
            if (searchString1 != null && searchString1 != "")
            {
                booksModelsList = await bookService.getProductFilteredAsync(searchString1);
                if (booksModelsList.Count > 0)
                {
                    foreach (var items in booksModelsList)
                    {
                        items.book_cover = string.Concat("data:image/png;base64,", items.book_cover);
                    }
                }
            }
            else
            {
                booksModelsList = await bookService.getBooksAsync();
                if (booksModelsList.Count > 0)
                {
                    foreach (var items in booksModelsList)
                    {
                        items.book_cover = string.Concat("data:image/png;base64,", items.book_cover);
                    }
                }
            }
        }

        private void readMoreClick()
        {
            _navigationManager.NavigateTo($"/ebooks/{SelectedProduct.book_id}");
        }
    }
}
