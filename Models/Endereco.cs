namespace DesafioClientes.Models;

public class Endereco
{
    public int EnderecoID { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Logradouro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Complemento { get; set; } = string.Empty;

    public int ClienteId { get; set; }
}
