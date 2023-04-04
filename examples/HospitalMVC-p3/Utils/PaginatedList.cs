using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Utils;

public class PaginatedList<T> : List<T>
{
	public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
	{
		PageIndex = pageIndex;
		PageSize = (int)Math.Ceiling(count / (double)pageSize);
		TotalItems = count;
		AddRange(items);
	}

	public int PageIndex { get; private set; }

	public int PageSize { get; private set; }

	public int TotalItems { get; private set; }

	public bool HasPreviousPage { get => PageIndex > 1; }

	public bool HasNextPage { get => PageIndex < PageSize; }

	public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> items, int index, int size) =>
		new PaginatedList<T>(await items.Skip((index - 1) * size).Take(size).ToListAsync(),
			await items.CountAsync(), index, size);
}
