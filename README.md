# ClientsAPI

A aplicação permite gerenciar **clientes**, seus **endereços** (com consulta automática via ViaCEP) e **contatos**.

## 🛠 Tecnologias e ferramentas

- ASP.NET Core 8 (Web API)
- Entity Framework Core (InMemory)
- AutoMapper
- Consumo de API externa (ViaCEP)
- Swagger / Postman para testes

## 📚 Funcionalidades

- `GET /api/clientes`: lista todos os clientes
- `GET /api/clientes/{clienteId}`: busca um cliente por ID
- `POST /api/clientes`: cadastra um novo cliente
- `PUT /api/clientes/{clienteId}`: atualiza dados de um cliente existente
- `DELETE /api/clientes/{clienteId}`: remove um cliente

### Detalhes:
- O endereço do cliente é obtido automaticamente a partir do CEP informado, usando a API [ViaCEP](https://viacep.com.br).
- Caso o CEP seja inválido ou inexistente, uma mensagem de erro adequada é retornada.
- Todos os endpoints usam DTOs para transferência de dados, com mapeamento feito via AutoMapper.

## ▶️ Como executar

1. Clone o repositório:
   ```bash
   git clone https://github.com/Marcola20/ClientsAPI.git
