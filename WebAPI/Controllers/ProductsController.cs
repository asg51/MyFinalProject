using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productManager;

        public ProductsController(IProductService productManager)
        {
            _productManager = productManager;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_productManager.GetAll());
            //BadRequest 400 Ok 200 status gönderir
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            return Ok(_productManager.Add(product));
        }


        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            return Ok(_productManager.GetAllByCategoryId(categoryId));
        }
    }
}
