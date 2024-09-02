using BCP.Shared.Models.DTO;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using BCP.Shared.Models;

namespace BCP_FRONT_JM.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BD_USUARIOS_DTO>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BD_USUARIOS_DTO>>("api/usuarios");
        }

        public async Task<BD_USUARIOS_DTO> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BD_USUARIOS_DTO>($"api/usuarios/{id}");
        }

        public async Task<bool> CreateUserAsync(BD_USUARIOS_CREATE_DTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(int id, BD_USUARIOS_UPDATE_DTO user)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/usuarios/{id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string usuario, string contrasenia)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios/login", new { Usuario = usuario, Contrasenia = contrasenia });
            return response.IsSuccessStatusCode;
        }
    }
}




