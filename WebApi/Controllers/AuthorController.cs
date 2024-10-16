using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(IBookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public IActionResult GetAuthorDetail(int id)
    {
        AuthorDetailViewModel result;
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
        query.AuthorId = id;
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();
        return Ok(result);
    }
    [HttpPost]
    public IActionResult CreateAuthor([FromBody] CreateAuthorModel Model)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
        command.Model = Model;
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel model)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
        command.AuthorId = id;
        command.Model = model;
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = id;
        DeleteAuthorCommandValidator validatior = new DeleteAuthorCommandValidator();
        validatior.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}