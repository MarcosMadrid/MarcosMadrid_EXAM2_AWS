using AWSServerlessApiEventos.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplicationMVCEventos.Services;

namespace WebApplicationMVCEventos.Controllers
{
    public class EventosController : Controller
    {
        private readonly ServiceApiEventos serviceEventos;

        public EventosController(ServiceApiEventos context)
        {
            serviceEventos = context;
        }

        public async Task<IActionResult> Index()
        {
            Console.WriteLine(await serviceEventos.GetCategorias());
            ViewData["categorias"] = await serviceEventos.GetCategorias();
            return View(await serviceEventos.GetEventos());
        }

        [HttpGet]
        [Route("EventosCategoria/{id}")]
        public async Task<IActionResult> EventosCategoria(int id)
        {
            List<Evento> eventos = await serviceEventos.GetEventosByCategory(id);
            ViewData["categorias"] = await serviceEventos.GetCategorias();
            return View("Index", eventos);
        }
    }
}
