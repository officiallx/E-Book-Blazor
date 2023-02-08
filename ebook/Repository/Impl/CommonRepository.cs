using ebook.Models.General;
using ebook.Repository.Interface;

namespace ebook.Repository.Impl
{
    public class CommonRepository : DapperRepoBase, ICommonRepository
    {
        private readonly IConfiguration _configuration;

        public CommonRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<DropDownModel>> GetDropDownAsync(string ddlType)
        {
            var ddl = await GetQueryResultAsync<DropDownModel>(
                 "exec spDropDown @ddlType=@ddlType"
                 , new { ddlType });

            return ddl;
        }
    }
}
