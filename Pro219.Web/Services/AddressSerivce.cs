using Pro219.DAL.Models;
using Pro219.Web.Constants;
using Pro219.Web.DTOs;
using System.Collections.Generic;
using System.Net;

namespace Pro219.Web.Services
{
    public class AddressSerivce
    {
        private readonly HttpClient _httpClient;

        public AddressSerivce(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<Address>>> GetAll()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Address/GetAll");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<Address>>();
                return ServiceResult<List<Address>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<Address>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<Address>>> GetAllByCustomerId(int customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Address/GetByCustomerId/{customerId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync <List<Address>>();
                return ServiceResult<List<Address>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<Address>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<ProvinceDTO>>> GetAllProvince()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Address/GetAllProvinces");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<ProvinceDTO>>();
                return ServiceResult<List<ProvinceDTO>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<ProvinceDTO>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<DistrictDTO>>> GetAllDistrictByProvinceId(int provinceId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Address/GetAllDistrictsByProvinceId/{provinceId}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<DistrictDTO>>();
                return ServiceResult<List<DistrictDTO>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<DistrictDTO>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<List<WardDTO>>> GetAllWardByDistrictCode(int districtCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/Address/GetAllWardByDistrictCode/{districtCode}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<WardDTO>>();
                return ServiceResult<List<WardDTO>>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result];
                return ServiceResult<List<WardDTO>>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<Address>> Create(AddressModel address)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/Address/Add");

            request.Content = JsonContent.Create(address);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Address>();
                return ServiceResult<Address>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result ?? ""];
                return ServiceResult<Address>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }

        public async Task<ServiceResult<ShippingFeeDTO>> CaculateFee(CalculateFeeRequestModel calculateFeeRequestModel)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"/Address/CalculateFee?to_district_id={calculateFeeRequestModel.ToDistrictId}&to_ward_code={calculateFeeRequestModel.ToWardCode}");

            request.Content = JsonContent.Create(calculateFeeRequestModel.Items);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ShippingFeeDTO>();
                return ServiceResult<ShippingFeeDTO>.Success(result);
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                var errorMess = Constant.Errors[result ?? ""];
                return ServiceResult<ShippingFeeDTO>.Failure(result, errorMess, response.StatusCode.ToString());
            }
        }
    }
}
