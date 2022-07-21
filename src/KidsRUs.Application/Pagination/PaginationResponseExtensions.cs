
using Microsoft.EntityFrameworkCore;

public static class PaginationResponseExtensions
{
    public static async Task<PaginationResponse<TDestination>> PaginatedListAsync<T, TDestination>(
        this IQueryable<T> repository, IMapper map, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        where T : class
        where TDestination : class, IMapFrom<T>
    {
        var list = await repository.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        var count = await repository.CountAsync(cancellationToken);

        var destinationList = map.Map<List<TDestination>>(list);
        
        return new PaginationResponse<TDestination>(destinationList, count, pageNumber, pageSize);
    }
}