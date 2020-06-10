using Countries.Core.Extensions;
using Countries.Core.Infrastructure;
using Countries.Core.Repository.Interfaces;
using Countries.DAL;
using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Repository
{
	public class GenericRepository : IGenericRepository
	{
		private readonly CountriesDbContext _ctx;
		public GenericRepository(CountriesDbContext ctx)
		{
			_ctx = ctx;
		}
		public async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			await _ctx.Set<TEntity>().AddAsync(entity);
			await _ctx.SaveChangesAsync();
			return entity;
		}

		public async Task<IEnumerable<long>> CreateAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
		{
			await _ctx.Set<TEntity>().AddRangeAsync(entities);
			await _ctx.SaveChangesAsync();
			return entities.Select(e => e.Id);
		}

		public async Task DeleteAsync<TEntity>(long id) where TEntity : BaseEntity
		{
			var entity = await _ctx.Set<TEntity>().FindAsync(id);
			_ctx.Set<TEntity>().Remove(entity);
			await _ctx.SaveChangesAsync();
		}

		public async Task<bool> ExistsAsync<TEntity>(long id) where TEntity : BaseEntity
		{
			var entity = await _ctx.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
			return entity != null;
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
		{
			return await _ctx.Set<TEntity>().ToListAsync();
		}

		public async Task<PagedResponse<TEntity>> GetAllAsync<TEntity>(PageArguments pageArgs, SortingArguments sortingArgs, 
			List<FilterArguments> filterArgs, LogicalOperator logicalOperator) where TEntity : BaseEntity
		{
			return await _ctx.Set<TEntity>().CreatePaginatedResponse(pageArgs, sortingArgs, filterArgs, logicalOperator);
		}

		public async Task<IEnumerable<TEntity>> GetListUsingFilters<TEntity>(List<FilterArguments> filters, LogicalOperator op) where TEntity : BaseEntity
		{
			var entities = _ctx.Set<TEntity>().AsQueryable().ApplyFilters(filters, op);
			return await entities.ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> GetListWithPredicateAsync<TEntity>(List<Expression<Func<TEntity, bool>>> predicates)
			where TEntity : BaseEntity
		{
			var entities = _ctx.Set<TEntity>().AsQueryable();
			foreach (var exp in predicates)
			{
				entities = entities.Where(exp);
			}
			return await entities.ToListAsync();
		}

		public async Task<TEntity> GetOneAsync<TEntity>(long id) where TEntity : BaseEntity
		{
			var entity = await _ctx.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
			return entity;
		}

		public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
		{
			_ctx.Entry(entity).State = EntityState.Modified;
			await _ctx.SaveChangesAsync();
		}
	}
}
