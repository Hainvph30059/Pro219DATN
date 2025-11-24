// Pro219.Web/Services/ProductService.cs
using Azure;
using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Pro219.Web.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductShowDto>> GetTopNewestProductAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ProductShowDto>>("Product/product-show");
                return result ?? new List<ProductShowDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return new List<ProductShowDto>();
            }
        }

        public async Task<List<ProductShowDto>> GetBestSellerAsync()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<ProductShowDto>>("Product/best-seller");
                return result ?? new List<ProductShowDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return new List<ProductShowDto>();
            }
        }


        public async Task<ProductShowDto?> GetProductByIdAsync(int id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ProductShowDto>($"Product/GetById/{id}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy dữ liệu: {ex.Message}");
                return null;
            }
        }

        public async Task<ServiceResult<ProductDetailDto>> GetProductDetail(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/detail/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ProductDetailDto>();
                return ServiceResult<ProductDetailDto>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<ProductDetailDto>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}