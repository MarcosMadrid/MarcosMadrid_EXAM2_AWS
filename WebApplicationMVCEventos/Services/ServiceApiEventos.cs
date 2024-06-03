using AWSServerlessApiEventos.Helpers;
using AWSServerlessApiEventos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApplicationMVCEventos.Services
{
    public class ServiceApiEventos
    {
        private MediaTypeWithQualityHeaderValue mediaType;
        private string UrlApiEventos;

        public ServiceApiEventos()
        {
            UrlApiEventos = JsonDocument.Parse(HelperSecretManager.GetSecretAsync().Result)
                .RootElement
                .GetProperty("UrlApiEventos")
                .GetString()!;
            mediaType = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Evento>> GetEventos()
        {
            string request = "/api/Eventos/GetEventos";
            return
                (await GetRequest<List<Evento>>(request))!;
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            string request = "/api/Eventos/GetCategorias";
            return
                (await GetRequest<List<Categoria>>(request))!;
        }

        public async Task<List<Evento>> GetEventosByCategory(int id)
        {
            string request = $"/api/Eventos/GetEventosByCategoria/{id}";
            return
                (await GetRequest<List<Evento>>(request))!;
        }

        private async Task<T?> GetRequest<T>(string request)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(mediaType);

                HttpResponseMessage message = await httpClient.GetAsync(UrlApiEventos + request);
                if (message.IsSuccessStatusCode)
                {
                    string stringResponse = await message.Content.ReadAsStringAsync();
                    T response = JsonConvert.DeserializeObject<T>(stringResponse)!;
                    return response;
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
