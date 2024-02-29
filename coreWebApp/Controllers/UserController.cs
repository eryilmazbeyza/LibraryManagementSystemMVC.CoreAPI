using coreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;


namespace coreWebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.GetAsync("User");
                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<List<UserViewModel>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserViewModel userViewModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7191/api/");
                HttpResponseMessage response = await client.PostAsJsonAsync("User", userViewModel);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View();
        }
    }
}








//Diğer Kodlar
//    private readonly IHttpClientFactory _httpClientFactory;

//    [HttpPost]
//    public async Task<IActionResult> UserLogIn(UserViewModel userViewModel)
//    {
//        try
//        {
//            using (var client = _httpClientFactory.CreateClient())
//            {
//                client.BaseAddress = new Uri("https://localhost:7191/api/");

//                // API'ye log in bilgilerini gönderen bir HTTP POST isteği yapın
//                HttpResponseMessage response = await client.PostAsJsonAsync("User", userViewModel);

//                if (response.IsSuccessStatusCode)
//                {
//                    // Başarılı log in durumunda, gelen veriyi işleyebilirsiniz.
//                    // Örneğin, token veya başka bir bilgiyi alabilir ve saklayabilirsiniz.

//                    // Daha sonra başka bir sayfaya yönlendirin ya da giriş yapıldı mesajını gösterin
//                    return RedirectToAction("User");
//                }
//                else
//                {
//                    // Giriş başarısızsa hata mesajını gösterin
//                    ModelState.AddModelError(string.Empty, "Invalid username or password");
//                    return View();
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            // Hata oluştuğunda konsol çıktısına yazdır
//            Console.WriteLine($"Error in UserLogIn action: {ex.Message}");
//            throw; // Hatanın daha üst seviyede yönetilmesi için tekrar fırlat
//        }
//    }

