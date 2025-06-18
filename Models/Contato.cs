namespace DesafioClientes.Models;

public class Contato
{
    public int ContatoID { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;

    public int ClienteID { get; set; }
}
