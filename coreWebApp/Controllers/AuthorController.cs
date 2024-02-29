using coreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace coreWebApp.Controllers
{
    public class AuthorController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AuthorViewModel> authors = new List<AuthorViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Author");
                if (response.IsSuccessStatusCode)
                {
                    authors = await response.Content.ReadAsAsync<List<AuthorViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(authors);
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorViewModel authorViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PostAsJsonAsync("Author", authorViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Author");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditAuthor(int id)
        {
            AuthorViewModel authorViewModel = new AuthorViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Author/" + id);
                if (response.IsSuccessStatusCode)
                {
                    authorViewModel = await response.Content.ReadAsAsync<AuthorViewModel>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(authorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthor(AuthorViewModel authorViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PutAsJsonAsync("Author/" + authorViewModel.AuthorId, authorViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Author");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.DeleteAsync("Author/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Author");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View("Index", new List<AuthorViewModel>());
        }
    }
}
