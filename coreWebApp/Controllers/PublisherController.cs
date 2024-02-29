using coreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace coreWebApp.Controllers
{
    public class PublisherController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PublisherViewModel> publishers = new List<PublisherViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Publisher");
                if (response.IsSuccessStatusCode)
                {
                    publishers = await response.Content.ReadAsAsync<List<PublisherViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(publishers);
        }

        [HttpGet]
        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherViewModel publisherViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PostAsJsonAsync("Publisher", publisherViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Publisher");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditPublisher(int id)
        {
            PublisherViewModel publisherViewModel = new PublisherViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Publisher/" + id);
                if (response.IsSuccessStatusCode)
                {
                    publisherViewModel = await response.Content.ReadAsAsync<PublisherViewModel>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(publisherViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPublisher(PublisherViewModel publisherViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PutAsJsonAsync("Publisher/" + publisherViewModel.PublisherId, publisherViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Publisher");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.DeleteAsync("Publisher/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Publisher");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View("Index", new List<PublisherViewModel>());
        }
    }
}
