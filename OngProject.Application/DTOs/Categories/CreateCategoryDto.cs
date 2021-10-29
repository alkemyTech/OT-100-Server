﻿using Microsoft.AspNetCore.Http;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Application.DTOs.Categories
{
    public class CreateCategoryDto : IMapFrom<Category>
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
    }
}
