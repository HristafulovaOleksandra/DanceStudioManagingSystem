using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Bll.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public interface IDanceClassService
    {
        Task<Pagination<DanceClassDTO>> GetAllClassesAsync(DanceClassSpecParams specParams); 

        Task<DanceClassDTO?> GetClassByIdAsync(long id);
        Task<DanceClassDTO> CreateClassAsync(DanceClassCreateUpdateDTO dto);
        Task UpdateClassAsync(long id, DanceClassCreateUpdateDTO dto);
        Task DeleteClassAsync(long id);

    }
}
