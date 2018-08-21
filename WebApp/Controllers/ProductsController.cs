using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCApp.Models;
using Newtonsoft.Json;

namespace MVCApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductsDTO> dto = new List<ProductsDTO>();

            var client = _clientFactory.CreateClient("prod");

            HttpResponseMessage res = await client.GetAsync("api/products");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;

                dto = JsonConvert.DeserializeObject<List<ProductsDTO>>(result);
            }
            return View(dto);
        }

        // GET: Products/Create  
        public async Task<IActionResult> Create()
        {
            List<CategoriesDTO> dto = new List<CategoriesDTO>();
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = await client.GetAsync("api/categories");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<CategoriesDTO>>(result);
            }

            dto.Insert(0, new CategoriesDTO { CategoriesId = 0, Name = "--Select--" });

            ViewBag.ListofCategories = dto;

            return View();
        }

        // POST: Products/Create  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductsDTO product)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("prod");

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PostAsync("api/products", content).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }



            return View(product);
        }

        // GET: Products/Edit/1  
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ProductsDTO> dto = new List<ProductsDTO>();
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = await client.GetAsync("api/products");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<ProductsDTO>>(result);
            }

            var product = dto.SingleOrDefault(m => m.ProductsId == id);
            if (product == null)
            {
                return NotFound();
            }

            List<CategoriesDTO> dto1 = new List<CategoriesDTO>();
            var client1 = _clientFactory.CreateClient("prod");
            HttpResponseMessage res1 = await client.GetAsync("api/categories");

            if (res.IsSuccessStatusCode)
            {
                var result = res1.Content.ReadAsStringAsync().Result;
                dto1 = JsonConvert.DeserializeObject<List<CategoriesDTO>>(result);
            }

            dto1.Insert(0, new CategoriesDTO { CategoriesId = 0, Name = "--Select--" });

            ViewBag.ListofCategories = dto1;

            return View(product);
        }

        // POST: Products/Edit/1  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, ProductsDTO product)
        {
            if (id != product.ProductsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("prod");

                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                HttpResponseMessage res = client.PutAsync($"api/products/{id}", content).Result;

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        // GET: Products/Delete/1  
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<ProductsDTO> dto = new List<ProductsDTO>();
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = await client.GetAsync("api/products");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                dto = JsonConvert.DeserializeObject<List<ProductsDTO>>(result);
            }

            var product = dto.SingleOrDefault(m => m.ProductsId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5  
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var client = _clientFactory.CreateClient("prod");
            HttpResponseMessage res = client.DeleteAsync($"api/products/{id}").Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}