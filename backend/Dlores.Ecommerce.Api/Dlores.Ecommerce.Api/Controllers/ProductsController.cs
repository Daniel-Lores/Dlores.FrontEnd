using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dlores.Ecommerce.Api.Data;
using Dlores.Ecommerce.Api.Models;

namespace Dlores.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly EcommerceDb _context;

        public ProductsController(EcommerceDb context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            if (_context.Product.Any())
            {
                return Ok(_context.Product);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            if (_context.Product.Any(p => p.Id == id))
            {
                return Ok(_context.Product.SingleOrDefault(p => p.Id == id));
            }
            else
            {
                return NotFound($"The database don´t have a product with the Id {id}");
            }
        }

        [HttpGet("/category/{Id}")]
        public ActionResult<IEnumerable<Product>> GetByCategoryId(int id)
        {
            if (_context.Product.Any(p => p.CategoryId == id))
            {
                return Ok(_context.Product.FirstOrDefault(p => p.CategoryId == id));
            }
            else
            {
                return NotFound($"The database don´t have a product with the categoryId {id}");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Product newProduct)
        {
            if (!_context.Product.Any(p => p.Id == newProduct.Id))
            {
                _context.Product.Add(newProduct);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database already has a product with the Id {newProduct.Id}");
            }
        }

        [HttpPut]
        public ActionResult Edit([FromBody] Product newProduct)
        {
            if (_context.Product.Any(p => p.Id == newProduct.Id))
            {
                var ProductToUpdate = _context.Product.Single(p => p.Id == newProduct.Id);
                _context.Product.Remove(ProductToUpdate);
                _context.Product.Add(newProduct);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a product with the Id {newProduct.Id}");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {

            if (_context.Product.Any(p => p.Id == id))
            {
                var ProductToDelete = _context.Product.Single(p => p.Id == id);
                _context.Product.Remove(ProductToDelete);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a product with the id {id}");
            }
        }
    }
}
