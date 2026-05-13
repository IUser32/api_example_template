using APIAvanzados.Models;
using FluentValidation;

namespace APIAvanzados.Validators;

public class ProductoValidator : AbstractValidator<Producto>
{
    public ProductoValidator()
    {
        RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

        RuleFor(p => p.Precio)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.")
            .LessThanOrEqualTo(9999999).WithMessage("El precio excede el maximo permitido.");

        RuleFor(p => p.Existencia)
            .GreaterThanOrEqualTo(0).WithMessage("La existencia no puede ser negativa.");

        RuleFor(p => p.CategoriaId)
            .GreaterThan(0).WithMessage("Debe indicar una categoria valida.");

        RuleFor(p => p.ProveedorId)
            .GreaterThan(0).WithMessage("Debe indicar un proveedor valido.");
    }
}
