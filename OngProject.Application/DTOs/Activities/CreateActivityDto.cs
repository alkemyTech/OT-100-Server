﻿using System.ComponentModel.DataAnnotations;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.DTOs.Activities
{
    public class CreateActivityDto : IMapFrom<Activity>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(1200)]
        public string Content { get; set; }
        
        [Required]
        //[DataType(DataType.Upload)]
        public string Image { get; set; }
    }
}