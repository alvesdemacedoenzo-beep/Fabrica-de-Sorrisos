namespace FabricaDeSorrisos.UI.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Senha { get; set; } = string.Empty;

        public string TipoUsuario { get; set; } = string.Empty;
    }
}
