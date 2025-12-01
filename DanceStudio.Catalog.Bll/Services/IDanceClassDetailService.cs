using DanceStudio.Catalog.Bll.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanceStudio.Catalog.Bll.Services
{
    public interface IDanceClassDetailService
    {
        Task<DanceClassDetailDTO?> GetDetailByIdAsync(long id);
        Task<DanceClassDetailDTO> CreateDetailAsync(DanceClassDetailCreateUpdateDTO dto);
        Task UpdateDetailAsync(long id, DanceClassDetailCreateUpdateDTO dto);
        Task DeleteDetailAsync(long id);

        Task<DanceClassDetailDTO?> GetDetailWithClassAsync(long detailId);
    }
}
