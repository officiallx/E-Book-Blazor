using ebook.Models;

namespace ebook.Repository.Interface
{
    public interface IMenuRoleRepository
    {
        Task<MessageResponse> saveMenuRoleAsync(MenuRoleModel menuRole);
        Task<MenuRoleModel?> getIdAsync(int RoleId);
    }
}
