using Dlores.Ecommerce.Api.Data;
using Dlores.Ecommerce.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dlores.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly EcommerceDb _context;

        public CategoriesController(EcommerceDb context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            if (_context.Category.Any())
            {
                return Ok(_context.Category);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            if (_context.Category.Any(c => c.Id == id))
            {
                return Ok(_context.Category.SingleOrDefault(c => c.Id == id));
            }
            else
            {
                return NotFound($"The database don´t have a category with the Id {id}");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody] Category newCategory)
        {
            if (!_context.Category.Any(p => p.Id == newCategory.Id))
            {
                _context.Category.Add(newCategory);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database already has a category with the Id {newCategory.Id}");
            }
        }

        [HttpPut]
        public ActionResult Edit([FromBody] Category newCategory)
        {
            if (_context.Category.Any(c => c.Id == newCategory.Id))
            {
                var CategoryToUpdate = _context.Category.Single(c => c.Id == newCategory.Id);
                _context.Category.Remove(CategoryToUpdate);
                _context.Category.Add(newCategory);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a category with the Id {newCategory.Id}");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {

            if (_context.Category.Any(c => c.Id == id))
            {
                var CategoryToDelete = _context.Category.Single(c => c.Id == id);
                _context.Category.Remove(CategoryToDelete);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a category with the id {id}");
            }
        }

    }
}
