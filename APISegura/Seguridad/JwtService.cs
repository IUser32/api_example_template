using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APISegura.Models;
using Microsoft.IdentityModel.Tokens;

namespace APISegura.Seguridad;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime Expira) GenerarToken(Usuario usuario)
    {
        var jwt = _configuration.GetSection("Jwt");
        var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);
        var expira = DateTime.UtcNow.AddMinutes(int.Parse(jwt["ExpiraEnMinutos"]!));

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
            new(JwtRegisteredClaimNames.Email, usuario.Correo),
            new(ClaimTypes.Role, usuario.Rol),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: expira,
            signingCredentials: credenciales);

        return (new JwtSecurityTokenHandler().WriteToken(token), expira);
    }
}
