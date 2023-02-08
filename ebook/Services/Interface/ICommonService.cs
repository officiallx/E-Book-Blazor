using ebook.Models.General;

namespace ebook.Services.Interface
{
    public interface ICommonService
    {
        Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE);

    }
}
