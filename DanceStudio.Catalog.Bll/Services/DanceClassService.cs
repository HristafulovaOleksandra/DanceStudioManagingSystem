using AutoMapper;
using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Bll.Helpers;
using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using DanceStudio.Catalog.Domain.Specifications.DanceClassSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public class DanceClassService : IDanceClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DanceClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<DanceClassDTO>> GetAllClassesAsync(DanceClassSpecParams specParams)
        {

            var spec = new DanceClassesSpecification(
                specParams.DifficultyLevel,
                specParams.Sort,
                specParams.PageIndex,
                specParams.PageSize,
                specParams.Search
            );

            var countSpec = new DanceClassesWithFiltersForCountSpecification(
                specParams.DifficultyLevel,
                specParams.Search
            );

            var totalItems = await _unitOfWork.DanceClasses.CountAsync(countSpec);
            var classes = await _unitOfWork.DanceClasses.ListAsync(spec);


            var data = _mapper.Map<IReadOnlyList<DanceClassDTO>>(classes);

            return new Pagination<DanceClassDTO>(
                specParams.PageIndex,
                specParams.PageSize,
                totalItems,
                data
            );
        }

        public async Task<DanceClassDTO?> GetClassByIdAsync(long id)
        {
            var danceClassCollection = await _unitOfWork.DanceClasses.GetAllAsync(filter: dc => dc.Id == id);
            var entity = danceClassCollection.FirstOrDefault();

            if (entity == null)
            {
                throw new KeyNotFoundException($"Dance class with ID {id} not found.");
            }
            return _mapper.Map<DanceClassDTO>(entity);
        }

        public async Task<DanceClassDTO> CreateClassAsync(DanceClassCreateUpdateDTO dto)
        {
            var danceClass = _mapper.Map<DanceClass>(dto);

            await _unitOfWork.DanceClasses.AddAsync(danceClass);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DanceClassDTO>(danceClass);
        }

        public async Task UpdateClassAsync(long id, DanceClassCreateUpdateDTO dto)
        {
            var existingClass = await _unitOfWork.DanceClasses.GetByIdAsync(id);
            if (existingClass == null)
            {
                throw new KeyNotFoundException($"Dance class with ID {id} not found.");
            }

            _mapper.Map(dto, existingClass);

            _unitOfWork.DanceClasses.Update(existingClass);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClassAsync(long id)
        {
            var danceClass = await _unitOfWork.DanceClasses.GetByIdAsync(id);
            if (danceClass == null)
            {
                throw new KeyNotFoundException($"Dance class with ID {id} not found.");
            }

            _unitOfWork.DanceClasses.Delete(danceClass);
            await _unitOfWork.CompleteAsync();
        }

    }
}
