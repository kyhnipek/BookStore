// using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBookDetailViewModel result;
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        try
        {
            query.Id = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
    }

    // [HttpGet]
    // public Book Get([FromQuery] string id)
    // {
    //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
    //     return book;
    // }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        try
        {
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            // ValidationResult result = validator.Validate(command);
            validator.ValidateAndThrow(command);

            // if (!result.IsValid)
            // {
            //     foreach (var item in result.Errors)
            //     {
            //         Console.WriteLine("Property: {0} - Error message: {1}", item.PropertyName, item.ErrorMessage);
            //     }
            // }
            // else
            // {
            command.Handle();
            // }


        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
    {
        UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        try
        {
            command.Id = id;
            command.Model = updatedBook;
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new DeleteBookCommand(_context);
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        try
        {
            command.Id = id;
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
        return Ok();
    }
}
