using Countries.Core.Infrastructure;
using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Repository.Interfaces
{
	public interface IGenericRepository
	{
		Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;
		Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(PageArguments pageArgs, SortingArguments sortingArgs, List<FilterArguments> filterArgs, LogicalOperator logicalOperator) 
			where TEntity : BaseEntity;
		Task<IEnumerable<TEntity>> GetListWithPredicateAsync<TEntity>(List<Expression<Func<TEntity, bool>>> predicates) where TEntity : BaseEntity;
		Task<IEnumerable<TEntity>> GetListUsingFilters<TEntity>(List<FilterArguments> filters, LogicalOperator op) where TEntity : BaseEntity;
		Task<TEntity> GetOneAsync<TEntity>(long id) where TEntity : BaseEntity;
		Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
		Task<IEnumerable<long>> CreateAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;
		Task UpdateAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
		Task DeleteAsync<TEntity>(long id) where TEntity : BaseEntity;
		Task<bool> ExistsAsync<TEntity>(long id) where TEntity : BaseEntity;
	}
}
