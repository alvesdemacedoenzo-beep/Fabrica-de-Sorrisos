namespace FabricaDeSorrisos.UI.ViewModels.Brinquedos
{
    public class BrinquedoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public string? ImagemUrl { get; set; }
        public int Estoque { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string? SubCategoria { get; set; }
        public string? Personagem { get; set; }
        public string FaixaEtaria { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public string Status => Ativo ? "Ativo" : "Inativo";
    }
}
