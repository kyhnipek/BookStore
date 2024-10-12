using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken;

public class RefreshTokenCommand
{
    public string RefreshToken { get; set; }
    public readonly IBookStoreDbContext _context;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
    {
        _context = dbContext;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
        if (user is null)
        {
            throw new InvalidOperationException("Geçerli bir refresh token bulunamadı.");
        }
        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();
        return token;
    }
}