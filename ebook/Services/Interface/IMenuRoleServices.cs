using ebook.Models;

namespace ebook.Services.Interface
{
    public interface IMenuRoleServices
    {
        Task<MessageResponse> saveMenuRoleAsync(MenuRoleModel menuRole);
        Task<MenuRoleModel?> getIdAsync(int RoleId);
    }
}
