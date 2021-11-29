using IsfahanHC.Models.DataModels;
using IsfahanHC.Models.ViewModels;
using IsfahanHC.Models.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IsfahanHC.Data.Repository
{
    public interface IProductsRepository
    {
        IList<ShowProductViewModel> GetShowProducts();
        IList<ShowProductViewModel> GetShowProductsWhere(Expression<Func<ShowProductViewModel, bool>> Predicate);
        IList<ShowProductViewModel> GetShowProductsByStor(int storId);
        IList<ShowProductViewModel> GetShowProductsBySeller(int userId);
        ShowProductViewModel GetShowProductById(int id);
        Product GetProductById(int id);
        int AddProduct(AddProductViewModel addProduct);
        void EditProduct(EditProductViewModel addProduct);
        void AddProductImage(ProductImageViewModel productImage);
        void RemoveProductImageById(int imageId);
        void RemoveProductById(int productId);
        int GetProductIdByImage(int imageId);
        // void AddProductImage();
    }

    public class ProductsRepository : IProductsRepository
    {
        IsfahanHCDbContext _context;
        public ProductsRepository(IsfahanHCDbContext context)
        {
            _context = context;
        }

        public int AddProduct(AddProductViewModel addProduct)
        {
            var storId = _context.Users
                .Include(u => u.Seller)
                .Where(u => u.UserId == addProduct.UserId)
                .Single()
                .Seller.StorId;

            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                StorId = storId,
            };
            _context.Add(product);
            _context.SaveChanges();
            var item = new Item
            {
                Price = addProduct.Price,
                QuantityInStock = addProduct.QuantityInStock,
                ProductId = product.ProductId
            };
            _context.Add(item);

            foreach (var image in addProduct.Images)
            {
                string fileName, filePath;
                CreateFilePath(image.FileName, product.ProductId, out fileName, out filePath);

                CopyFileToServer(image, filePath);

                var pImage = new PImage
                {
                    ProductId = product.ProductId,
                    FileName = fileName,
                };
                _context.Add(pImage);
            }

            _context.SaveChanges();
            return product.ProductId;
        }

        private static void CopyFileToServer(IFormFile file, string filePath)
        {
            using (Stream st = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(st);
            }
        }

        public void AddProductImage(ProductImageViewModel productImage)
        {
            foreach (var image in productImage.UploadImages)
            {
                string fileName, filePath;
                CreateFilePath(image.FileName, productImage.ProductId, out fileName, out filePath);
                CopyFileToServer(image, filePath);

                var pImg = new PImage
                {
                    ProductId = productImage.ProductId,
                    FileName = fileName
                };
                _context.Add(pImg);
            }
            _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .Include(p => p.Images)
                .Include(p => p.Item)
                .Where(p => p.ProductId == id)
                .FirstOrDefault();
        }

        public ShowProductViewModel GetShowProductById(int id)
        {
            return _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => new ShowProductViewModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    StorName = p.Stor.Name,
                    ImagesFileName = p.Images.Select(s => s.FileName),
                    Price = p.Item.Price,
                    QuantityInStock = p.Item.QuantityInStock,
                    ProductId = p.ProductId
                }).FirstOrDefault();
        }

        public IList<ShowProductViewModel> GetShowProducts()
        {
            return _context.Products
                .Include(p => p.Item)
                .Include(p => p.Images)
                .Select(p => new ShowProductViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    StorName = p.Stor.Name,
                    ImagesFileName = p.Images.Select(s => s.FileName).ToList(),
                    Price = p.Item.Price,
                    QuantityInStock = p.Item.QuantityInStock,
                    ProductId = p.ProductId
                }).ToList();
        }

        public IList<ShowProductViewModel> GetShowProductsBySeller(int userId)
        {
            var storId = 0;
            try
            {
                storId = _context.Sellers
                    .Where(s => s.UserId == userId)
                    .FirstOrDefault()
                    .StorId;
            }
            catch (NullReferenceException)
            {
                return new List<ShowProductViewModel>();
            }
            return GetShowProductsByStor(storId);
        }

        public IList<ShowProductViewModel> GetShowProductsByStor(int storId)
        {
            return _context.Products
                .Include(p => p.Item)
                .Include(p => p.Images)
                .Where(p => p.StorId == storId)
                .Select(p => new ShowProductViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    StorName = p.Stor.Name,
                    ImagesFileName = p.Images.Select(s => s.FileName).ToList(),
                    Price = p.Item.Price,
                    QuantityInStock = p.Item.QuantityInStock,
                    ProductId = p.ProductId
                }).ToList();
        }


        private static void CreateFilePath(string imageFileName, int productId, out string fileName, out string filePath)
        {
            var extention = Path.GetExtension(imageFileName);
            fileName = Guid.NewGuid().ToString().Replace("-", "q") + productId + extention;
            var directory = Path.Combine(Directory.GetCurrentDirectory(),
                                    "wwwroot",
                                    "img",
                                    "Products",
                                    "p" + productId);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            filePath = Path.Combine(directory, fileName);
        }
        private static string GetProductImagePath(string imageFileName, int productId)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "Products",
                        "p" + productId);
            return Path.Combine(directory, imageFileName);
        }
        public void RemoveProductImageById(int id)
        {
            var image = _context.PImages.
                        FirstOrDefault(pi => pi.ImageId == id);

            if (image == null) return;

            string fp = GetProductImagePath(image.FileName, image.ProductId);
            if (File.Exists(fp))
                File.Delete(fp);


            _context.Remove(image);
            _context.SaveChanges();

        }

        public int GetProductIdByImage(int imageId)
        {
            return _context.PImages
                .FirstOrDefault(pi => pi.ImageId == imageId)
                .ProductId;
        }

        public void EditProduct(EditProductViewModel addProduct)
        {
            var pro = _context.Products
                .Include(p => p.Item)
                .Where(p => p.ProductId == addProduct.ProductId)
                .FirstOrDefault();

            if (pro != null)
            {
                pro.Name = addProduct.Name;
                pro.Description = addProduct.Description;
                pro.Item.Price = addProduct.Price;
                pro.Item.QuantityInStock = addProduct.QuantityInStock;
                _context.SaveChanges();
            }

        }

        public void RemoveProductById(int productId)
        {
            var product = _context.Products
                .Include(p => p.Images)
                //.Include(p => p.Item)
                .FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
                return;

            foreach (var pImage in product.Images)
            {
                var imgPath = GetProductImagePath(pImage.FileName, productId);
                if (File.Exists(imgPath))
                    File.Delete(imgPath);
            }
            _context.Remove(product);
            _context.SaveChanges();
        }

        public IList<ShowProductViewModel> GetShowProductsWhere(Expression<Func<ShowProductViewModel, bool>> predicate)
        {
            return _context.Products
                .Include(p => p.Item)
                .Include(p => p.Images)
                .Include(p => p.Stor)
                .Select(p => new ShowProductViewModel()
                {
                    Name = p.Name,
                    Description = p.Description,
                    StorName = p.Stor.Name,
                    ImagesFileName = p.Images.Select(s => s.FileName).ToList(),
                    Price = p.Item.Price,
                    QuantityInStock = p.Item.QuantityInStock,
                    ProductId = p.ProductId
                })
                .Where(predicate)
                .ToList();
        }
    }
}
