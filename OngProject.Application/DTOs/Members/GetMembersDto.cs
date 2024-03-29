﻿using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Members
{
    public class GetMembersDto : IMapFrom<Member>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
      
    }
}