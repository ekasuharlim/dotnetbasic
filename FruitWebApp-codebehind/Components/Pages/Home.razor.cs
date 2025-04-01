using Microsoft.AspNetCore.Components;
using FruitWebApp.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Web;

namespace FruitWebApp.Components.Pages;

public partial class Home : ComponentBase
{
    // IHttpClientFactory set using dependency injection 
    [Inject]
    public required IHttpClientFactory HttpClientFactory { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    /* Add the data model, enumerable since an array is expected as a response */
    private IEnumerable<FruitModel>? _fruitList;

    // Begin GET operation code
    protected override async Task OnInitializedAsync()
    {
        // Create the HTTP client using the FruitAPI named factory
        var httpClient = HttpClientFactory.CreateClient("FruitAPI");

        // Retrieve record information
        using HttpResponseMessage response = await httpClient.GetAsync("/fruits");

        if (response.IsSuccessStatusCode)
        {
            using var contentStream = await response.Content.ReadAsStreamAsync();
            _fruitList = await JsonSerializer.DeserializeAsync<IEnumerable<FruitModel>>(contentStream);
            
        }
        else
        {
            Console.WriteLine("Failed to retrieve fruits. Status code: {response.StatusCode}");
        }

    }

    // End GET operation code

    private void DeleteButton(int id) => NavigationManager!.NavigateTo($"/delete/{id}");
    private void EditButton(int id) => NavigationManager!.NavigateTo($"/edit/{id}");

}