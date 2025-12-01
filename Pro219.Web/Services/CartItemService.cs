using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;

namespace Pro219.Web.Services
{
    public class CartItemService
    {
        private readonly HttpClient _httpClient;

        public CartItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<CartDTO>>> GetAllCartItemWithDetailByCartId(int cartId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/CartItem/get-all-cart-item-with-detail/{cartId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<CartDTO>>();
                return ServiceResult<List<CartDTO>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<CartDTO>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<bool> Delete(int id, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/CartItem/Delete/{id}");

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
