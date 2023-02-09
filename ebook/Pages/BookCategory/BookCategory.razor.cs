using ebook.Models;
using ebook.Services.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ebook.Pages.BookCategory
{
    public partial class BookCategory
    {
        [Parameter] public MenuRoleModel? menuRoleModel { get; set; } = new();
        [Parameter] public MenuRoleModel? menuRoleModel1 { get; set; } = new();
        [Inject] private IMenuRoleServices MenuRoleRepository { get; set; }
        public bool CheckSelected { get; private set; }

        private async void Save()
        {

            menuRoleModel1.menu = menuRoleModel.menu.Where(x => x.IsActive == true).ToList();
            menuRoleModel.menu = menuRoleModel.menu.ToList();
            menuRoleModel1.book_id = menuRoleModel.book_id;

            var data = await MenuRoleRepository.saveMenuRoleAsync(menuRoleModel1);
            if (data.Message == "Book Category Updated Successfully")
            {
                _snackBar.Add(data.Message, Severity.Success);
            }
            else
            {
                _snackBar.Add(data.Message, Severity.Error);
            }
        }

        private async Task MenuRole(int book_id)
        {
            menuRoleModel.book_id = book_id;
            if (menuRoleModel.book_id != 0)
            {
                var data = await MenuRoleRepository.getIdAsync(menuRoleModel.book_id);
                menuRoleModel.menu = data.menu.ToList();
            }
        }

        private void check(bool id)
        {
            CheckSelected = id;
            if (id == true)
            {
                menuRoleModel.menu.ForEach(x =>
                {
                    x.IsActive = true;
                });
            }
            else
            {
                menuRoleModel.menu.ForEach(x =>
                {
                    x.IsActive = false;
                });
            }
        }
    }
}
