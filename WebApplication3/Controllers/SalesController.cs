using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Interfaces;
using WebApplication3.Model;
using WebApplication3.Utility;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAttribute(RoleName ="Customer")]
    public class SalesController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public SalesController(IProductRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("AddProduct")]
        [AllowAnonymous]
        public IActionResult AddProduct([FromBody]Product product)
        {
            var result = _repository.Add(product);
            if (result == null)
            {
                return BadRequest(new { message = "Product cannot be added" });
            }
            return Ok();
        }

        [HttpGet("GetAllProducts")]
        [AllowAnonymous]
        public IActionResult GetAllProducts()
        {
            var result = _repository.GetProducts();
            if (result == null)
            {
                return BadRequest(new { message = "Product cannot be retrieved" });
            }
            return Ok(result);
        }

        [HttpPost("UpdateProduct")]
        [AllowAnonymous]
        public IActionResult UpdateProduct([FromBody]Product product)
        {
            var result = _repository.Update(product);
            if (result == null)
            {
                return BadRequest(new { message = "Product details cannot be updated" });
            }
            return Ok();
        }

        [HttpPost("DeleteProduct")]
        [AllowAnonymous]
        public IActionResult DeleteProduct(int productId)
        {
            var result = _repository.Delete(productId);

            if (result == false)
            {
                return BadRequest(new { message = "Product details cannot be deleted" });
            }
            return Ok();
        }

    }
}