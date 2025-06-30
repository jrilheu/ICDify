using ICDify.Application.DTOs;
using ICDify.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICDify.Application.UseCases
{
    public class ExtractAndMapIndicationsUseCase(IIndicationMapper mapper, IDrugRepository repository)
    {
        private readonly IIndicationMapper _mapper = mapper;
        private readonly IDrugRepository _repository = repository;

        public async Task<List<IndicationDto>> ExecuteAsync(string drugName)
        {
            var indications = await _mapper.ExtractAndMapAsync(drugName);
            await _repository.SaveIndicationsAsync(drugName, indications);
            return indications;
        }
    }
}
