using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System;

namespace OngProject.Application.DTOs.Comments
{
    public class GetCommentsDto : IMapFrom<Comment>
    {
        public string Body { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
