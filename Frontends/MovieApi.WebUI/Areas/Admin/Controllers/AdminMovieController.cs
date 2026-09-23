using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApi.Dto.Dtos.AdminCategoryDtos;
using MovieApi.Dto.Dtos.AdminMovieDtos;
using Newtonsoft.Json;
using System.Text;

namespace MovieApi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMovieController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MovieList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7047/api/Movies/GetMovieWithCategory");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AdminResultMovieDto>>(jsonData);
                return View(values);
            }


            return View();
        }




        [HttpGet]
        public async Task<IActionResult> CreateMovie()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7047/api/Categories");
            if(responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<AdminResultCategoryDto>>(jsonData);
                ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");
            }

            return View();
        }







        [HttpPost]
        public async Task<IActionResult> CreateMovie(AdminCreateMovieDto adminCreateMovieDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(adminCreateMovieDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7047/api/Movies", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieList");
            }
            
            return View();
        }



    }
}
