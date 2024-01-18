using Rainfall.Api.Models.Response;
using System.IO;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace Rainfall.Api.Services
{
    public class RainfallPublicService
    {
        public async Task<RainfallReadingResponse> GetReadingsByStation(string stationId, int limit = 10)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            RainfallPublicReading responseContent = null;
            HttpResponseMessage response = await client.GetAsync($"id/stations/{stationId}/readings?_sorted&_limit=10");
            if (response.IsSuccessStatusCode)
            {
                responseContent = await response.Content.ReadAsAsync<RainfallPublicReading>();
            }

            List<RainfallReading> result = new List<RainfallReading>();
            if (responseContent != null && responseContent.Items != null && responseContent.Items.Count > 0)
            {
                foreach (var item in responseContent.Items)
                {
                    RainfallReading reading = new RainfallReading
                    {
                        DateMeasured = item.DateTime,
                        AmountMeasured = item.Value
                    };

                    result.Add(reading);
                }
            }

            return new RainfallReadingResponse { Readings = result};
        }
    }
}
