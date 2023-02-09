using ebook.Configuration;
using ebook.Models.Books;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ebook.Pages.Book
{
    partial class EBookImageInsert
    {
        IList<IBrowserFile> files = new List<IBrowserFile>();

        [Parameter] public BooksModel? booksModel { get; set; }
        [Inject] private IBookService bookService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        private EBookModel ebooks { get; set; } = new();
        [Parameter] public List<EBookModel> ebooksList { get; set; }

        private async Task Save()
        {
            if(ebooksList.Count > 0)
            {
                ebooks.page_num = ebooksList.Count;
            }
            if (booksModel.title == "")
            {
                _snackBar.Add("Book Name Required!", Severity.Error);
            }
            else
            {
                ebooks.book_id = booksModel.book_id;
                foreach (var items in files)
                {
                    ebooks.page_num += 1;
                    using var stream = items.OpenReadStream(51200000000);
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    ebooks.book_image = base64;
                    await bookService.saveBooksImageAsync(ebooks);
                }
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
        private void UploadFiles(IBrowserFile file)
        {
            files.Add(file);
        }
    }
}
