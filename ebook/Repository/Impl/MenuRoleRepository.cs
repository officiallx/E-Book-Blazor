using AutoMapper;
using ebook.Models;
using ebook.Repository.Interface;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ebook.Repository.Impl
{
    public class MenuRoleRepository : DapperRepoBase, IMenuRoleRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public MenuRoleRepository(IConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<MessageResponse> saveMenuRoleAsync(MenuRoleModel menuRole)
        {
            try
            {
                var saveData = JsonConvert.SerializeObject(menuRole.menu);
                var Response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("Exec spBookSubcategory @Flag='S',@saveData=@saveData,@BookId=@BookId",
                    new
                    {
                        BookId = menuRole.book_id,
                        saveData = saveData,

                    });

                return Response;
            }

            catch (SqlException ex)
            {
                var message = new MessageResponse();
                message.Message = ex.Message;
                return message;
            }
        }

        public async Task<MenuRoleModel> getIdAsync(int RoleId)
        {
            try
            {
                var model = new MenuRoleModel();

                var data = await this.GetQueryResultAsync<MenuList>("exec spBookSubcategory @Flag='Z',@BookId=@BookId",
                     new { BookId = RoleId });

                model.menu = data;
                return model;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
