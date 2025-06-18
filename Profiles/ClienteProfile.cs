using AutoMapper;
using DesafioClientes.DTOs;
using DesafioClientes.Models;

namespace DesafioClientes.Profiles;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDto>().ReverseMap();
        CreateMap<Endereco, EnderecoDto>().ReverseMap();
        CreateMap<Contato, ContatoDto>().ReverseMap();
    }
}
