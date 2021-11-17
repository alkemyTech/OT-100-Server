using System;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Application.Helpers.Wrappers
{
    public class Pagination<T>
    {
        public Pagination(int pageIndex, int pageSize, int count, List<T> items)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public IList<T> Items { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static Pagination<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new Pagination<T>(pageIndex, pageSize, count, items);
        }
    }
}