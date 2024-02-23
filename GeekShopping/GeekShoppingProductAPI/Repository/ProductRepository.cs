using AutoMapper;
using GeekShoppingProductAPI.Data.ValueObjects;
using GeekShoppingProductAPI.Models;
using GeekShoppingProductAPI.Models.Context;
using GeekShoppingProductAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShoppingProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? throw new Exception("Product not found") : _mapper.Map<ProductVO>(product);
        }


        public async Task<ProductVO> Create(ProductVO vo)
        {
            var entity = _mapper.Map<Product>(vo);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            //vo.Id = entity.Id;
            return _mapper.Map<ProductVO>(entity);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            var entity = _mapper.Map<Product>(vo);
            //_context.Entry(entity).State = EntityState.Modified;
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductVO>(entity);
        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
