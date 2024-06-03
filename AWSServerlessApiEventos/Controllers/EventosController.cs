using AWSServerlessApiEventos.Helpers;
using AWSServerlessApiEventos.Models;
using AWSServerlessApiEventos.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AWSServerlessApiEventos.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private RepositoryEventos repositoryEventos;

        public EventosController(RepositoryEventos repositoryEventos)
        {
            this.repositoryEventos = repositoryEventos;
        }

        // GET: api/<EventosController>
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento?>> Get(int id)
        {
            return
                await repositoryEventos.GetEvento(id);
        }

        // GET: api/<EventosController>
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            List<Evento> eventos = await repositoryEventos.GetEventos();
            foreach (var item in eventos)
            {
                item.Image = HelperPathUrlImages.ReturnUrlImgs() + item.Image;
            }
            return eventos;
        }

        // GET: api/<EventosController>
        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            return
                await repositoryEventos.GetCategorias();
        }

        // GET: api/<EventosController>
        [HttpGet("{idcategory}")]
        public async Task<ActionResult<List<Evento>>> GetEventosByCategoria(int idcategory)
        {
            List<Evento> eventos = await repositoryEventos.GetEventosCategory(idcategory);
            foreach (var item in eventos)
            {
                item.Image = HelperPathUrlImages.ReturnUrlImgs() + item.Image;
            }
            return eventos;
        }

        // POST api/<EventosController>
        [HttpPost]
        public async Task PostEvento([FromBody] Evento evento)
        {
            await repositoryEventos.CreteEvento(evento.Name!, evento.Artist!, evento.IdCategory!.Value, evento.Image);
        }

        // PUT api/<EventosController>
        [HttpPut]
        public async Task PutEvento([FromBody] Evento evento)
        {
            await repositoryEventos.UpdateEvento(evento.Id, evento.Name!, evento.Artist!, evento.IdCategory!.Value, evento.Image!);
        }

        // DELETE api/<EventosController>
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await repositoryEventos.DeleteEvento(id);
        }
    }
}
