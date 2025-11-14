using Pro219.Web.DTOs;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace Pro219.Web.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string HashPassword(string password)
        {
            //4297F44B13955235245B2497399D7A93 
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
    }
}
