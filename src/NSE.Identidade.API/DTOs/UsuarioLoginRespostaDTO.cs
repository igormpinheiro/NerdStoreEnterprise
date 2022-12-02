namespace NSE.Identidade.API.DTOs;

public class UsuarioLoginRespostaDTO
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UsuarioTokenDTO UsuarioToken { get; set; }
}

public class UsuarioTokenDTO
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaimDTO> Claims { get; set; }
}

public class UsuarioClaimDTO
{
    public string Value { get; set; }
    public string Type { get; set; }
}