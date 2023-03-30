using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly string baseUrl = "https://localhost:44394/api/";
        private readonly string mimeType = "application/json";

        // GET: ProductsController
        [AllowAnonymous]
        public async Task<ActionResult> Index(int? categoryId = 1)
        {
            try
            {
                var categories = await GetAsync<IEnumerable<Category>>("Categories");

                ViewBag.Categories = new SelectList(categories, "categoryId", "categoryName", categoryId);

                var products = await GetAsync<IEnumerable<Product>>($"Products/ByCategory/{categoryId}");

                if (products == null)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = categoryId?.ToString(),
                        Description = $"Unable to find products with category id {categoryId}."
                    });
                }

                return View(products);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = categoryId?.ToString(),
                    Description = e.Message
                });
            }
        }

        // GET: ProductsController/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id?.ToString(),
                    Description = "Missing product ID, please check your URL"
                });
            }

            try
            {
                var product = await GetAsync<Product>($"Products/{id}");

                if (product == null)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = id?.ToString(),
                        Description = $"Unable to find product with id {id}."
                    });
                }

                var category = await GetAsync<Category>($"Categories/GetCategoryBy/{product.categoryId}");

                ViewBag.Title = product.productName;
                ViewBag.CategoryName = category.categoryName;

                return View(product);
            }
            catch (Exception e)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = e.Message
                });
            }
        }

        private async Task<T> GetAsync<T>(string requestUri)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mimeType));
                client.BaseAddress = new Uri(baseUrl);

                var response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(json);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return default;
        }
    }
}
