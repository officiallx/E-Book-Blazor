using ebook.Models.Author;
using ebook.Repository;
using ebook.Services.Interface;
using System.Data.SqlClient;
using AutoMapper;
using ebook.Models;

namespace ebook.Services.Impl
{
    public class AuthorService : DapperRepoBase, IAuthorService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AuthorService(IConfiguration configuration, IMapper mapper) : base(configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task deleteAuthorAsync(long Id)
        {
            await ExecuteAsync("exec spAuthor @Flag='D',@AuthorId=@AuthorId", new { AuthorId = Id });
        }

        public async Task<List<AuthorModel>> getAuthorAsync()
        {
            var response = await this.GetQueryResultAsync<AuthorModel>("exec spAuthor @Flag=@Flag",new { Flag='G'});
            return response;
        }

        public async Task<MessageResponse> saveAuthorAsync(AuthorModel author)
        {
            try
            {
                if (author.author_id==0)
                {
                    var response = await GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spAuthor @Flag='S', @AuthorName = @AuthorName",
                       new { AuthorName=author.author_name });
                    return response;
                }
                else
                {
                    var response = await GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spAuthor @Flag='U', @AuthorName = @AuthorName, @AuthorId = @AuthorId",
                       new { AuthorName = author.author_name, AuthorId = author.author_id });
                    return response;
                }

            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message);
                MessageResponse message = new();
                message.Message = ex.Message;
                return message;
            }
        }
    }
}
