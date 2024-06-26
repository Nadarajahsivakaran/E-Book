﻿using E_Book.DataAccess.IRepository;
using E_Book.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Book.DataAccess.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAll(string? includeProperties = "")
        {
            try
            {
                IQueryable<Book> query = _dbContext.Set<Book>();
                if (!string.IsNullOrWhiteSpace(includeProperties))
                    query = query.Include(includeProperties);
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw ;
            }
        }

        public async Task Create(Book book)
        {
            try
            {
                await _dbContext.Set<Book>().AddAsync(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> GetData(Expression<Func<Book, bool>> predicate, string? includeProperties = "")
        {
            try
            {
                IQueryable<Book> query = _dbContext.Set<Book>().Where(predicate);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                    query = query.Include(includeProperties);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching getData function: {ex.Message}");
                throw;
            }
        }

        public async Task Update(Book book)
        {
            try
            {
                _dbContext.Set<Book>().Update(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating books: {ex.Message}");
                throw;
            }
        }
    }
}
