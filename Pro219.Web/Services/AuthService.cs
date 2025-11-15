using Microsoft.AspNetCore.Identity.Data;
using Pro219.Web.DTOs;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Pro219.Web.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string HashPassword(string password)
        {
            // 4297F44B13955235245B2497399D7A93 (123123)
            // 26dc318942685872cf79c5eb96c9bb13 (Admin@12345)
            // b855e41c5c5f5061ecba4fd8613a7760 (User@12345)
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            md5.Clear();
            return sb.ToString();

        }

        public async Task<LoginResponseDTO> LoginCustomer(string username, string password)
        {
            var loginRequest = new LoginModel
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            var response = await _httpClient.PostAsJsonAsync("/Access/LoginCustomer", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return responseDTO ?? new LoginResponseDTO { LoginSuccess = false };
            }
            else
            {
                return new LoginResponseDTO { LoginSuccess = false };
            }
        }

        public async Task<LoginResponseDTO> LoginStaff(string username, string password)
        {
            var loginRequest = new LoginModel
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            var response = await _httpClient.PostAsJsonAsync("/Access/LoginStaff", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return responseDTO ?? new LoginResponseDTO { LoginSuccess = false };
            }
            else
            {
                return new LoginResponseDTO { LoginSuccess = false };
            }
        }

        public async Task<GetMeResponseDTO> AccessCheck(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/Access/Check");

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<GetMeResponseDTO>();
                return responseDTO;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<GetMeResponseDTO>();
                return responseDTO ?? new GetMeResponseDTO { IsExpired = true };
            }
            else
            {
                var responseDTO = await response.Content.ReadFromJsonAsync<GetMeResponseDTO>();
                return responseDTO ?? new GetMeResponseDTO { IsExpired = true };
            }
        }
    }
}
