namespace DesafioClientes.Models;

public class Cliente
{
    public int ClienteID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string DataCadastro { get; set; } = string.Empty;
    public Endereco Endereco { get; set; } = new();
    public List<Contato> Contatos { get; set; } = new();
}
