﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.DTO;
using Gex.Utils;

namespace Gex.Services.Interface;
public interface IMateriaService
{
    Task<GexResponse<ICollection<MateriaDTO>>> GetMateriasAsync();
    Task<GexResponse<MateriaDTO>> GetMateriaAsync(int id);
    Task<GexResponse<MateriaDTO>> CreateMateriaAsync(MateriaDTO MateriaDTO);
    Task<GexResponse<MateriaDTO>> UpdateMateriaAsync(MateriaDTO MateriaDTO);
    Task<GexResponse<MateriaDTO>> DeleteMateriaAsync(int id);
}