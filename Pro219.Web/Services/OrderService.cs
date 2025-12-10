using Microsoft.AspNetCore.WebUtilities;
using Pro219.API.DTOs;
using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;

namespace Pro219.Web.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<string>> GetCheckoutUrl(
            string token,
            List<CheckoutModel> listProduct,
            decimal discountAmount = 0,
            decimal shippingFee = 0,
            string note = "",
            int shippingAddressId = -99,
            int? paymentMethodTypeId = 2,
            int? discountId = null)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "discountAmount", discountAmount.ToString() },
                { "shippingFee", shippingFee.ToString() },
                { "PaymentMethodTypeId", paymentMethodTypeId.ToString() },
                { "note", note },
                { "addressId", shippingAddressId.ToString() }
            };

            if (discountId.HasValue)
            {
                queryParams.Add("discountId", discountId.Value.ToString());
            }

            string baseUrl = "Order/Checkout";

            string url = QueryHelpers.AddQueryString(baseUrl, queryParams!);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (!string.IsNullOrEmpty(token))
            {
                var formatToken = token.Trim('"');
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
            }

            request.Content = JsonContent.Create(listProduct);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var resultUrl = await response.Content.ReadAsStringAsync();
                return ServiceResult<string>.Success(resultUrl);
            }
            else
            {
                var errorCode = await response.Content.ReadAsStringAsync();

                var errorMess = Constant.Errors.ContainsKey(errorCode ?? "")
                                    ? Constant.Errors[errorCode ?? ""]
                                    : $"Lỗi không xác định: {response.ReasonPhrase}";

                return ServiceResult<string>.Failure(
                    errorCode,
                    errorMess,
                    response.StatusCode.ToString()
                );
            }
        }

        public async Task<ServiceResult<List<Order>>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Order/GetAll");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Order>>();
                return ServiceResult<List<Order>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<Order>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Order>> GetById(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Order/GetById/{id}");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Order>();
                return ServiceResult<Order>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<Order>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<Order>>> GetAllByCustomerId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Order/GetByCustomerId/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Order>>();
                return ServiceResult<List<Order>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<Order>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<OrderItem>>> GetAllOrderItemByOrderId(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/OrderItem/GetByOrderId/{id}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<OrderItem>>();
                return ServiceResult<List<OrderItem>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<OrderItem>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Order>> UpdateStatus(Order order, string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "/Order/Update");

                if (!string.IsNullOrEmpty(token))
                {
                    var formatToken = token.Trim('"');
                    request.Headers.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", formatToken);
                }

                request.Content = JsonContent.Create(order);

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Order>();
                    return ServiceResult<Order>.Success(result);
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var errorMess = Constant.Errors.ContainsKey(result ?? "") 
                        ? Constant.Errors[result ?? ""] 
                        : $"Lỗi không xác định: {response.ReasonPhrase} (Status: {response.StatusCode})";
                    return ServiceResult<Order>.Failure(result, errorMess, response.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                return ServiceResult<Order>.Failure("EXCEPTION", $"Lỗi khi gọi API: {ex.Message}", "500");
            }
        }
    }
}
