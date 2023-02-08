using ebook.Models.General;
using ebook.Repository.Interface;
using ebook.Services.Interface;

namespace ebook.Services.Impl
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        public CommonService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }
        public async Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE)
        {
            var ddl = await _commonRepository.GetDropDownAsync(DDL_TYPE);
            return ddl;
        }
    }
}
