using ICDify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDify.Application.Interfaces
{
    public  interface IIndicationMapper
    {
        Task<List<IndicationDto>> ExtractAndMapAsync(string drugName, CancellationToken cancellationToken = default);
    }
}
