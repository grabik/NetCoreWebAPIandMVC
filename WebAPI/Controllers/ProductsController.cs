using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IDataRepository<Products, long> _iRepo;
        public ProductsController(IDataRepository<Products, long> repo)
        {
            _iRepo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Products> Get()
        {
            return _iRepo.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Products Get(int id)
        {
            return _iRepo.Get(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody] Products product)
        {
            _iRepo.Add(product);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Products product)
        {
            _iRepo.Update(product.ProductsId, product);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _iRepo.Delete(id);
        }
    }
}