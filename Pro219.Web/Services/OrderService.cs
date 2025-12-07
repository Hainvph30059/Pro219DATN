using Microsoft.AspNetCore.WebUtilities;
using Pro219.API.DTOs;
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
            int? paymentMethodTypeId = 2,
            int? discountId = null)
        {
            var queryParams = new Dictionary<string, string?>
            {
                { "discountAmount", discountAmount.ToString() },
                { "shippingFee", shippingFee.ToString() },
                { "PaymentMethodTypeId", paymentMethodTypeId.ToString() },
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
    }
}
