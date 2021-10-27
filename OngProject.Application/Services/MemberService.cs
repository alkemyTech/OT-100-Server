using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OngProject.Application.DTOs.Members;
using OngProject.DataAccess.Interfaces;

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
                .AsNoTracking()
                .ProjectTo<GetMembersDto>(_mapper.ConfigurationProvider);
        }
    }
}