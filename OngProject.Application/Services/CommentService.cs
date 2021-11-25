using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Comments;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Domain.Entities;
using OngProject.Application.Interfaces.Identity;
using System;

namespace OngProject.Application.Services
{
    public class CommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _identityService = identityService;
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

        public async Task Update(int id, UpdateCommentDto updateComment)
        {

            var comment = await _unitOfWork.Comments.GetById(id);

            if (comment is null)
                throw new NotFoundException(nameof(Comment), id);

            var user = await _unitOfWork.UsersDetails.GetById(comment.UserDetailsId);

            if (user.IdentityId != new Guid(_currentUserService.userId))
                throw new BadRequestException("403 - You do not have permission to modify.");


            await _unitOfWork.Comments.Update(_mapper.Map(updateComment, comment));
            await _unitOfWork.CompleteAsync();

        }
        public async Task Delete(int id)
        {
            var comment = await _unitOfWork.Comments.GetById(id);

            if (comment is null)
                throw new NotFoundException(nameof(Comment), id);

            var user = await _unitOfWork.UsersDetails.GetById(comment.UserDetailsId);
            var role = await _identityService.GetUserRol(_currentUserService.userId);

            if (user.IdentityId != new Guid(_currentUserService.userId) || role =="Admin")
                throw new BadRequestException("403 - You do not have permission to delete.");

            await _unitOfWork.Comments.Delete(comment);
            await _unitOfWork.CompleteAsync();
           
        }

    }
}