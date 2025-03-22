using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Text.Json;

namespace Northwind.Controllers;

public class ProductsController : Controller
{
    private readonly string baseUrl;
    private readonly string appJson;

    public ProductsController(IConfiguration config)
    {
        baseUrl = config.GetValue<string>("BaseUrl");
        appJson = config.GetValue<string>("AppJson");
    }

    private void ConfigClient(HttpClient client)
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appJson));
        client.BaseAddress = new Uri(baseUrl);
    }

    // GET: ProductsController
    public async Task<ActionResult> Index()
    {
        try
        {
            var products = new List<Product>();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(appJson));
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync("products/bycategory/1");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    products = JsonSerializer.Deserialize<List<Product>>(json);
                }
                else
                {
                    ViewBag.ErrorMessage = response.StatusCode.ToString();
                    return View("Error", new ErrorViewModel());
                }
            }

            return View(products);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    // GET: ProductsController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }
}
