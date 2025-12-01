using DanceStudio.Catalog.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public interface IInstructorService
    {
        Task<IEnumerable<InstructorDTO>> GetAllInstructorsAsync();
        Task<InstructorDTO?> GetInstructorByIdAsync(long id);
        Task<InstructorDTO> CreateInstructorAsync(InstructorCreateUpdateDTO dto);
        Task UpdateInstructorAsync(long id, InstructorCreateUpdateDTO dto);
        Task DeleteInstructorAsync(long id);

        Task<IEnumerable<InstructorDTO>> GetInstructorsWithClassesAsync();
    }
}
