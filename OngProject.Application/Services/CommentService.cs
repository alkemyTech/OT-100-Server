using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Application.Services
{
    public class CommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCommentsDto>> GetComments()
        {
            var comments = await _unitOfWork.Comments.GetAll();
            
            return comments
                .AsQueryable()
                .ProjectTo<GetCommentsDto>(_mapper.ConfigurationProvider)
                .ToList();
        }

        /*
         * public async Task<Pagination<GetTestimonialsDto>> GetTestimonials(TestimonialsQueryDto queryDto)
        {
            var testimonials = await _unitOfWork.Testimonials.GetAll();

            return testimonials
                .AsQueryable()
                .ProjectTo<GetTestimonialsDto>(_mapper.ConfigurationProvider)
                .PaginatedResponse(queryDto.PageNumber, queryDto.PageSize);
        }
         */
    }
}