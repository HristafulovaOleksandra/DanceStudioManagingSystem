using AutoMapper;
using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Domain.Entities;
using DanceStudio.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InstructorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructorDTO>> GetAllInstructorsAsync()
        {
            var instructors = await _unitOfWork.Instructors.GetAllAsync();
            return _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
        }

        public async Task<InstructorDTO?> GetInstructorByIdAsync(long id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {id} not found.");
            }
            return _mapper.Map<InstructorDTO>(instructor);
        }

        public async Task<InstructorDTO> CreateInstructorAsync(InstructorCreateUpdateDTO dto)
        {
            var instructor = _mapper.Map<Instructor>(dto);

            await _unitOfWork.Instructors.AddAsync(instructor);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<InstructorDTO>(instructor);
        }

        public async Task UpdateInstructorAsync(long id, InstructorCreateUpdateDTO dto)
        {
            var existingInstructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (existingInstructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {id} not found.");
            }

            _mapper.Map(dto, existingInstructor);

            _unitOfWork.Instructors.Update(existingInstructor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteInstructorAsync(long id)
        {
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(id);
            if (instructor == null)
            {
                throw new KeyNotFoundException($"Instructor with ID {id} not found.");
            }

            _unitOfWork.Instructors.Delete(instructor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<InstructorDTO>> GetInstructorsWithClassesAsync()
        {
            var instructors = await _unitOfWork.Instructors.GetInstructorsWithClassesAsync();
            return _mapper.Map<IEnumerable<InstructorDTO>>(instructors);
        }
    }
}
