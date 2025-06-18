namespace DesafioClientes.DTOs;

public class ClienteDto
{
    public int ClienteID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;
    public EnderecoDto Endereco { get; set; } = new();
    public List<ContatoDto> Contatos { get; set; } = new();
}
