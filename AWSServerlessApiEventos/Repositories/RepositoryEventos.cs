using AWSServerlessApiEventos.Data;
using AWSServerlessApiEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace AWSServerlessApiEventos.Repositories
{
    public class RepositoryEventos
    {
        private EventosContext context;

        public RepositoryEventos(EventosContext context)
        {
            this.context = context;
        }

        public async Task<List<Evento>> GetEventos()
        {
            return
                await context.Eventos.ToListAsync();
        }

        public async Task<Evento?> GetEvento(int id)
        {
            return
                await context.Eventos
                .FirstOrDefaultAsync(evt => evt.Id.Equals(id));
        }

        public async Task<Categoria?> GetCategoria(int id)
        {
            return
                await context.Categorias
                .FirstOrDefaultAsync(cat => cat.Id == id);
        }

        public async Task CreteEvento(string name, string artist, int idcategory, string? image)
        {
            Evento evento = new Evento()
            {
                Id = await GetMaxIdEvento() + 1,
                Name = name,
                Artist = artist,
                IdCategory = idcategory,
                Image = image
            };
            context.Eventos.Add(evento);
            await context.SaveChangesAsync();
        }

        public async Task UpdateEvento(int id, string name, string artist, int idcategory, string image)
        {
            Evento? evento = await GetEvento(id);
            if (evento != null)
            {
                evento.Name = name;
                evento.Artist = artist;
                evento.IdCategory = idcategory;
                evento.Image = image;
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            List<Categoria> categorias = await context.Categorias.ToListAsync();
            if (categorias == null)
            {
                return new List<Categoria>();
            }
            return categorias;
        }

        public async Task<List<Evento>> GetEventosCategory(int idCategory)
        {
            List<Evento> eventos = await context.Eventos
                .Where(evt => evt.IdCategory.Equals(idCategory))
                .ToListAsync();

            if (eventos == null)
            {
                return new List<Evento>();
            }
            return eventos;
        }

        public async Task DeleteEvento(int id)
        {
            Evento? evento = await context.Eventos.FirstOrDefaultAsync(evt => evt.Id.Equals(id));
            if (evento != null)
            {
                context.Remove(evento);
                await context.SaveChangesAsync();
            }
        }

        internal async Task<int> GetMaxIdEvento()
        {
            return
                await context.Eventos
                .Select(evt => evt.Id).MaxAsync();
        }
    }
}
