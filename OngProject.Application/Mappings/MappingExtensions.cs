using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace OngProject.Application.Mappings
{
    public static class MappingExtensions
    {
        public static List<TDestination> ProjectToList<TDestination>(this IQueryable queryable,
            IConfigurationProvider configurationProvider) 
            => queryable.ProjectTo<TDestination>(configurationProvider).ToList();
    }
}