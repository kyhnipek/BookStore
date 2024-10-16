using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;

public class TokenHandler
{
    public IConfiguration Configuration { get; set; }
    public TokenHandler(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        tokenModel.Expiration = DateTime.Now.AddMinutes(15);
        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: Configuration["Token:Issuer"],
            audience: Configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now, //token üretildikten ne kadar süre sonra devreye gireceğini belirler, now ile hemen.
            signingCredentials: credentials
        );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        //token yaratılıyor.
        tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken = CreateRefreshToken();
        return tokenModel;
    }
    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}