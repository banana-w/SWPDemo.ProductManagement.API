using Microsoft.EntityFrameworkCore;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.ProductManagement.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductDbContext _context;
        private GenericRepository<Category> _categoryRepo;
        private GenericRepository<Product> _productRepo;
        private GenericRepository<Order> _orderRepo;


        public UnitOfWork(ProductDbContext context)
        {
            _context = context;
        }

       


        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        GenericRepository<Category> IUnitOfWork.CategoryRepository
        {
            get
            {
                if (_categoryRepo == null)
                {
                    this._categoryRepo = new GenericRepository<Category>(_context);
                }
                return _categoryRepo;
            }
        }

        GenericRepository<Product> IUnitOfWork.ProductRepository
        {
            get
            {
                if (_productRepo == null)
                {
                    this._productRepo = new GenericRepository<Product>(_context);
                }
                return _productRepo;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (_orderRepo == null)
                {
                    this._orderRepo = new GenericRepository<Order>(_context);
                }
                return _orderRepo;
            }
        }
    }
}
