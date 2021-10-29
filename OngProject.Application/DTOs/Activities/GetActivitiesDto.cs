using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Activities
{
    public class GetActivitiesDto : IMapFrom<Activity>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}