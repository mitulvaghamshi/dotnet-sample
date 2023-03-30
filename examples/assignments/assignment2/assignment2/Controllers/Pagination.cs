using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Controllers
{
    public class Pagination<T> : List<T>
    {
        public Pagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); ;
            AddRange(items);
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage { get => PageIndex > 1; }

        public bool HasNextPage { get => PageIndex < TotalPages; }

        public static async Task<Pagination<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(items, count, pageIndex, pageSize);
        }
    }
}