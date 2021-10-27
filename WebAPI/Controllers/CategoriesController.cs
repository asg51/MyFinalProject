using Business.Abstract;
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
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
            //BadRequest 400 Ok 200 status gönderir
        }

        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            return BadRequest(_categoryService.Add(category));
        }
    }
}
