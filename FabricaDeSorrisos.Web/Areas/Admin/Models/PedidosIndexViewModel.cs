using FabricaDeSorrisos.Domain.Entities;

namespace FabricaDeSorrisos.Web.Areas.Admin.Models;

public class PedidosIndexViewModel
{
    public List<Pedido> Pendentes { get; set; } = new();
    public List<Pedido> Finalizados { get; set; } = new();
}
