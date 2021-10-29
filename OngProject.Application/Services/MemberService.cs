using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Members;
using OngProject.Application.Exceptions;
using OngProject.DataAccess.Interfaces;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class MemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetMembersDto>> GetMembers()
        {
            var members = await _unitOfWork.Members.GetAll();
            
            return members
                .AsQueryable()
                .ProjectTo<GetMembersDto>(_mapper.ConfigurationProvider);
        }

        public async Task<GetMemberDetailsDto> GetMemberDetails(int id)
        {
            var member = await _unitOfWork.Members.GetById(id);

            if (member is null)
                throw new NotFoundException(nameof(Member), id);

            return _mapper.Map<GetMemberDetailsDto>(member);
        }

        public async Task<int> CreateMember(CreateMemberDto memberDto)
        {
            var member = _mapper.Map<Member>(memberDto);

            await _unitOfWork.Members.Create(member);
            await _unitOfWork.CompleteAsync();

            return member.Id;
        }

        public async Task UpdateMember(int id, CreateMemberDto memberDto)
        {
            var member = await _unitOfWork.Members.GetById(id);
            
            if (member is null)
                throw new NotFoundException(nameof(Member), id);

            member.Id = id;
            await _unitOfWork.Members.Update(_mapper.Map(memberDto, member));
            await _unitOfWork.CompleteAsync();
        }

        public async Task SoftDeleteMember(int id)
        {
            var member = await _unitOfWork.Members.GetById(id);
            
            if (member is null)
                throw new NotFoundException(nameof(Member), id);

            await _unitOfWork.Members.Delete(member);
            await _unitOfWork.CompleteAsync();
        }
    }
}