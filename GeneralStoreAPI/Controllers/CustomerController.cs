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
    public class CustomerController : ApiController
    {
        private readonly StoreDbContext _context = new StoreDbContext(); 
        //POST (create)
        // api/Customer
        //IHttpActionResult is an interface that captures all of the different response types.
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomer([FromBody] Customer model)
        {
            if (model is null)
            {
                return BadRequest("Your request body cannot be empty.");
            }

            //ModelState tells us the actual state of the model when sent to us
            if (ModelState.IsValid)
            {
                // Store the model in the database
                _context.Customers.Add(model);
                await _context.SaveChangesAsync();

                return Ok("Your customer was created");
            }

            // The model is not valid, go ahead and reject it
            // The model state in it will tell us everything that was wrong with it
            return BadRequest(ModelState);
        }
        //Get All Customers (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            // ToListAsync asyncronously turns the whole customers table into a list of customers
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }
        //Get a Customer by Id (GET)
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if(customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }


        // Update an existing Customer by its Id (PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            // Check matching ids
            if(id != updatedCustomer?.Id)
            {
                return BadRequest("Ids do not match.");
            }

            // Check the ModelState
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Find customer in db
            Customer customer = await _context.Customers.FindAsync(id);

            // If the restaurant does not exist then do something
            if (customer is null)
                return NotFound();

            // Update the property
            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Id = customer.Id;

            // Save the changes
            await _context.SaveChangesAsync();

            return Ok("The customer was updated");
        }

        // Delete an existing Customer by its Id (Delete)
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if(customer is null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);

            if(await _context.SaveChangesAsync() == 1)
            {
                return Ok("The custoemr was deleted");
            }

            return InternalServerError();
        }
    }
}
