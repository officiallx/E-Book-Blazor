using ebook.Configuration;
using ebook.Models.Books;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace ebook.Pages.Book
{
    public partial class BookSave
    {
        IList<IBrowserFile> files = new List<IBrowserFile>();
        [Parameter] public BooksModel? booksModel { get; set; }
        [Inject] private IBookService bookService { get; set; }
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        private async Task Save()
        {

            if (booksModel.title == "")
            {
                _snackBar.Add("Book Name Required!", Severity.Error);
            }
            else
            {
                var stream = files.FirstOrDefault().OpenReadStream();
                byte[] bytes = stream.ReadAllBytes();
                var base64 = Convert.ToBase64String(bytes);
                booksModel.book_cover = base64;
                var Message = await bookService.saveBooksAsync(booksModel);
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
        private void UploadFiles(IBrowserFile file)
        {
            if(files.Count > 0)
            {
                files.Clear();
            }
            files.Add(file);
        }
    }
}
