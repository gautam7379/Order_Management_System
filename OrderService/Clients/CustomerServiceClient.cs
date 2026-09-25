namespace OrderService.Clients
{
    public class CustomerServiceClient
    {
        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            var response =
                await _httpClient.GetAsync($"/api/customers/{customerId}");

            return response.IsSuccessStatusCode;
        }
    }
}