using Azure;
using EntityLayer.ViewModels;
using KuaforSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace KuaforSite.Controllers
{
    public class PostsController : Controller
    {
        private readonly HttpClient _httpClient;
        private string apiUrl => "https://localhost:7265/api/post";

        public PostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? selectedHairdresser)
        {
            var response = new HttpResponseMessage();
            if (selectedHairdresser.IsNullOrEmpty())
            {
                response = await _httpClient.GetAsync(apiUrl + "/GetAllPosts");
            }
            else
            {
                response = await _httpClient.GetAsync(apiUrl + $"/GetPostsByHairdresser/hairdresser_id={int.Parse(selectedHairdresser)}");
            }
            
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var postList = System.Text.Json.JsonSerializer.Deserialize<List<PostVM>>(jsonContent,options);
                
                var responseHairdressers = await _httpClient.GetAsync(apiUrl + "/GetHairdressers");
                if (responseHairdressers.IsSuccessStatusCode)
                {
                    string jsonContentHairdressers = await responseHairdressers.Content.ReadAsStringAsync();
                    var optionsHairdressers = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };


                    var hairdresserList = System.Text.Json.JsonSerializer.Deserialize<List<HairdresserForPostVM>>(jsonContentHairdressers, optionsHairdressers);
                    var selectList = hairdresserList.Select(hairdresser => new SelectListItem
                    {
                        Value = hairdresser.Id.ToString(),  
                        Text = hairdresser.HairdresserName      
                    }).ToList();
                    ViewBag.Hairdressers = selectList;
                    ViewBag.Username = User.Identity!.Name;
                }
                else
                {
                    ViewData["GetPostMessage"] = "Kuaför verileri alınamadı, sayfanızı yenileyin!";
                }//
                return View(postList);
            }
            ViewData["GetPostMessage"] = "Api hata verdi!";
            return View(new List<PostVM>());
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> NewPost()
        {
            var response = await _httpClient.GetAsync(apiUrl+ "/GetHairdressers");
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };


                var hairdresserList = System.Text.Json.JsonSerializer.Deserialize<List<HairdresserForPostVM>>(jsonContent,options);
                var selectList = hairdresserList.Select(hairdresser => new SelectListItem
                {
                    Value = hairdresser.Id.ToString(), 
                    Text = hairdresser.HairdresserName            
                }).ToList();
                ViewBag.Hairdressers = selectList;
                ViewBag.Username = User.Identity!.Name;
            }
            else
            {
                TempData["PostMessage"] = "Kuaför verileri alınamadı, sayfanızı yenileyin!";
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewPost(PostAddModel post)
        {
            var fileUrl = await SaveFileAndGetItsUrlAsync(post.ImageUrl);
            var postAddVM = new PostAddVM()
            {
                UserName = post.UserName,
                HairdresserId = post.HairdresserId,
                ImageUrl = fileUrl,
                Detail = post.Detail
            };
            if (!ModelState.IsValid)
            {
                TempData["PostMessage"] = "Model doğrulanamadı!";
                return RedirectToAction("NewPost","Posts");
            }
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            var jsonContent = JsonConvert.SerializeObject(postAddVM);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var postUserName = HttpUtility.UrlEncode(postAddVM.UserName);
            var response = await _httpClient.PostAsync(apiUrl+$"/CreatePost/UserName={postUserName}", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["PostMessage"] = "Ekleme işlemi başarılı.";
                return RedirectToAction("NewPost","Posts");
            }
            TempData["PostMessage"] = "Api hizmerinde bir sorun oluştu!";
            return RedirectToAction("NewPost", "Posts");
        }
        public async Task<string> SaveFileAndGetItsUrlAsync(IFormFile file)
        {
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img/posts");

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/img/posts/{fileName}";
        }
    }
}
