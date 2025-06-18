using DesafioClientes.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DesafioClientes.Services;

public class CepService : ICepService
{
    private readonly HttpClient _httpClient;

    public CepService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Endereco> ObterEnderecoPorCepAsync(string cep)
    {
        var url = $"https://viacep.com.br/ws/{cep}/json/";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new ArgumentException("CEP em formato inválido.");

            var resultadoRaw = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(resultadoRaw);
            if (doc.RootElement.TryGetProperty("erro", out var erroProp) &&
                (erroProp.ValueKind == JsonValueKind.True ||
                 (erroProp.ValueKind == JsonValueKind.String && erroProp.GetString()?.ToLower() == "true")))
            {
                throw new ArgumentException("CEP não encontrado.");
            }

            var resultado = JsonSerializer.Deserialize<ViaCepResponse>(resultadoRaw);

            return new Endereco
            {
                Cep = resultado?.Cep ?? "",
                Logradouro = resultado?.Logradouro ?? "",
                Cidade = resultado?.Localidade ?? ""
            };
        }
        catch (JsonException)
        {
            throw new ArgumentException("Erro ao processar resposta do ViaCEP.");
        }
    }

    private class ViaCepResponse
    {
        [JsonPropertyName("cep")]
        public string Cep { get; set; } = string.Empty;
        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } = string.Empty;
        [JsonPropertyName("localidade")]
        public string Localidade { get; set; } = string.Empty;
    }
}
