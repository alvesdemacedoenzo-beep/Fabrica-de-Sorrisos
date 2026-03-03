using System.Collections.Generic;

namespace FabricaDeSorrisos.Web.Areas.Admin.Models;

public class DashboardViewModel
{
    // Cards
    public int QtdBrinquedos { get; set; }
    public int QtdUsuarios { get; set; }
    public int QtdPedidos { get; set; }
    public decimal FaturamentoTotal { get; set; }

    // Gráfico 1: Categorias
    public string[] GraficoColunasLabels { get; set; }
    public int[] GraficoColunasValores { get; set; }

    // Gráfico 2: Brinquedos Mais Vendidos (Mudamos o nome aqui)
    public string[] GraficoBrinquedosLabels { get; set; }
    public int[] GraficoBrinquedosValores { get; set; }

    // Listas
    public List<ItemPopular> MaisPopulares { get; set; }
    public List<ItemRecente> MaisRecentes { get; set; }
}

public class ItemPopular
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
}

public class ItemRecente
{
    public string Nome { get; set; }
    public string Data { get; set; }
}