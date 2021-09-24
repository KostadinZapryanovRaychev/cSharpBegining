using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSimpleProject.Data;
using WebApiSimpleProject.Data.Models;
using WebApiSimpleProject.Models;
using WebApiSimpleProject.Services.Interfaces;

namespace WebApiSimpleProject.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductCategoryDTO> Add(ProductCategoryDTO productCategory)
        {
            ProductCategory productCategoryToAdd = _mapper.Map<ProductCategory>(productCategory);
            ProductCategory result = (await _context.ProductCategories.AddAsync(productCategoryToAdd)).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductCategoryDTO>(result);
        }

        public async Task<bool> Delete(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return false;
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.ProductCategories.AnyAsync(pc => pc.Id == id);
        }

        public async Task<ProductCategoryDTO> Get(int id)
        {
            ProductCategory result = await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.Id == id);
            return _mapper.Map<ProductCategoryDTO>(result);
        }

        public async Task<List<ProductCategoryDTO>> GetAll()
        {
            List<ProductCategory> result = await _context.ProductCategories.ToListAsync();
            return _mapper.Map<List<ProductCategoryDTO>>(result);
        }

        public async Task<bool> Update(ProductCategoryDTO productCategory)
        {
            if (!await Exist(productCategory.Id))
            {
                return false;
            }

            ProductCategory productCategoryToUpdate = _mapper.Map<ProductCategory>(productCategory);
            _context.Entry(productCategoryToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
