using AutoMapper;
using Domain.Common.Pagionation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public class PaginatedListConverter<TSource, TDestination> :
     ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>>
    {
        public PaginatedList<TDestination> Convert(
            PaginatedList<TSource> source,
            PaginatedList<TDestination> destination,
            ResolutionContext context)
        {
            var items = context.Mapper.Map<List<TDestination>>(source.Items);

            return new PaginatedList<TDestination>(
                items,
                source.TotalCount,
                source.PageNumber,
                source.PageSize
            );
        }
    }

}
