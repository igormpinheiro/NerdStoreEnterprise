﻿using System.ComponentModel.DataAnnotations;

namespace NSE.Identidade.API.DTOs;

public class NewUserDTO
{
    public NewUserDTO(string email, string password, string confirmPassword)
    {
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
    
    public NewUserDTO() { }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido")] 
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; } = null!;
    
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    [Compare("Password", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmPassword { get; set; } = null!;
}