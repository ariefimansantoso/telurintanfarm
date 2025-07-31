using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using QuickAccounting.Data.Recording;

namespace QuickAccounting.Services
{
	public class OpenAiService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey = "";
		private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";

		public OpenAiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<string> AnalyzeFarmDataAsync(dynamic records)
		{
			var payload = new
			{
				model = "gpt-4.1-mini",
				temperature = 0.2,
				messages = new[]
				{
					new {
						role = "user",
						content = $"Berikut ini adalah data recording saya:\n\n{JsonSerializer.Serialize(new { records })}\n\n" +
								  "Tolong berikan analisis berikut:\n1. Deteksi anomali pada Feed Intake dan Hen Day\n" +
								  "2. Tolong berikan ringkasan performa\n3. Rekomendasikan beberapa poin perbaikan."
					}
				}
			};

			var request = new HttpRequestMessage(HttpMethod.Post, _apiUrl);
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
			request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

			var response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
			}

			using var stream = await response.Content.ReadAsStreamAsync();
			using var doc = await JsonDocument.ParseAsync(stream);

			var content = doc.RootElement
				.GetProperty("choices")[0]
				.GetProperty("message")
				.GetProperty("content")
				.GetString();

			return content;
		}
	}
}
