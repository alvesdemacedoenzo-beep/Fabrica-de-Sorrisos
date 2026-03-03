using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Application.Abstractions.Repositories;

public interface ICarrinhoRepository
{
    // Pega o carrinho completo do usuário (com os dados do brinquedo para mostrar foto/preço)
    Task<List<CarrinhoItem>> GetCarrinhoDoUsuarioAsync(int usuarioId);
    
    // Busca um item específico (para saber se já existe e só aumentar a quantidade)
    Task<CarrinhoItem?> GetItemAsync(int usuarioId, int brinquedoId);

    Task AddAsync(CarrinhoItem item);
    Task UpdateAsync(CarrinhoItem item);
    Task DeleteAsync(CarrinhoItem item);
    
    // Limpa o carrinho depois que comprar
    Task LimparCarrinhoAsync(int usuarioId);
}