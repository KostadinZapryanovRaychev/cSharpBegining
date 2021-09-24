using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiSimpleProject.Models;
using WebApiSimpleProject.Services.Interfaces;

namespace WebApiSimpleProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(IProductCategoryService productCategoryService, ILogger<ProductCategoryController> logger)
        {
            _productCategoryService = productCategoryService;
            _logger = logger;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetProductCategories()
        {
            try
            {
                return await _productCategoryService.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // GET: api/ProductCategory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDTO>> GetProductCategory(int id)
        {
            try
            {
                var productCategory = await _productCategoryService.Get(id);

                if (productCategory == null)
                {
                    return NotFound();
                }


                return productCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // PUT: api/ProductCategory
        [HttpPut]
        public async Task<IActionResult> UpdateProductCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                if (productCategory.Id <= 0)
                {
                    return BadRequest();
                }

                bool result = await _productCategoryService.Update(productCategory);

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

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTO>> AddProductCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                ProductCategoryDTO result = await _productCategoryService.Add(productCategory);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

        // DELETE: api/ProductCategory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                bool result = await _productCategoryService.Delete(id);
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
