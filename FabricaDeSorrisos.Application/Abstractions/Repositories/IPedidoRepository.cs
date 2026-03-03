using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface IPedidoRepository
{
    Task AddAsync(Pedido pedido);
    Task<List<Pedido>> GetPedidosDoUsuarioAsync(int usuarioId);
    Task<Pedido?> GetPedidoPorIdAsync(int id);

    // ADICIONE ESTA LINHA:
    Task<List<Pedido>> GetTodosPedidosAsync();

    // ADICIONE ESTA LINHA:
    Task UpdateAsync(Pedido pedido);
}