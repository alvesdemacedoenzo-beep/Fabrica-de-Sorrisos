using FabricaDeSorrisos.UI.Models;

public static class RoleHelper
{
    public static bool IsAdmin => UserSession.Role == "Admin";
    public static bool IsGerente => UserSession.Role == "Gerente";
    public static bool IsCliente => UserSession.Role == "Cliente";
}
