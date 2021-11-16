using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.Helpers.Wrappers;

namespace OngProject.Application.Mappings
{
    public static class MappingExtensions
    {
        public static List<TDestination> ProjectToList<TDestination>(this IQueryable queryable, IConfigurationProvider configurationProvider) 
            => queryable.ProjectTo<TDestination>(configurationProvider).ToList();

        public static Pagination<TDestination> PaginatedResponse<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) 
            => Pagination<TDestination>.Create(queryable, pageNumber, pageSize);
    }
}