using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;

namespace Pro219.Web.Services
{
    public class BrandService
    {
        private readonly HttpClient _httpClient;

        public BrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<Brand>> Create(BrandModel brand)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Brand/Add");

            request.Content = JsonContent.Create(brand);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Brand>();
                return ServiceResult<Brand>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<Brand>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
