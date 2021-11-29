using IsfahanHC.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IsfahanHC.Data.Repository
{
    public interface IStorRepository
    {
        Task<IList<StorViewModel>> GetStorsAsync();
        StorViewModel GetStorByProduct(int productId);
        StorViewModel GetStorById(int id);
    }

    public class StorRepository : IStorRepository
    {
        IsfahanHCDbContext _context;
        public StorRepository(IsfahanHCDbContext context)
        {
            _context = context;
        }

        public StorViewModel GetStorById(int id)
        {
            return _context.Stors
                .Include(s => s.Image)
                .Include(s => s.Products)
                .Include(s => s.Sellers)
                .Where(s => s.StorId == id)
                .Select(s => new StorViewModel
                {
                    ImageFileName = s.Image.FileName,
                    Name = s.Name,
                    ProductsCount = s.Products.Count,
                    SellersCount = s.Sellers.Count,
                    StorId = s.StorId
                })
                .FirstOrDefault();
        }

        public StorViewModel GetStorByProduct(int productId)
        {
            return _context.Stors
                .Include(s => s.Image)
                .Include(s => s.Products)
                .Include(s => s.Sellers)
                .Where(s => s.Products.Any(p => p.ProductId == productId))
                .Select(s => new StorViewModel
                {
                    ImageFileName = s.Image.FileName,
                    Name = s.Name,
                    ProductsCount = s.Products.Count,
                    SellersCount = s.Sellers.Count,
                    StorId = s.StorId
                })
                .FirstOrDefault();
        }

        public async Task<IList<StorViewModel>> GetStorsAsync()
        {
            return await _context.Stors
                .Include(s => s.Image)
                .Include(s => s.Products)
                .Include(s => s.Sellers)
                .Select(s => new StorViewModel
                {
                    ImageFileName = s.Image.FileName,
                    Name = s.Name,
                    ProductsCount = s.Products.Count,
                    SellersCount = s.Sellers.Count,
                    StorId = s.StorId
                })
                .ToListAsync();
        }
    }
}
