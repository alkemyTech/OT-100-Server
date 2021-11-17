﻿using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Comments
{
    public class GetCommentsDto : IMapFrom<Comment>
    {
        public string Body { get; set; }

    }
}
