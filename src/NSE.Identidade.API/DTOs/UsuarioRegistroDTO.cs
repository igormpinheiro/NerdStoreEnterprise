using System.ComponentModel.DataAnnotations;

namespace NSE.Identidade.API.DTOs;

public class UsuarioRegistroDTO
{
    public UsuarioRegistroDTO(string email, string senha, string senhaConfirmacao)
    {
        Email = email;
        Senha = senha;
        SenhaConfirmacao = senhaConfirmacao;
    }
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")] 
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Senha { get; set; }
    
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string SenhaConfirmacao { get; set; }
}