using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApiSimpleProject.Data;
using WebApiSimpleProject.Data.Models;
using WebApiSimpleProject.Models;
using WebApiSimpleProject.Services.Interfaces;

namespace WebApiSimpleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            try
            {
                return await _productService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            try
            {
                var product = await _productService.Get(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // PUT: api/Product/{id}
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            try
            {
                if (product.Id <= 0)
                {
                    return BadRequest();
                }

                if (!await _productCategoryService.Exist(product.CategoryId))
                {
                    return NotFound(new { Message = "Category not found" });
                }

                bool result = await _productService.Update(product);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(ProductDTO product)
        {
            try
            {
                if (!await _productCategoryService.Exist(product.CategoryId))
                {
                    return NotFound(new { Message = "Category not found" });
                }
                ProductDTO result = await _productService.Add(product);


                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                bool result = await _productService.Delete(id);
                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
