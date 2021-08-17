using GeneralStoreAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Models
{
    public class TransactionController : ApiController
    {
        private readonly StoreDbContext _context = new StoreDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostTransaction([FromBody] Transaction model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            Product product = await _context.Products.FindAsync(model.Sku);

            if (product is null)
            {
                return BadRequest("This product does not exist");
            }

            if (product.IsInStock == false)
            {
                return BadRequest("This product is out of stock");
            }

            if (product.NumberInInventory < model.ItemCount)
            {
                return BadRequest("There is not enough of this product in stock to complete your transaction.");
            }


            //ModelState tells us the actual state of the model when sent to us
            if (ModelState.IsValid)
            {
                // Store the model in the database
                _context.Transactions.Add(model);
                int amountOfStockLeft = product.NumberInInventory - model.ItemCount;
                product.NumberInInventory = amountOfStockLeft;
                await _context.SaveChangesAsync();

                return Ok("Your transaction was completed");
            }

            // The model is not valid, go ahead and reject it
            // The model state in it will tell us everything that was wrong with it
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);

            if (transaction != null)
            {
                return Ok(transaction);
            }

            return NotFound();
        }
    }
}
