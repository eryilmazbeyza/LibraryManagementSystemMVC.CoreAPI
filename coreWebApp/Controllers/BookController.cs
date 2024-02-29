using coreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace coreWebApp.Controllers
{
    public class BookController : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            List<BookViewModel> books = new List<BookViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");

                HttpResponseMessage response = await client.GetAsync($"Book?searchBy={searchBy}&search={search}");

                if (response.IsSuccessStatusCode)
                {
                    books = await response.Content.ReadAsAsync<List<BookViewModel>>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occurred");
                }
            }

            if (searchBy == "Genre")
            {
                books = books.Where(x => x.Genre == search || search == null).ToList();
            }
            else
            {
                books = books.Where(x => x.Title == search || search == null).ToList();
            }

            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> BookList()
        {
            List<BookViewModel> books = new List<BookViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Book");
                if (response.IsSuccessStatusCode)
                {
                    books = await response.Content.ReadAsAsync<List<BookViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(books);
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel bookViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PostAsJsonAsync("Book", bookViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Book");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            BookViewModel bookViewModel = new BookViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Book/"+id);
                if (response.IsSuccessStatusCode)
                {
                    bookViewModel = await response.Content.ReadAsAsync<BookViewModel>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookViewModel bookViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PutAsJsonAsync("Book/" + bookViewModel.BookId, bookViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Book");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.DeleteAsync("Book/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Book");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View("Index",new List<BookViewModel>());
        }
    }
}
