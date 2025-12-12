using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;
using static MudBlazor.Icons.Custom;

namespace Pro219.Web.Services
{
    public class ProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<ProductImage>>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/ProductImage/GetAll");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : result; 
                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProductImage>>> GetAllByProductId(int productId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/ProductImage/GetByProductId/{productId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : result; 
                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProductImage>>> GetAllOnlyPoductByProductId(int productId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/ProductImage/GetOnlyByProductId/{productId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : result; 
                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProductImage>>> GetAllByProductVariantId(int productVariantId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/ProductImage/GetByVariantId/{productVariantId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : result; 
                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProductImage>>> CreateAny(List<ProductImage> productImage)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/ProductImage/AddRange");

            request.Content = JsonContent.Create(productImage);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : result; 
                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProductImage>>> DeleteAny(List<int> ids, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/ProductImage/DeleteAny");

            if(!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            request.Content = JsonContent.Create(ids);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProductImage>>();
                return ServiceResult<List<ProductImage>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                
                var errorCode = result;

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                        : result; 

                return ServiceResult<List<ProductImage>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
