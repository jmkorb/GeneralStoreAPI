using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly StoreDbContext _context = new StoreDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> PostProduct([FromBody] Product model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            //ModelState tells us the actual state of the model when sent to us
            if (ModelState.IsValid)
            {
                // Store the model in the database
                _context.Products.Add(model);
                await _context.SaveChangesAsync();

                return Ok("Your product was created");
            }

            // The model is not valid, go ahead and reject it
            // The model state in it will tell us everything that was wrong with it
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetBySku([FromUri]string sku)
        {
            Product product = await _context.Products.FindAsync(sku);
            
            if(product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}
