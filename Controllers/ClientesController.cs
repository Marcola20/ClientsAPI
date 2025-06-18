using AutoMapper;
using DesafioClientes.Data;
using DesafioClientes.DTOs;
using DesafioClientes.Models;
using DesafioClientes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioClientes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICepService _cepService;

    public ClientesController(AppDbContext context, IMapper mapper, ICepService cepService)
    {
        _context = context;
        _mapper = mapper;
        _cepService = cepService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAll()
    {
        var clientes = await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<ClienteDto>>(clientes));
    }

    [HttpGet("{clienteId}")]
    public async Task<ActionResult<ClienteDto>> GetById(int clienteId)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Endereco)
            .Include(c => c.Contatos)
            .FirstOrDefaultAsync(c => c.ClienteID == clienteId);

        if (cliente == null)
            return NotFound(new { erro = "Cliente não encontrado." });

        return Ok(_mapper.Map<ClienteDto>(cliente));
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Create(ClienteDto dto)
    {
        try
        {
            var cliente = _mapper.Map<Cliente>(dto);

            var enderecoApi = await _cepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);
            enderecoApi.Numero = dto.Endereco.Numero;
            enderecoApi.Complemento = dto.Endereco.Complemento;
            cliente.Endereco = enderecoApi;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { clienteId = cliente.ClienteID }, _mapper.Map<ClienteDto>(cliente));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPut("{clienteId}")]
    public async Task<IActionResult> Update(int clienteId, ClienteDto dto)
    {
        try
        {
            if (clienteId != dto.ClienteID) return BadRequest();

            var clienteDb = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.ClienteID == clienteId);

            if (clienteDb == null) return NotFound();

            _mapper.Map(dto, clienteDb);

            var enderecoApi = await _cepService.ObterEnderecoPorCepAsync(dto.Endereco.Cep);
            enderecoApi.Numero = dto.Endereco.Numero;
            enderecoApi.Complemento = dto.Endereco.Complemento;
            enderecoApi.ClienteID = clienteDb.ClienteID;

            clienteDb.Endereco = enderecoApi;

            _context.Update(clienteDb);
            await _context.SaveChangesAsync();

            var atualizado = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.ClienteID == clienteId);

            return Ok(_mapper.Map<ClienteDto>(atualizado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{clienteId}")]
    public async Task<IActionResult> Delete(int clienteId)
    {
        try
        {
            var cliente = await _context.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Contatos)
                .FirstOrDefaultAsync(c => c.ClienteID == clienteId);

            if (cliente == null)
                return NotFound(new { erro = "Cliente não encontrado." });

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Cliente excluído com sucesso." });
        }
        catch (Exception)
        {
            return StatusCode(500, new { erro = "Erro interno ao excluir o cliente." });
        }
    }

}
