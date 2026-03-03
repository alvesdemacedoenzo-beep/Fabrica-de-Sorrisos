namespace FabricaDeSorrisos.Application.DTOs
{
    public class DtoConversa
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string UltimaMensagem { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public bool TemMensagemNaoLida { get; set; }
    }
}