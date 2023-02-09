using ebook.Models;
using ebook.Repository.Interface;
using ebook.Services.Interface;

namespace ebook.Services.Impl
{
    public class MenuRoleServices : IMenuRoleServices
    {
        private readonly IMenuRoleRepository _menuRoleRepository;
        public MenuRoleServices(IMenuRoleRepository menuRoleRepository)
        {
            _menuRoleRepository = menuRoleRepository;
        }

        public async Task<MenuRoleModel> getIdAsync(int RoleId)
        {
            return (await _menuRoleRepository.getIdAsync(RoleId));
        }

        public async Task<MessageResponse> saveMenuRoleAsync(MenuRoleModel menuRole)
        {
            return await _menuRoleRepository.saveMenuRoleAsync(menuRole);
        }
    }
}
