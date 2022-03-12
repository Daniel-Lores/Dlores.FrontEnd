using Dlores.Ecommerce.Api.Data;
using Dlores.Ecommerce.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dlores.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly EcommerceDb _context;

        public CartController(EcommerceDb context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Create([FromBody] Cart newCart)
        {
            if (!_context.Cart.Any(p => p.Id == newCart.Id))
            {
                _context.Cart.Add(newCart);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database already has a cart with the Id {newCart.Id}");
            }
        }

        [HttpPut("{email}/{idProducto}")]
        public ActionResult Edit(string email, int productId, [FromBody] int quantity)
        {
            var cartToUpdate = _context.Cart.Include(c => c.Product).FirstOrDefault(c => c.Email == email && c.ProductId == productId);

            if (cartToUpdate!=null)
            {
                cartToUpdate.Quantity = quantity;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a cart with the Id {productId}");
            }
        }

        [HttpDelete("{email}/{ProductId}")]
        public ActionResult DeleteProduct(string email, int ProductId)
        {
            if (_context.Cart.Any(c => c.Email == email))
            {
                var CartToDelete = _context.Cart.Single(c => c.ProductId == ProductId);
                _context.Cart.Remove(CartToDelete);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest($"The database don´t have a product in the cart with the id {ProductId}");
            }
        }
        [HttpDelete("{email}")]
        public ActionResult DeleteCart(String email)
        {
            if (_context.Cart.Any(c => c.Email == email))
            {
                var EmptyCart = _context.Cart.Where(c => c.Email == email);
                _context.Cart.RemoveRange(EmptyCart);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"No hay productos con el email {email}");
            }


        }
    }
}
