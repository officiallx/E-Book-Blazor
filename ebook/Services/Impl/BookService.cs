﻿using ebook.Models;
using ebook.Models.Books;
using ebook.Repository;
using ebook.Services.Interface;
using System.Data.SqlClient;

namespace ebook.Services.Impl
{
    public class BookService : DapperRepoBase, IBookService
    {
        private readonly IConfiguration _configuration;
        public BookService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task deleteBooksAsync(long Id)
        {
            await ExecuteAsync("exec spBooksList @Flag='D',@BookId=@BookId", new { BookId = Id });
        }

        public async Task<List<BooksModel>> getBooksAsync()
        {
            var response = await this.GetQueryResultAsync<BooksModel>("exec spBooksList @Flag=@Flag", new { Flag = 'G' });
            return response;
        }

        public async Task<List<EBookModel>> getEBooksAsyncByBookId(int bookId)
        {
            var response = await this.GetQueryResultAsync<EBookModel>("exec spEbook @Flag=G,@BookId=@BookId", new { BookId=bookId });
            return response;
        }

        public async Task<List<BooksModel>> getProductFilteredAsync(string bookName)
        {
            var response = await this.GetQueryResultAsync<BooksModel>("exec spBooksList @Flag='GF',@Name=@Name", new { Name = bookName });
            return response;
        }

        public async Task<MessageResponse> saveBooksAsync(BooksModel books)
        {
            string Message = "";
            try
            {
                if (books.book_id == 0)
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spBooksList @Flag='S', @ISBN=@ISBN,@Title=@Title,@PAGES=@PAGES,@PublicationDate=@PublicationDate,@AuthorId=@AuthorId,@BookCover=@BookCover",
                       new { ISBN = books.isbn, Title = books.title, PAGES = books.num_pages,PublicationDate = books.publication_date,AuthorId = books.author_id, BookCover=books.book_cover });
                    return response;
                }
                else
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spBooksList @Flag='U', @ISBN=@ISBN,@Title=@Title,@PAGES=@PAGES,@PublicationDate=@PublicationDate,@BookId = @BookId,@AuthorId=@AuthorId,@BookCover=@BookCover",
                       new { ISBN = books.isbn, Title = books.title, PAGES = books.num_pages,PublicationDate = books.publication_date,BookId = books.book_id, AuthorId = books.author_id, BookCover = books.book_cover });
                    return response;
                }

            }
            catch (SqlException ex)
            {
                MessageResponse message = new();
                message.Message = ex.Message;
                return message;
            }
        }

        public async Task<MessageResponse> saveBooksImageAsync(EBookModel books)
        {
            string Message = "";
            try
            {
                if (books.ebook_id == 0)
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spEbook @Flag='I',@BookImage=@BookImage,@PageNum=@PageNum,@BookId=@BookId",
                       new { BookImage=books.book_image, PageNum=books.page_num, BookId=books.book_id });
                    return response;
                }
                else
                {
                    var response = await this.GetQueryFirstOrDefaultResultAsync<MessageResponse>("exec spEbook @Flag='U', @BookImage=@BookImage,@PageNum=@PageNum,@BookId=@BookId",
                       new { BookImage = books.book_image, PageNum = books.page_num, BookId = books.book_id });
                    return response;
                }

            }
            catch (SqlException ex)
            {
                MessageResponse message = new();
                message.Message = ex.Message;
                return message;
            }
        }
    }
}
