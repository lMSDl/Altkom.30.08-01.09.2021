using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Net.Http;

namespace Services.WebApi
{
    public class Service<T> : IAsyncService<T> where T : Entity
    {
        private HttpClient _client;
        private string RoutePrefix { get; set; }


        public Service(string baseAddress, string routePrefix) : this(baseAddress)
        {
            RoutePrefix = routePrefix;
        }

        public Service(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<int> CreateAsync(T entity)
        {
            var response = await _client.PostAsJsonAsync(RoutePrefix, entity);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<int>() : 0;
        }

        public async Task<T> ReadAsync(int id)
        {
            var response = await _client.GetAsync($"{RoutePrefix}/{id}");
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<T>() : null;
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            var response = await _client.GetAsync(RoutePrefix);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsAsync<IEnumerable<T>>() : null;
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var response = await _client.PutAsJsonAsync($"{RoutePrefix}/{id}", entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{RoutePrefix}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}