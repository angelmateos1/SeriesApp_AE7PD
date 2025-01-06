using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesApp.Helpers;
using SeriesApp.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace SeriesApp.Services
{
        public class SeriesServices : ISeriesService
        {
            private readonly HttpClient client;

            public SeriesServices()
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServerUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue
                    ("application/json"));
            }


            public async Task<bool> Add<T>(string endpoint, T s)
            {
                var response = await
                    client.PostAsJsonAsync(endpoint, s);

                return response.IsSuccessStatusCode;
            }

            public async Task<T> Get<T>(string endpoint, int id) where T : class
            {
                var fullEndpoint = endpoint + "/" + id;
                var response = await client.GetAsync(fullEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.
                        ReadFromJsonAsync<T>();
                    return data;
                }
                else
                    return (default);
            }

            public async Task<List<T>> GetAll<T>(string endpoint) where T : class
            {
                var response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var data = await
                        response.Content
                        .ReadFromJsonAsync<List<T>>();

                    return data;
                }
                else
                    return (default);
            }

                    public async Task<bool> Update<T>(string endpoint, int id, T s)
        {
            var fullEndpoint = endpoint + "/" + id;
            var response = await client.PutAsJsonAsync(fullEndpoint, s);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete<T>(string endpoint, int id)
        {
            var fullEndpoint = endpoint + "/" + id;
            var response = await client.DeleteAsync(fullEndpoint);

            return response.IsSuccessStatusCode;
        }
        }
    }
