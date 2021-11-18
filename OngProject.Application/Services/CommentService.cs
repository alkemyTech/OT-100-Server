using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Exceptions;
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
                .ProjectTo<GetCommentsDto>(_mapper.ConfigurationProvider);
        }

        public async Task Delete(int id)
        {
            var comments = await _unitOfWork.Comments.GetById(id);

            if (comments is null)
                throw new NotFoundException(nameof(comments), id);

            await _unitOfWork.Comments.Delete(comments);
            await _unitOfWork.CompleteAsync();

        }

    }
}