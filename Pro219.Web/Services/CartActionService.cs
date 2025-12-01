using Pro219.Web.DTOs;
using Blazored.LocalStorage;
using Pro219.Web.Constants;
using System.Text.Json;

namespace Pro219.Web.Services
{
    public class CartDetailDTO
    {
        public List<CartItemWithProductDTO> CartItems { get; set; } = new List<CartItemWithProductDTO>();
        public int TotalItemCount => CartItems.Count > 0 ? CartItems.Sum(i => i.Quantity) : 0;
        public decimal TemporaryTotal => CartItems.Sum(i => i.Quantity * i.UnitPrice) ?? 0;
    }

    public class CartStateService
    {
        private readonly HttpClient _httpClient;

        private readonly ILocalStorageService _localStorage;

        private const string AuthTokenKey = Constant.TokenNameLocalStorage;

        private const string AuthTokenExpiryKey = Constant.TokenExpiredLocalStorage;

        private const string GuestCartDataKey = Constant.GuestCartLocalStorage;

        private const string AuthCartIdKey = Constant.AuthCartIdLocalStorage;

        public CartDetailDTO CartDetails { get; private set; } = new CartDetailDTO();

        public bool IsLoadingDetails { get; private set; } = false;

        public event Action OnCountChange;

        public event Action OnRequestLoadDetails;

        public CartStateService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        private async Task<bool> IsUserAuthenticatedAsync()
        {
            try
            {
                var token = await _localStorage.GetItemAsStringAsync(AuthTokenKey);
                if (string.IsNullOrEmpty(token)) return false;

                var expiryString = await _localStorage.GetItemAsStringAsync(AuthTokenExpiryKey);
                if (string.IsNullOrEmpty(expiryString)) return false;

                if (DateTime.TryParse(expiryString.Trim('"'), out DateTime expiryTime))
                {
                    return expiryTime > DateTime.Now;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<string> GetCurrentCartIdAsync()
        {
            bool isAuthenticated = await IsUserAuthenticatedAsync();

            if (isAuthenticated)
            {
                var authCartId = await _localStorage.GetItemAsStringAsync(AuthCartIdKey);
                return authCartId?.Trim('"');
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task SetAuthCartIdAsync(string cartId)
        {
            if (!string.IsNullOrEmpty(cartId))
            {
                await _localStorage.SetItemAsStringAsync(AuthCartIdKey, cartId);
            }
        }

        public async Task LoadCartDetailsAsync(bool forceReload = false)
        {
            if (IsLoadingDetails && !forceReload) return;

            IsLoadingDetails = true;
            OnCountChange?.Invoke();

            bool isAuthenticated = await IsUserAuthenticatedAsync();

            if (isAuthenticated)
            {
                try
                {
                    var authCartId = await GetCurrentCartIdAsync();

                    if (string.IsNullOrEmpty(authCartId))
                    {
                         CartDetails = new CartDetailDTO();
                    } else
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, $"/CartItem/get-all-cart-item-with-detail/{authCartId}");

                        var response = await _httpClient.SendAsync(request);

                        if(response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadFromJsonAsync<List<CartItemWithProductDTO>>();
                            CartDetails.CartItems = result ?? new List<CartItemWithProductDTO>();
                        } else
                        {
                            CartDetails = new CartDetailDTO();
                        }
                    }
                }
                catch (Exception ex)
                {
                    CartDetails = new CartDetailDTO();
                }
            } else
            {
                try
                {
                    var dataJson = await _localStorage.GetItemAsStringAsync(GuestCartDataKey);
                    if (!string.IsNullOrEmpty(dataJson))
                    {
                        var deserializedData = JsonSerializer.Deserialize<CartDetailDTO>(dataJson);
                        CartDetails.CartItems = deserializedData?.CartItems ?? new List<CartItemWithProductDTO>();
                    }
                    else
                    {
                        CartDetails = new CartDetailDTO();
                    }
                }
                catch (Exception)
                {
                    CartDetails = new CartDetailDTO();
                }
            }

            IsLoadingDetails = false;
            OnCountChange?.Invoke();
        }

        public void RequestOpenAndLoadCart()
        {
            OnRequestLoadDetails?.Invoke();
        }
    }
}
