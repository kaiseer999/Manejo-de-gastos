using Dapper;
using Manejo_de_gastos.Models;
using Manejo_de_gastos.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Manejo_de_gastos.Controllers
{
    public class TipoCuentasController : Controller
    {
        private readonly IRepositorioTipoCuenta repositorioTipoCuenta;

        public TipoCuentasController(IRepositorioTipoCuenta repositorioTipoCuenta)
        {
            this.repositorioTipoCuenta = repositorioTipoCuenta;
        }


        [HttpGet]
        public IActionResult Crear()
        {
          
            return View();
        }


        [HttpPost]
        public async Task <IActionResult> Crear(TipoCuentaDTO tipoCuenta) 
        {
            if (!ModelState.IsValid) { 

                return View(tipoCuenta);

            }

            tipoCuenta.UsuarioId = 1;

            var yaExiste = await repositorioTipoCuenta.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExiste)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe.");

                return View(tipoCuenta);
            }


            await repositorioTipoCuenta.Crear(tipoCuenta);

            return View();
        
        }

[AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;

            try
            {
                var yaExisteTipoCuenta = await repositorioTipoCuenta.Existe(nombre, usuarioId);

                if (yaExisteTipoCuenta)
                {
                    return Json($"El nombre {nombre} ya existe");
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                // Manejo de la excepción (registrar, etc.)
                return StatusCode(500, "Ocurrió un error al verificar el tipo de cuenta.");
            }
        }




    }
}
