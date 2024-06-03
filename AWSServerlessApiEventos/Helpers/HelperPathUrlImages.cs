using System.Text.Json;

namespace AWSServerlessApiEventos.Helpers
{
    public class HelperPathUrlImages
    {       
        public static string ReturnUrlImgs()
        {
             return JsonDocument.Parse(HelperSecretManager.GetSecretAsync().Result)
            .RootElement
            .GetProperty("Urls3")
            .GetString()!;
        }
    }
}
