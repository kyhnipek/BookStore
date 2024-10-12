using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;

public class CreateTokenCommand
{
    public CreateTokenModel Model { get; set; }
    public readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CreateTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public Token Handle()
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
        if (user is null)
        {
            throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı!");
        }
        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);

        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
        _context.SaveChanges();

        return token;
    }
}
public class CreateTokenModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}