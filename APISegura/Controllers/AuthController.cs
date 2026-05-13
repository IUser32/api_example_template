using APISegura.Data;
using APISegura.Models;
using APISegura.Models.Auth;
using APISegura.Seguridad;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISegura.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly SeguraDbContext _context;
    private readonly JwtService _jwt;
    private readonly PasswordHasher<Usuario> _hasher = new();

    public AuthController(SeguraDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("registrar")]
    public async Task<ActionResult<AuthResponse>> Registrar(RegistroRequest request)
    {
        if (await _context.Usuarios.AnyAsync(u => u.NombreUsuario == request.NombreUsuario))
        {
            return Conflict(new { mensaje = "El nombre de usuario ya esta en uso." });
        }

        if (await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo))
        {
            return Conflict(new { mensaje = "El correo ya esta registrado." });
        }

        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            NombreUsuario = request.NombreUsuario,
            Correo = request.Correo,
            Rol = "Usuario",
            FechaRegistro = DateTime.UtcNow
        };

        usuario.HashContrasena = _hasher.HashPassword(usuario, request.Contrasena);

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var (token, expira) = _jwt.GenerarToken(usuario);

        return Ok(new AuthResponse
        {
            Token = token,
            Expira = expira,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NombreUsuario == request.NombreUsuario);

        if (usuario is null)
        {
            return Unauthorized(new { mensaje = "Credenciales invalidas." });
        }

        var resultado = _hasher.VerifyHashedPassword(usuario, usuario.HashContrasena, request.Contrasena);

        if (resultado == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new { mensaje = "Credenciales invalidas." });
        }

        var (token, expira) = _jwt.GenerarToken(usuario);

        return Ok(new AuthResponse
        {
            Token = token,
            Expira = expira,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol
        });
    }
}
