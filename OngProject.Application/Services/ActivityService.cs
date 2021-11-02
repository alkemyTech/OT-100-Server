using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OngProject.Application.DTOs.Activities;
using OngProject.Application.Exceptions;
using OngProject.Application.Interfaces;
using OngProject.Application.Mappings;
using OngProject.Domain.Entities;

namespace OngProject.Application.Services
{
    public class ActivityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActivityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetActivitiesDto>> GetActivities()
        {
            var activities = await _unitOfWork.Activities.GetAll();

            return activities
                .AsQueryable()
                .ProjectToList<GetActivitiesDto>(_mapper.ConfigurationProvider);
        }

        public async Task<GetActivityDetailsDto> GetActivityDetails(int id)
        {
            var activity = await _unitOfWork.Activities.GetById(id);

            if (activity is null)
                throw new NotFoundException(nameof(Activity), id);

            return _mapper.Map<GetActivityDetailsDto>(activity);
        }

        public async Task<int> CreateActivity(CreateActivityDto activityDto)
        {
            var activity = _mapper.Map<Activity>(activityDto);

            await _unitOfWork.Activities.Create(activity);
            await _unitOfWork.CompleteAsync();

            return activity.Id;
        }

        public async Task UpdateActivity(int id, CreateActivityDto activityDto)
        {
            var activity = await _unitOfWork.Activities.GetById(id);

            if (activity is null)
                throw new NotFoundException(nameof(Activity), id);

            activity.Id = id;
            await _unitOfWork.Activities.Update(_mapper.Map(activityDto, activity));
            await _unitOfWork.CompleteAsync();
        }

        public async Task SoftDeleteActivity(int id)
        {
            var activity = await _unitOfWork.Activities.GetById(id);

            if (activity is null)
                throw new NotFoundException(nameof(Activity), id);

            await _unitOfWork.Activities.Delete(activity);
            await _unitOfWork.CompleteAsync();
        }
    }
}