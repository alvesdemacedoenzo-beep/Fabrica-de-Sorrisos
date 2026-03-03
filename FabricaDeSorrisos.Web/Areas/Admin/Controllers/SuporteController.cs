using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FabricaDeSorrisos.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Administrador,ADMINISTRADOR,Gerente")]
    public class SuporteController : Controller
    {
        // Ação que carrega a tela principal do Chat no Admin
        public IActionResult Index()
        {
            return View();
        }
    }
}
