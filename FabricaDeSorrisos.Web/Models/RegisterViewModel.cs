using System.ComponentModel.DataAnnotations;

namespace FabricaDeSorrisos.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessage = "O nome completo é obrigatório")]
    public string NomeCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "A senha deve ter no mínimo {2} caracteres.", MinimumLength = 8)]
    // REGEX PODEROSA:
    // (?=.*[a-z]) -> Pelo menos uma letra minúscula
    // (?=.*[A-Z]) -> Pelo menos uma letra maiúscula
    // (?=.*\d)    -> Pelo menos um número
    // (?=.*[\W_]) -> Pelo menos um caractere especial (!, @, #, etc.)
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve conter: Letra Maiúscula, Minúscula, Número e Caractere Especial.")]
    public string Senha { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Display(Name = "Confirmar senha")]
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; } = string.Empty;
}