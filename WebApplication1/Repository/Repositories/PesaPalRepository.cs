using Newtonsoft.Json;
using Person.DTOs;
using Person.PesaPalRepository;
using System.Net.Http.Headers;
using System.Text;

namespace Person.PesaPalRepository
{
    public class PesaPalRepository : IPesaPalRepository
    {
        private readonly HttpClient _httpClient;

        public PesaPalRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<PesapalAuthResponse> RequestTokenAsync(PesapalAuthRequest request)
        {
            try
            {
                var Url = "/pesapalv3/api/Auth/RequestToken";
                // Convert request object to JSON
                var jsonRequest = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Make the HTTP POST request to the Pesapal Auth API
                var response = await _httpClient.PostAsync(Url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    // Parse the JSON response into PesapalAuthResponse DTO
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PesapalAuthResponse>(jsonResponse);
                }
                else
                {
                    // Handle error response
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<PesapalAuthResponse>(jsonResponse);
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<RegisterIPNResponseDTO> RegisterIPNUrlAsync(RegisterIPNRequestDTO request)
        {
            var url = "/pesapalv3/api/URLSetup/RegisterIPN";
            var accessToken = "YOUR_ACCESS_TOKEN"; // Replace with actual access token

            try
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.PostAsync(url, requestContent);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RegisterIPNResponseDTO>(jsonResponse);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<RegisterIPNResponseDTO>(jsonResponse);
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GetIPNListResponseDTO>> GetIPNListAsync(string accessToken)
        {
            var url = "/pesapalv3/apiURLSetup/GetIpnList";

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<GetIPNListResponseDTO>>(jsonResponse);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    // Handle error response
                    Console.WriteLine($"Error response: {jsonResponse}");
                    return new List<GetIPNListResponseDTO>();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<SubmitOrderResponseDTO> SubmitOrderRequestAsync(string accessToken, SubmitOrderRequestDTO request)
        {
            var url = "/pesapalv3/api/Transactions/SubmitOrderRequest";
           

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var jsonRequest = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SubmitOrderResponseDTO>(jsonResponse);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    // Handle error response
                    Console.WriteLine($"Error response: {jsonResponse}");
                    return new SubmitOrderResponseDTO();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        public async Task<TransactionStatusDTO> GetTransactionStatusAsync(string accessToken, string orderTrackingId)
        {
            var url = $"/pesapalv3/api/Transactions/GetTransactionStatus?orderTrackingId={orderTrackingId}";

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TransactionStatusDTO>(jsonResponse);
                }
                else
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    // Handle error response
                    Console.WriteLine($"Error response: {jsonResponse}");
                    return new TransactionStatusDTO();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
