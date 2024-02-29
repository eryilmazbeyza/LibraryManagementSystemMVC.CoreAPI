using coreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace coreWebApp.Controllers
{
    public class LoanController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LoanViewModel> loans = new List<LoanViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Loan");
                if (response.IsSuccessStatusCode)
                {
                    loans = await response.Content.ReadAsAsync<List<LoanViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(loans);
        }

        [HttpGet]
        public async Task<IActionResult> Loanlist()
        {
            List<LoanViewModel> loans = new List<LoanViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("Loan");
                if (response.IsSuccessStatusCode)
                {
                    loans = await response.Content.ReadAsAsync<List<LoanViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(loans);
        }


        [HttpGet]
        public IActionResult BorrowBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(LoanViewModel loanViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PostAsJsonAsync("Loan", loanViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Loan");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReturnBook(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.DeleteAsync("Loan/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Loan");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View("Index", new List<LoanViewModel>());
        }
    }
}
