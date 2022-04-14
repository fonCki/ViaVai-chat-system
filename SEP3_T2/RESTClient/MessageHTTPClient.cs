using System.Text;
using System.Text.Json;
using Entities;

namespace RESTClient;

public static class MessageHTTPClient {

    public static async Task<string> AddMessage(string messageAsJson) {
        using HttpClient client = new();
        StringContent content = new(messageAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/Message/Write", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode) {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }

        return responseContent;
    }
}