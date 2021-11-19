using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Domain.Entities;

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

        public async Task<List<GetCommentsDto>> GetComments()
        {
            var comments = await _unitOfWork.Comments.GetAll();

            return comments.OrderBy(x => x.CreatedAt)
                           .AsQueryable()
                           .ProjectTo<GetCommentsDto>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<int> CreateComment(CreateCommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);

            await _unitOfWork.Comments.Create(comment);
            await _unitOfWork.CompleteAsync();

            return comment.Id;
        }

    }
}