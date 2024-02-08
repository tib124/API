using ConsumingAnimeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace ConsumingAnimeApi.Controllers
{
    public class AnimeController : Controller
    {
        private readonly HttpClient _Client;

        public AnimeController(IHttpClientFactory client)
        {
            _Client = client.CreateClient();
            _Client.BaseAddress = new Uri("https://localhost:7067/api");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<AnimeViewModel> animeslist = new List<AnimeViewModel>();
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/Anime/GetAll").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                animeslist = JsonConvert.DeserializeObject<List<AnimeViewModel>>(data);
            }

            return View(animeslist);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(AnimeViewModel anime)
        {
            try
            {
                string data = JsonConvert.SerializeObject(anime);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _Client.PostAsync(_Client.BaseAddress + "/Anime/AddAnime", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View();

            }
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AnimeViewModel anime = new();
                HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/Anime/GetById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    anime = JsonConvert.DeserializeObject<AnimeViewModel>(data);
                }
                return View(anime);
            }
            catch (Exception ex)
            {
                return View();
            }


        }

        [HttpPost]
        public IActionResult Edit(int id, AnimeViewModel anime)
        {
            string data = JsonConvert.SerializeObject(anime);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _Client.PutAsync(_Client.BaseAddress + "/Anime/UpdateAnime/"+ id,content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            AnimeViewModel anime = new();
            HttpResponseMessage response = _Client.GetAsync(_Client.BaseAddress + "/Anime/GetById/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                anime = JsonConvert.DeserializeObject<AnimeViewModel>(data);
            }
            return View(anime);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            HttpResponseMessage response = _Client.DeleteAsync(_Client.BaseAddress + "/Anime/DeleteAnime/" + id).Result;


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


}
}
