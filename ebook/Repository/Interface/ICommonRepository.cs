using ebook.Models.General;

namespace ebook.Repository.Interface
{
    public interface ICommonRepository
    {
        Task<List<DropDownModel>> GetDropDownAsync(string DDL_TYPE);

    }
}
