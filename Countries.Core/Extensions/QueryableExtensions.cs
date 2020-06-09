using Countries.Core.Infrastructure;
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
		public static IQueryable<TEntity> ApplyFilters<TEntity>(this IQueryable<TEntity> source, List<Filter> filters, LogicalOperator op) where TEntity : class
		{
			var predicate = new StringBuilder();
			int index = 0;

			for (int j = 0; j < filters.Count; j++)
			{
				for (int i = 0; i < filters[j].PropertyValues.Length; i++)
				{
					if (i + j != 0)
					{
						predicate.Append($" {op} ");
					}
					predicate.Append($"{filters[j].PropertyName}.{nameof(string.Contains)}(@{index++})");
				}
			}

			if (filters.Any())
			{
				var propertyValues = filters.SelectMany(f => f.PropertyValues).ToArray();
				source = source.Where(predicate.ToString(), propertyValues);
			}

			return source;
		}
	}
}
