namespace ITOps.ViewModelComposition.Json
{
    using System.Dynamic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class HttpContentExtensions
    {
        public static async Task<ExpandoObject> AsExpandoAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<ExpandoObject>(await content.ReadAsStringAsync());
        }

        public static async Task<ExpandoObject[]> AsExpandoArrayAsync(this HttpContent content)
        {
            return JsonConvert.DeserializeObject<ExpandoObject[]>(await content.ReadAsStringAsync());
        }
    }
}