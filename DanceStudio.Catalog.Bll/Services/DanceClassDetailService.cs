using AutoMapper;
using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public class DanceClassDetailService : IDanceClassDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DanceClassDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DanceClassDetailDTO?> GetDetailByIdAsync(long id)
        {
            var detail = await _unitOfWork.ClassDetails.GetByIdAsync(id);
            if (detail == null)
            {
                throw new KeyNotFoundException($"Dance class detail with ID {id} not found.");
            }
            return _mapper.Map<DanceClassDetailDTO>(detail);
        }

        public async Task<DanceClassDetailDTO?> GetDetailWithClassAsync(long detailId)
        {
            var detail = await _unitOfWork.ClassDetails.GetDetailWithClassExplicitlyAsync(detailId);

            if (detail == null)
            {
                throw new KeyNotFoundException($"Dance class detail with ID {detailId} not found.");
            }

            return _mapper.Map<DanceClassDetailDTO>(detail);
        }

        public async Task<DanceClassDetailDTO> CreateDetailAsync(DanceClassDetailCreateUpdateDTO dto)
        {
            var danceClass = await _unitOfWork.DanceClasses.GetByIdAsync(dto.DanceClassId);
            if (danceClass == null)
            {
                throw new KeyNotFoundException($"DanceClass with ID {dto.DanceClassId} does not exist. Cannot create detail.");
            }

            var detail = _mapper.Map<DanceClassDetail>(dto);
            detail.Id = dto.DanceClassId;

            await _unitOfWork.ClassDetails.AddAsync(detail);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DanceClassDetailDTO>(detail);
        }

        public async Task UpdateDetailAsync(long id, DanceClassDetailCreateUpdateDTO dto)
        {
            var existingDetail = await _unitOfWork.ClassDetails.GetByIdAsync(id);
            if (existingDetail == null)
            {
                throw new KeyNotFoundException($"Dance class detail with ID {id} not found.");
            }


            if (existingDetail.Id != dto.DanceClassId)
            {
                throw new InvalidOperationException("Cannot change the associated DanceClassId after creation.");
            }

            _mapper.Map(dto, existingDetail);

            _unitOfWork.ClassDetails.Update(existingDetail);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteDetailAsync(long id)
        {
            var detail = await _unitOfWork.ClassDetails.GetByIdAsync(id);
            if (detail == null)
            {
                throw new KeyNotFoundException($"Dance class detail with ID {id} not found.");
            }

            _unitOfWork.ClassDetails.Delete(detail);
            await _unitOfWork.CompleteAsync();
        }
    }
}