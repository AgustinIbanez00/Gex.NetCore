﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Gex.Utils;
using Gex.ViewModels.Request;
using Gex.ViewModels.Response;

namespace Gex.Services.Interface;
public interface IExamenService
{
    Task<GexResult<ICollection<ExamenResponse>>> GetExamenesAsync();
    Task<GexResult<ExamenResponse>> GetExamenAsync(long id);
    Task<GexResult<ExamenResponse>> CreateExamenAsync(ExamenRequest ExamenDTO);
    Task<GexResult<ExamenResponse>> UpdateExamenAsync(ExamenRequest ExamenDTO);
    Task<GexResult<ExamenResponse>> DeleteExamenAsync(long id);
}
