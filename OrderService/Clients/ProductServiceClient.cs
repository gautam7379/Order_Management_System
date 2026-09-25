using System.Net.Http.Json;

namespace OrderService.Clients
{
    public class ProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductResponse?> GetProductAsync(int productId)
        {
            var response =
                await _httpClient.GetAsync($"/api/products/{productId}");

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductResponse>();
        }
    }

    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}