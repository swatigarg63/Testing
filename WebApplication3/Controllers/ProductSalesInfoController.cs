using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSalesInfoController : ControllerBase
    {
        private readonly ISaleInfoRepository _repository;
        public ProductSalesInfoController(ISaleInfoRepository repository)
        {
            _repository = repository;
        }


        [HttpPost("AddSalesInfo")]
        [AllowAnonymous]
        public IActionResult AddSalesInfo([FromBody]SalesInfo salesInfo)
        {
            int TotalProductSoldPrice= 0;   
            int DiscountAmount = 0;
            string[] ProductPrices = salesInfo.ProductPrice.Split(",");

            foreach (var Price in ProductPrices)
            {
                TotalProductSoldPrice = Convert.ToInt32(Price) + TotalProductSoldPrice;
            }
            DiscountAmount = (TotalProductSoldPrice * salesInfo.Discount) / 100;
            salesInfo.InvoiceTotal = TotalProductSoldPrice + salesInfo.VATApplied - DiscountAmount;

            var result = _repository.Add(salesInfo);
            if (result == null)
            {
                return BadRequest(new { message = "Sales Info cannot be added" });
            }
            return Ok(result);
        }

    }
}