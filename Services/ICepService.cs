using DesafioClientes.Models;

namespace DesafioClientes.Services;

public interface ICepService
{
    Task<Endereco> ObterEnderecoPorCepAsync(string cep);
}
