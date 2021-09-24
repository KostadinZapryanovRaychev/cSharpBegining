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
    public class ProductService : IProductService
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Add(ProductDTO product)
        {
            Product productToAdd = _mapper.Map<Product>(product);
            Product result = (await _context.Products.AddAsync(productToAdd)).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<ProductDTO> Get(int id)
        {
            Product result = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDTO>(result);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            List<Product> result = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
            return _mapper.Map<List<ProductDTO>>(result);
        }

        public async Task<bool> Update(ProductDTO product)
        {
            if (!await Exist(product.Id))
            {
                return false;
            }

            Product productToUpdate = _mapper.Map<Product>(product);
            _context.Entry(productToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
