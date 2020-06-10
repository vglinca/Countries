using Countries.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Core.Extensions
{
	public static class QueryableExtensions
	{
		public static async Task<PagedResponse<TEntity>> CreatePaginatedResponse<TEntity>(this IQueryable<TEntity> source, PageArguments pageArgs, 
			SortingArguments sortArgs, List<FilterArguments> filterArgs, LogicalOperator logicalOperator) where TEntity : class
		{
			source = source.ApplyFilters(filterArgs, logicalOperator);
			var total = await source.CountAsync();
			source = source.ApplyPagination(pageArgs.PageIndex, pageArgs.PageSize);
			source = source.ApplySort(sortArgs.OrderBy, sortArgs.Direction);
			var listResult = await source.ToListAsync();
			return new PagedResponse<TEntity>
			{
				PageData = new PageData
				{
					PageIndex = pageArgs.PageIndex,
					PageSize = pageArgs.PageSize,
					TotalItems = total
				},
				Items = listResult
			};
		}

		public static IQueryable<TEntity> ApplyFilters<TEntity>(this IQueryable<TEntity> source, List<FilterArguments> filters, LogicalOperator op) where TEntity : class
		{
			var predicate = new StringBuilder();
			int index = 0;

			for (int j = 0; j < filters.Count; j++)
			{
				for (int i = 0; i < filters[j].FilterValues.Length; i++)
				{
					if (i + j != 0)
					{
						predicate.Append($" {op} ");
					}
					predicate.Append($"{filters[j].FilterProperty}.{nameof(string.Contains)}(@{index++})");
				}
			}

			if (filters.Any())
			{
				var propertyValues = filters.SelectMany(f => f.FilterValues).ToArray();
				source = source.Where(predicate.ToString(), propertyValues);
			}

			return source;
		}

		public static IQueryable<TEntity> ApplyPagination<TEntity>(this IQueryable<TEntity> source, int pageIndex, int pageSize) where TEntity : class
		{
			return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
		}

		public static IQueryable<TEntity> ApplySort<TEntity>(this IQueryable<TEntity> source, string orderBy, string direction) where TEntity : class
		{
			if (!string.IsNullOrWhiteSpace(orderBy))
			{
				source = source.OrderBy($"{orderBy} {direction}");
			}
			return source;
		}
	}
}
