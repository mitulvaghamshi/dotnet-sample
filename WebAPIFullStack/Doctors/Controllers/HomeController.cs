using Doctors.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Doctors.Controllers;

public class HomeController : Controller
{
    private static readonly MediaTypeWithQualityHeaderValue mediaType = new("application/json");

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    public async Task<IActionResult> Index()
    {
        try
        {
            var doctors = await HttpRequest<List<Doctor>>(async (client, decode, _) =>
                await decode(await client.GetAsync("physicians")));

            return View(doctors);
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Description = e.Message
            });
        }
    }

    // GET: DoctorsController/Details/5
    public async Task<ActionResult> Details(int id)
    {
        try
        {
            var doctor = await HttpRequest<Doctor>(async (client, decode, _) =>
                await decode(await client.GetAsync($"physicians/{id}")));

            if (doctor != null && doctor.PhysicianId > 0) return View(doctor);

            return View("Error", new ErrorViewModel
            {
                RequestId = id.ToString(),
                Description = $"Unable to find a doctor with given id: {id}"
            });
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

    // GET: DoctorsController/Create
    public ActionResult Create() => View();

    // POST: DoctorsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Doctor doctor)
    {
        if (!ModelState.IsValid) return View(doctor);

        try
        {
            await HttpRequest<Doctor>(async (client, _, encode) =>
            {
                await client.PostAsync("physicians", encode(doctor));
                return doctor;
            });

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Description = e.Message
            });
        }
    }

    // GET: DoctorsController/Edit/5
    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            var doctor = await HttpRequest<Doctor>(async (client, decode, _) =>
                await decode(await client.GetAsync($"physicians/{id}")));

            if (doctor != null && doctor.PhysicianId > 0) return View(doctor);

            return View("Error", new ErrorViewModel
            {
                RequestId = id.ToString(),
                Description = $"Unable to find a doctor with given id: {id}"
            });
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

    // POST: DoctorsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, Doctor doctor)
    {
        if (id != doctor.PhysicianId || !ModelState.IsValid) return View(doctor);

        try
        {
            await HttpRequest<Doctor>(async (client, _, encode) =>
            {
                await client.PutAsync($"physicians/{id}", encode(doctor));
                return doctor;
            });

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Description = e.Message
            });
        }
    }

    // GET: DoctorsController/Delete/5
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var doctor = await HttpRequest<Doctor>(async (client, decode, _) =>
                await decode(await client.GetAsync($"physicians/{id}")));

            if (doctor != null && doctor.PhysicianId > 0) return View(doctor);

            return View("Error", new ErrorViewModel
            {
                RequestId = id.ToString(),
                Description = $"Unable to find a doctor with given id: {id}"
            });
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

    // POST: DoctorsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id, Doctor doctor)
    {
        try
        {
            await HttpRequest<HttpResponseMessage>(async (client, _, _) =>
                await client.DeleteAsync($"physicians/{id}"));

            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Description = e.Message
            });
        }
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    private static async Task<T> HttpRequest<T>(Builder<T> builder)
    {
        var client = new HttpClient { BaseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost", 5002).Uri };
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(mediaType);

        return await builder(client, decoder, encoder);

        static async Task<T> decoder(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Unable to parse response data.");
        }

        static StringContent encoder(T @object) =>
            new(JsonSerializer.Serialize(@object), Encoding.UTF8, mediaType.MediaType);
    }

    private delegate Task<T> Builder<T>(HttpClient client, [Optional] Decode<T, HttpResponseMessage> decode, [Optional] Encode<StringContent, T> encode);

    private delegate Task<T> Decode<T, in E>(E response);

    private delegate T Encode<T, in E>(E @object);
}
