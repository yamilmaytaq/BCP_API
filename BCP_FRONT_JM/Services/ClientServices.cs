using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCP.Shared.Models.DTO;

namespace BCP_FRONT_JM.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;

        public ClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BD_CLIENTES_DTO>> GetClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BD_CLIENTES_DTO>>("api/clientes");
        }

        public async Task<BD_CLIENTES_DTO> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BD_CLIENTES_DTO>($"api/clientes/{id}");
        }

        public async Task<bool> CreateClientAsync(BD_CLIENTES_CREATE_DTO client)
        {
            var response = await _httpClient.PostAsJsonAsync("api/clientes", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(int id, BD_CLIENTES_UPDATE_DTO client)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/clientes/{id}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
    
