using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiClient.Requests
{
	public class ApiRequesterBase
	{
		private const string MEDIA_TYPE = "application/json";

		protected string BaseApiUrl { get; set; }

		protected async Task<TO> Get<TO>(string url)
		{
			var response = await GetFromWebApi(url);

			return await CreateWebApiResult<TO>(response);
		}

		private async Task<HttpResponseMessage> GetFromWebApi(string url)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE));

				try
				{
					var response = await client.GetAsync(url);

					return response.IsSuccessStatusCode? response : null;
				}
				catch
				{
					return null;
				}
			}
		}

		private async Task<TO> CreateWebApiResult<TO>(HttpResponseMessage response)
		{
			if (response == null)
				return default(TO);

			var content = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<TO>(content);
		}
	}
}